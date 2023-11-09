using MediatR;
using Microsoft.EntityFrameworkCore;
using RoutePlanning.Application.Locations.Queries.DeliveryInfo;

namespace RoutePlanning.Domain.Locations.Services;

public sealed class ShortestDistanceService : IShortestDistanceService
{
    private readonly IQueryable<Location> _locations;
    private readonly IMediator _mediator;

    public ShortestDistanceService(IQueryable<Location> locations, IMediator mediator)
    {
        _locations = locations;
        _mediator = mediator;

    }

    public async Task<int> CalculateShortestDistance(Location source, Location target, RouteType type, Parcel parcel)
    {
        var locations = _locations.Include(l => l.Connections).ThenInclude(c => c.Destination);

        var path = await CalculateShortestPath(locations, source, target, type, parcel);

        return path.Sum(c => c.Distance);
    }

    /// <summary>
    /// An implementation of the Dijkstra's shortest path algorithm
    /// </summary>
    private async Task<IEnumerable<Connection>> CalculateShortestPath(IEnumerable<Location> locations, Location start, Location end, RouteType type, Parcel parcel)
    {
        var shortestConnections = await CalculateShortestConnections(locations, start, end, type, parcel);

        var path = ConstructShortestPath(start, end, shortestConnections);

        return path;
    }

    /// <summary>
    /// An implementation of the Dijkstra's algorithm that computes the shortest connections to all locations until the end location is reached
    /// </summary>
    private async Task<Dictionary<Location, (Connection? SourceConnection, double Distance)>> CalculateShortestConnections(IEnumerable<Location> locations, Location start, Location end, RouteType type, Parcel parcel)
    {
        var shortestConnections = new Dictionary<Location, (Connection? SourceConnection, double Distance)>();
        var unvisitedLocations = locations.ToHashSet();

        foreach (var location in unvisitedLocations)
        {
            shortestConnections[location] = (SourceConnection: null, Distance: int.MaxValue);
        }

        shortestConnections[start] = (SourceConnection: null, Distance: 0);

        while (unvisitedLocations.Count > 0)
        {
            var location = unvisitedLocations.OrderBy(location => shortestConnections[location].Distance).First();

            if (location == end)
            {
                break;
            }

            foreach (var connection in location.Connections)
            {
                await UpdateShortestConnections(shortestConnections, location, connection, type, parcel);
            }

            unvisitedLocations.Remove(location);
        }

        return shortestConnections;
    }

    private async Task UpdateShortestConnections(Dictionary<Location, (Connection? SourceConnection, double Distance)> shortestConnections, Location location, Connection connection, RouteType type, Parcel parcel)
    {
        var types = parcel.GenerateTypesFromParcel();

        var segmentCostQuery = new DeliveryInfoQuery(connection.Source.Name, connection.Destination.Name, types, parcel.Weight);
        var deliveryInfo = await _mediator.Send(segmentCostQuery);

        double distance;

        if (type == RouteType.Fastest)
        {
            distance = shortestConnections[location].Distance + deliveryInfo.Time;
        }
        else if (type == RouteType.Cheapest)
        {
            distance = shortestConnections[location].Distance + deliveryInfo.Price;
        }
        else
        {
            distance = shortestConnections[location].Distance + deliveryInfo.Time;
        }

        if (distance < shortestConnections[connection.Destination].Distance)
        {
            shortestConnections[connection.Destination] = (SourceConnection: connection, Distance: distance);
        }
    }

        

    /// <summary>
    /// The shortest path is constructed by backtracking the Dijkstra connection data from the end location
    /// </summary>
    private static IEnumerable<Connection> ConstructShortestPath(Location start, Location end, Dictionary<Location, (Connection? SourceConnection, double Distance)> sourceConnections)
    {
        var path = new List<Connection>();
        var location = end;

        while (location != start)
        {
            var shortestConnection = sourceConnections[location].SourceConnection!;
            path.Add(shortestConnection);
            location = shortestConnection.Source;
        }

        path.Reverse();

        return path;
    }
}

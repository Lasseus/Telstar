using Microsoft.EntityFrameworkCore;
using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Locations;
using RoutePlanning.Domain.Locations.Services;

namespace RoutePlanning.Application.Routing.Queries.RouteInfoQuery;

public sealed class RouteInfoQueryhandler : IQueryHandler<RouteInfoQuery, RouteInfo>
{
    private readonly IQueryable<Location> _locations;
    private readonly IShortestDistanceService _shortestDistanceService;

    public RouteInfoQueryhandler(IQueryable<Location> locations, IShortestDistanceService shortestDistanceService)
    {
        _locations = locations;
        _shortestDistanceService = shortestDistanceService;
    }

    public async Task<RouteInfo> Handle(RouteInfoQuery request, CancellationToken cancellationToken)
    {
        var source = await _locations.FirstAsync(l => l.Id == request.SourceId, cancellationToken);
        var destination = await _locations.FirstAsync(l => l.Id == request.DestinationId, cancellationToken);

        var distance = _shortestDistanceService.CalculateShortestDistance(source, destination, 0, new Parcel());

        throw new NotImplementedException();
    }
}

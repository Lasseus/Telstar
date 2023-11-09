using Netcompany.Net.DomainDrivenDesign.Services;

namespace RoutePlanning.Domain.Locations.Services;

public interface IShortestDistanceService : IDomainService
{
    public Task<int> CalculateShortestDistance(Location source, Location target, RouteType type, Parcel parcel);
}

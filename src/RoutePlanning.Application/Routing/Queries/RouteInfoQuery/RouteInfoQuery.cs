using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Locations;

namespace RoutePlanning.Application.Routing.Queries.RouteInfoQuery;

public sealed record RouteInfoQuery(Location.EntityId SourceId, Location.EntityId DestinationId, RouteType Type) : IQuery<RouteInfo>;

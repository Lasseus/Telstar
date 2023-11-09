using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Locations;

namespace RoutePlanning.Application.Locations.Queries.DeliveryInfo;

public sealed record DeliveryInfoQuery(string Source, string Destination, List<string> Type, double Weight) : IQuery<DeliveryInfo>;

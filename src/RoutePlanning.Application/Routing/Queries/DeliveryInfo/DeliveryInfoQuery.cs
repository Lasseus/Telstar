using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Locations;

namespace RoutePlanning.Application.Locations.Queries.DeliveryInfo;

public sealed record DeliveryInfoQuery(string Source, string Destination, List<string> Type, double Weight, int Height = 0, int Width = 0, int Length = 0) : IQuery<DeliveryInfo>;

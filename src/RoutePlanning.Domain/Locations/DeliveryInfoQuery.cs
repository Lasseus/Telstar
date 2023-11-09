using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Locations;

namespace RoutePlanning.Application.Locations.Queries.DeliveryInfo;

public sealed record DeliveryInfoQuery(string From, string To, List<string> Types, double Weight, int Height = 0, int Width = 0, int Length = 0) : IQuery<DeliveryInfo>;

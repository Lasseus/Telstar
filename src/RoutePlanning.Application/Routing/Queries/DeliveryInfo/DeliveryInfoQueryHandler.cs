using Microsoft.EntityFrameworkCore;
using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Locations;

namespace RoutePlanning.Application.Locations.Queries.DeliveryInfo;

public sealed class DeliveryInfoQueryhandler : IQueryHandler<DeliveryInfoQuery, DeliveryInfo>
{
    private readonly IQueryable<Connection> _connections;

    public DeliveryInfoQueryhandler(IQueryable<Connection> connections)
    {
        _connections = connections;
    }

    public async Task<DeliveryInfo> Handle(DeliveryInfoQuery query, CancellationToken cancellationToken)
    {
        if (query.Type.Contains(PacketInfo.Weapons) || query.Weight > 40)
        {
            return new DeliveryInfo(0, -1);
        }

        var connection = await _connections.FirstAsync(c => c.Source.Name == query.Source && c.Destination.Name == query.Destination, cancellationToken);

        var baseCost = connection.GetConnectionCostDollars();
        var totalCost = GetTotalSegmentCost(query, baseCost);
        var time = connection.GetConnectionTimeHours();

        return new DeliveryInfo(time, totalCost);
    }

    private static double GetTotalSegmentCost(DeliveryInfoQuery query, double baseCost)
    {
        var recordedDeliveryIncrease = query.Type.Contains(PacketInfo.RecordedDelivery) ? 10 : 0;
        var liveAnimalsIncrease = query.Type.Contains(PacketInfo.LiveAnimals) ? baseCost * PacketInfo.LiveAnimalsMultiplier : 0;
        var cautiousParcelsIncrease = query.Type.Contains(PacketInfo.CautiousParcels) ? baseCost * PacketInfo.CautiousParcelsMultiplier : 0;
        var refrigeratedGoodsIncrease = query.Type.Contains(PacketInfo.RefrigeratedGoods) ? baseCost * PacketInfo.RefrigeratedGoodsMultiplier : 0;

        return baseCost + recordedDeliveryIncrease + liveAnimalsIncrease + cautiousParcelsIncrease + refrigeratedGoodsIncrease;
    }
}

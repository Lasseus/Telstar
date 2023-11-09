using System.Diagnostics;
using Netcompany.Net.DomainDrivenDesign.Models;
using RoutePlanning.Application.Locations.Queries.DeliveryInfo;

namespace RoutePlanning.Domain.Locations;

[DebuggerDisplay("{Source} --{Distance}--> {Destination}")]
public sealed class Connection : Entity<Connection>
{
    public Connection(Location source, Location destination, Distance distance)
    {
        Source = source;
        Destination = destination;
        Distance = distance;
    }

    private Connection()
    {
        Source = null!;
        Destination = null!;
        Distance = null!;
    }

    public Location Source { get; private set; }

    public Location Destination { get; private set; }

    public Distance Distance { get; private set; }

    public double GetConnectionCostDollars()
    {
        return Distance * 3;
    }

    public int GetConnectionTimeHours()
    {
        return Distance * 4;
    }

    public static double GetTotalSegmentCost(DeliveryInfoQuery query, double baseCost)
    {
        var recordedDeliveryIncrease = query.Types.Contains(PacketInfo.RecordedDelivery) ? 10 : 0;
        var liveAnimalsIncrease = query.Types.Contains(PacketInfo.LiveAnimals) ? baseCost * PacketInfo.LiveAnimalsMultiplier : 0;
        var cautiousParcelsIncrease = query.Types.Contains(PacketInfo.CautiousParcels) ? baseCost * PacketInfo.CautiousParcelsMultiplier : 0;
        var refrigeratedGoodsIncrease = query.Types.Contains(PacketInfo.RefrigeratedGoods) ? baseCost * PacketInfo.RefrigeratedGoodsMultiplier : 0;

        return baseCost + recordedDeliveryIncrease + liveAnimalsIncrease + cautiousParcelsIncrease + refrigeratedGoodsIncrease;
    }
}

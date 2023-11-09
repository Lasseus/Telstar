namespace RoutePlanning.Application;
internal static class PacketInfo
{
    public static readonly string RecordedDelivery = "Recorded Delivery";
    public static readonly string Weapons = "Weapons";
    public static readonly string LiveAnimals = "Live Animals";
    public static readonly string CautiousParcels = "Cautious Parcels";
    public static readonly string RefrigeratedGoods = "Refrigerated Goods";

    public static readonly double RecordedDeliveryIncrease = 10;
    public static readonly double LiveAnimalsMultiplier = 1.5;
    public static readonly double CautiousParcelsMultiplier = 1.75;
    public static readonly double RefrigeratedGoodsMultiplier = 1.1;
}

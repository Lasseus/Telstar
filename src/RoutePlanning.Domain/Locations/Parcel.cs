using System.Diagnostics;
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Locations;

[DebuggerDisplay("{Value}")]
public sealed record Parcel : IValueObject
{
    public bool LiveAnimals { get; set; }
    public bool CautiousParcels { get; set; }
    public bool RefigeratedGoods { get; set; }
    public double Weight { get; set; }
    public bool Signed { get; set; }
    public List<string> GenerateTypesFromParcel()
    {
        var retList = new List<string>();
        if (LiveAnimals)
        {
            retList.Add(PacketInfo.LiveAnimals.ToString());

        }

        if (CautiousParcels)
        {
            retList.Add(PacketInfo.CautiousParcels.ToString());

        }

        if (RefigeratedGoods)
        {
            retList.Add(PacketInfo.RefrigeratedGoods.ToString());

        }

        if (Signed)
        {
            retList.Add(PacketInfo.RecordedDelivery.ToString());

        }

        return retList;
    }
}

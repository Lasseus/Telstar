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
}

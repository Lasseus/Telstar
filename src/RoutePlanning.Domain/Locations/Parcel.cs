using System.Diagnostics;
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Locations;

[DebuggerDisplay("{Value} km")]
public sealed record Parcel : IValueObject
{
    public string[]? Type { get; set; }
    public double Weight { get; set; }
    public bool Signed { get; set; }
}

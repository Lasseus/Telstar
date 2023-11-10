using System.Diagnostics;
using Netcompany.Net.DomainDrivenDesign.Models;

namespace RoutePlanning.Domain.Locations;

[DebuggerDisplay("{Value} km")]
public sealed record Route : IValueObject
{
    public float Cost { get; set; }
    public int Time { get; set; }
    public List<Location>? RouteList { get; set; }
}

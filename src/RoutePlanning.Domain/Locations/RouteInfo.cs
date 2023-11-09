namespace RoutePlanning.Domain.Locations;

public enum RouteType
{
    Cheapest,
    Fastest,
    Best
}
public class RouteInfo
{
    public int Time { get; set; }
    public double Price { get; set; }

    public IEnumerable<Connection> Path { get; set; } = null!;
}

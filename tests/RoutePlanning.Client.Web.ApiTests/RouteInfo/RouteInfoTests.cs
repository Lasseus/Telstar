using System.Net;
using System.Text;
using System.Text.Json;
using Netcompany.Net.Testing.Api;
using RoutePlanning.Application.Locations.Queries.DeliveryInfo;
using RoutePlanning.Client.Web.Api;

namespace RoutePlanning.Client.Web.ApiTests.Authentication;

public class RouteInfoTests : IClassFixture<RoutePlanningApplicationFactory>
{
    private readonly RoutePlanningApplicationFactory _factory;
    private readonly HttpClient _client;

    public RouteInfoTests(RoutePlanningApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.HttpClient;
    }

    [Fact]
    public async Task GetDeliveryInfoTest()
    {
        // Arrange
        var url = _factory.GetRoute<Program, RoutesController>(x => x.GetDeliveryInfo);

        // Act
        var deliveryInfoQuery = new DeliveryInfoQuery("TANGER", "TUNIS", new List<string>() { "Recorded Delivery"}, 23);
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Content = new StringContent(JsonSerializer.Serialize(deliveryInfoQuery), Encoding.UTF8, "application/json");
        request.Headers.Add("Token", "TheSecretApiToken");

        var response = await _client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var results = JsonSerializer.Deserialize<DeliveryInfo>(content, options);
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}

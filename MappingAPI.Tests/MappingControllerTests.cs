using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Testing;

public class MappingControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public MappingControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ProcessMapping_ReturnsMappedData()
    {
        var request = new
        {
            InputData = new Dictionary<string, string> { { "Key1", "Value1" }, { "Key2", "Value2" } },
            MappingData = new Dictionary<string, string> { { "Key1", "MappedKey1" } }
        };

        var response = await _client.PostAsJsonAsync("/api/mapping/process", request);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        Assert.Contains("MappedKey1", result);
    }
}
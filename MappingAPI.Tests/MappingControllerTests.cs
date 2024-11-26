using Xunit;
using Microsoft.EntityFrameworkCore;
using MappingAPI.Controllers;
using MappingAPI.Data;
using MappingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MappingAPI.Tests
{
    public class MappingControllerTests
    {
        [Fact]
        public void ProcessMapping_ValidRequest_ReturnsMappedOutput()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_ValidRequest")
                .Options;

            using var context = new AppDbContext(options);
            var controller = new MappingController(context);

            var request = new MappingRequest
            {
                InputData = new Dictionary<string, string>
                {
                    { "key1", "value1" },
                    { "key2", "value2" }
                },
                MappingData = new Dictionary<string, string>
                {
                    { "key1", "mappedKey1" }
                }
            };

            // Act
            var result = controller.ProcessMapping(request) as OkObjectResult;

            // Assert
            Assert.NotNull(result);

            // Deserialize the response to match the JSON structure
            var json = JsonSerializer.Serialize(result.Value);
            var outputData = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json);
            Assert.NotNull(outputData);

            // Ensure the key matches the API's response
            Assert.True(outputData.ContainsKey("OutputData"), "Key 'OutputData' not found in API response.");
            var output = outputData["OutputData"];
            Assert.Equal("value1", output["mappedKey1"]);
            Assert.Equal("value2", output["key2"]);
        }

        [Fact]
        public void ProcessMapping_EmptyMappingData_ReturnsOkWithUnmappedInput()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_EmptyMapping")
                .Options;

            using var context = new AppDbContext(options);
            var controller = new MappingController(context);

            var request = new MappingRequest
            {
                InputData = new Dictionary<string, string>
                {
                    { "key1", "value1" },
                    { "key2", "value2" }
                },
                MappingData = new Dictionary<string, string>() // Empty mapping
            };

            // Act
            var result = controller.ProcessMapping(request) as OkObjectResult;

            // Assert
            Assert.NotNull(result);

            // Deserialize the response to match the JSON structure
            var json = JsonSerializer.Serialize(result.Value);
            var outputData = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json);
            Assert.NotNull(outputData);

            // Ensure the key matches the API's response
            Assert.True(outputData.ContainsKey("OutputData"), "Key 'OutputData' not found in API response.");
            var output = outputData["OutputData"];
            Assert.Equal("value1", output["key1"]);
            Assert.Equal("value2", output["key2"]);
        }
    }
}
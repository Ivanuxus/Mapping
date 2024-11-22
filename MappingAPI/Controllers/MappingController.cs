using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class MappingController : ControllerBase
{
    [HttpPost("process")]
    public IActionResult ProcessMapping([FromBody] MappingRequest request)
    {
        // Проверяем входные данные
        if (request == null || request.InputData == null || request.MappingData == null)
        {
            return BadRequest("Invalid input data.");
        }

        // Выполняем маппинг
        var outputData = ApplyMapping(request.InputData, request.MappingData);

        // Возвращаем результат
        return Ok(new { OutputData = outputData });
    }

    // Метод для применения маппинга
    private Dictionary<string, string> ApplyMapping(Dictionary<string, string> inputData, Dictionary<string, string> mappingData)
    {
        var result = new Dictionary<string, string>();

        foreach (var item in inputData)
        {
            if (mappingData.TryGetValue(item.Key, out var mappedKey))
            {
                result[mappedKey] = item.Value;
            }
            else
            {
                result[item.Key] = item.Value; // Если маппинг отсутствует, оставляем оригинальный ключ
            }
        }

        return result;
    }
}

// Класс для обработки входных данных
public class MappingRequest
{
    public Dictionary<string, string> InputData { get; set; }
    public Dictionary<string, string> MappingData { get; set; }
}
using Microsoft.AspNetCore.Mvc;
using MappingAPI.Data;
using MappingAPI.Models;

namespace MappingAPI.Controllers
{
    [ApiController]
[Route("api/[controller]")]
public class MappingController : ControllerBase
{
    private readonly AppDbContext _context;

    public MappingController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("process")]
    public IActionResult ProcessMapping([FromBody] MappingRequest request)
    {
        if (request == null || request.InputData == null || request.MappingData == null)
        {
            return BadRequest("Invalid input data.");
        }

        // Сохраняем маппинг в базу данных
        foreach (var mapping in request.MappingData)
        {
            _context.MappingData.Add(new MappingData
            {
                OriginalKey = mapping.Key,
                MappedKey = mapping.Value
            });
        }

        // Сохраняем входные данные в базу данных
        foreach (var input in request.InputData)
        {
            _context.InputData.Add(new InputData
            {
                Key = input.Key,
                Value = input.Value
            });
        }

        // Сохраняем изменения в базу
        _context.SaveChanges();

        // Применяем маппинг
        var outputData = ApplyMapping(request.InputData, request.MappingData);

        // Сохраняем результат маппинга в базу данных
        foreach (var item in outputData)
        {
            _context.MappedOutputData.Add(new MappedOutputData
            {
                Key = item.Key,
                MappedKey = item.Value,
                Value = request.InputData.ContainsKey(item.Key) ? request.InputData[item.Key] : "Default Value" // Если ключа нет в InputData, указываем дефолтное значение
            });
        }

        // Сохраняем результат маппинга в базе данных
        _context.SaveChanges();

        return Ok(new { OutputData = outputData });
    }

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
                result[item.Key] = item.Value;
            }
        }

        return result;
    }
}
}
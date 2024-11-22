using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Использование: app.exe <путь_к_входному_файлу> <путь_к_файлу_маппинга>");
            return;
        }

        string inputFilePath = args[0];
        string mappingFilePath = args[1];
        string outputFilePath = "output_OM_data.csv";

        try
        {
            // Чтение данных из входного файла
            var inputData = LoadData(inputFilePath);

            // Чтение маппинга
            var mappingData = LoadMapping(mappingFilePath);

            // Обработка данных и применение маппинга
            var outputData = ApplyMapping(inputData, mappingData);

            // Запись результатов в выходной файл
            SaveData(outputFilePath, outputData);

            Console.WriteLine($"Данные успешно обработаны и сохранены в {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }

    public static Dictionary<string, string> LoadData(string filePath)
    {
        var data = new Dictionary<string, string>();

        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split(',');
            if (parts.Length >= 2)
            {
                string key = parts[0].Trim();
                string value = parts[1].Trim();
                data[key] = value;
            }
        }

        return data;
    }

    public static Dictionary<string, string> LoadMapping(string filePath)
    {
        var mapping = new Dictionary<string, string>();

        if (File.Exists(filePath))
        {
            foreach (var line in File.ReadLines(filePath))
            {
                var parts = line.Split(',');
                if (parts.Length >= 2)
                {
                    string externalKey = parts[0].Trim();
                    string omKey = parts[1].Trim();
                    mapping[externalKey] = omKey;
                }
            }
        }
        else
        {
            Console.WriteLine("Файл маппинга отсутствует, используется пустой маппинг.");
        }

        return mapping;
    }

    public static Dictionary<string, string> ApplyMapping(Dictionary<string, string> inputData, Dictionary<string, string> mappingData)
    {
        var resultData = new Dictionary<string, string>();

        foreach (var entry in inputData)
        {
            string externalKey = entry.Key;
            string externalValue = entry.Value;

            // Проверка, есть ли соответствие в маппинге
            if (mappingData.TryGetValue(externalKey, out string omKey))
            {
                resultData[omKey] = externalValue;
            }
            else
            {
                // Если маппинга нет, оставляем внешний ключ без изменений
                resultData[externalKey] = externalValue;
            }
        }

        return resultData;
    }

    public static void SaveData(string filePath, Dictionary<string, string> data)
    {
        using (var writer = new StreamWriter(filePath))
        {
            foreach (var entry in data)
            {
                writer.WriteLine($"{entry.Key},{entry.Value}");
            }
        }
    }
}
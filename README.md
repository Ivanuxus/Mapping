# Mapping
Запуск dotnet run
Запуск после билда Mappingg.exe input_data.csv mapping_data.csv
Запуск тестов основного приложения dotnet test Mappingg.Tests
Запуск тестов API dotnet test MappingAPI.Tests
Пример содержания файлов, также они уже есть в корневой папке.
input_data.csv:
ext_id1,Value1
ext_id2,Value2
ext_id3,Value3
ext_id4,Value4

mapping_data.csv:
ext_id1,om_idA
ext_id2,om_idB
ext_id4,om_idD

Проверка запросов (Порт указать свой):
dotnet run --project MappingAPI
curl -X POST http://localhost:5220/api/mapping/process \
-H "Content-Type: application/json" \
-d '{
    "InputData": {
        "Key1": "Value1",
        "Key2": "Value2"
    },
    "MappingData": {
        "Key1": "MappedKey1"
    }
}'

# Mapping
Запуск dotnet run
Запуск тестов API: dotnet test
Пример содержания файлов, также они уже есть в корневой папке.
postman:
пример Post-запроса:http://localhost:5220/api/Mapping/process
    {
    "InputData": {
        "капуста": "100 руб",
        "помидор": "80 руб",
        "картофель": "50 руб"
    },
    "MappingData": {
        "капуста": "Cabbage",
        "помидор": "Tomato",
        "картофель": "Potato"
    }
    }

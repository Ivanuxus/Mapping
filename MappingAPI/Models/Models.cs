namespace MappingAPI.Models
{
    public class MappingData
    {
        public int Id { get; set; }
        public string OriginalKey { get; set; } = string.Empty;
        public string MappedKey { get; set; } = string.Empty;
    }

    public class InputData
    {
        public int Id { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
    public class MappingRequest
    {
        public Dictionary<string, string>? InputData { get; set; }
        public Dictionary<string, string>? MappingData { get; set; }
    }
    public class MappedOutputData
{
    public int Id { get; set; }
    public string Key { get; set; }
    public string MappedKey { get; set; }
    public string Value { get; set; }
}
}
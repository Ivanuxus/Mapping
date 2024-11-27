using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace MyConsoleApp.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void LoadData_ShouldReturnCorrectDictionary()
        {
            // Arrange
            var testFilePath = "test_input.csv";
            File.WriteAllLines(testFilePath, new string[] { "ext_id1,Value1", "ext_id2,Value2" });

            // Act
            var result = Program.LoadData(testFilePath);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Value1", result["ext_id1"]);
            Assert.Equal("Value2", result["ext_id2"]);

            // Cleanup
            File.Delete(testFilePath);
        }

        [Fact]
        public void LoadMapping_ShouldReturnCorrectDictionary()
        {
            // Arrange
            var testFilePath = "test_mapping.csv";
            File.WriteAllLines(testFilePath, new string[] { "ext_id1,om_idA", "ext_id2,om_idB" });

            // Act
            var result = Program.LoadMapping(testFilePath);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("om_idA", result["ext_id1"]);
            Assert.Equal("om_idB", result["ext_id2"]);

            // Cleanup
            File.Delete(testFilePath);
        }

        [Fact]
        public void ApplyMapping_ShouldMapKeysCorrectly()
        {
            // Arrange
            var inputData = new Dictionary<string, string> { { "ext_id1", "Value1" }, { "ext_id3", "Value3" } };
            var mappingData = new Dictionary<string, string> { { "ext_id1", "om_idA" } };

            // Act
            var result = Program.ApplyMapping(inputData, mappingData);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Value1", result["om_idA"]);  // Ключ изменен на om_idA
            Assert.Equal("Value3", result["ext_id3"]);  // Ключ остался как ext_id3
        }

        [Fact]
        public void SaveData_ShouldWriteCorrectlyToFile()
        {
            // Arrange
            var testFilePath = "test_output.csv";
            var dataToSave = new Dictionary<string, string> { { "om_idA", "Value1" }, { "ext_id3", "Value3" } };

            // Act
            Program.SaveData(testFilePath, dataToSave);

            // Assert
            var savedData = File.ReadAllLines(testFilePath);
            Assert.Equal("om_idA,Value1", savedData[0]);
            Assert.Equal("ext_id3,Value3", savedData[1]);

            // Cleanup
            File.Delete(testFilePath);
        }
    }
}

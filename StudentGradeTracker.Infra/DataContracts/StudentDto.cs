using System.Text.Json.Serialization;

namespace StudentGradeTracker.Infra.DataContracts;

public class StudentDto
{
    [JsonPropertyName("idCard")]
    public string IdCard { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
}

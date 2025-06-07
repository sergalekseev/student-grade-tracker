using System.Text.Json;
using System.Text.Json.Serialization;

namespace StudentGradeTracker.Infra.DataContracts;

public class NotificationHubMethods
{
    public const string NotifyNewStudent = "NotifyNewStudent";
}

public class NotificationHubMessages
{
    public const string NewStudentUpdate = "NewStudentUpdate";
}

// { "name": "" }

public class Message
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("payload"), 
     JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public virtual object Payload { get; set; }
}

public class IncomingMessage : Message, IJsonOnDeserialized
{
    [JsonIgnore]
    public override object Payload { get; set; }


    [JsonPropertyName("payload")]
    public JsonElement RawPayload { get; set; }

    public void OnDeserialized()
    {
        Payload = Name switch
        {
            NotificationHubMessages.NewStudentUpdate =>
                JsonSerializer.Deserialize<StudentDto>(RawPayload.GetRawText()),
            _ => null
        };
    }
}

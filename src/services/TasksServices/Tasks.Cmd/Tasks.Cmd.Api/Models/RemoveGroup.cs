using System.Text.Json.Serialization;
using Tasks.Common.Converter;

namespace Tasks.Cmd.Api.Models;

public class RemoveGroup
{
    public Guid Id { get; set; }
    [JsonConverter(typeof(TupleConverter))]
    public List<(Guid taskId, Guid authorId)> RemovedTasks { get; set; } = new();
}
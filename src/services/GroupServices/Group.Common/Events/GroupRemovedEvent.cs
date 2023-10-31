using System.Text.Json.Serialization;
using Group.Common.Converter;

namespace Group.Common.Events;

public class GroupRemovedEvent:BaseEvent
{

    [JsonConverter(typeof(TupleConverter))]
    public List<(Guid taskId, Guid authorId)> RemovedTasks { get; set; }
    public GroupRemovedEvent()
        :base(nameof(GroupRemovedEvent))
    {
        
    }
}
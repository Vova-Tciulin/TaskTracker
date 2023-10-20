using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Tasks.Common.Events;

namespace Tasks.Cmd.Domain.Models;

public class EventModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public Guid AggregateId { get; set; }
    public int Version { get; set; }
    public string AggregateType { get; set; }
    public DateTime EventsCreated { get; set; }
    public string EventType { get; set; }
    public BaseEvent Event { get; set; }
}
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using Tasks.Common.Events;

namespace Tasks.Cmd.Infrastructure;

public static class MongoConfig
{
    public static IServiceCollection AddMongoMap(this IServiceCollection services)
    {
        
        BsonClassMap.RegisterClassMap<BaseEvent>(cm =>
        {
            cm.AutoMap();
            cm.MapMember(u => u.Type).SetElementName("type");
            cm.SetDiscriminator("type");
            cm.SetIsRootClass(true);
            cm.AddKnownType(typeof(TaskCompletedEvent));
            cm.AddKnownType(typeof(TaskCreatedEvent));
            cm.AddKnownType(typeof(TaskRemovedEvent));
            cm.AddKnownType(typeof(TaskTakenOnWorkEvent));
            cm.AddKnownType(typeof(TaskUpdatedDeadlineEvent));
            cm.AddKnownType(typeof(TaskUpdatedTaskEvent));
            cm.AddKnownType(typeof(TaskUpdatedTitleEvent));
            cm.AddKnownType(typeof(TaskReturnToNewState));
        });

        BsonClassMap.RegisterClassMap<TaskCompletedEvent>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator(nameof(TaskCompletedEvent));
        });
        
        BsonClassMap.RegisterClassMap<TaskCreatedEvent>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator(nameof(TaskCreatedEvent));
        });
        
        BsonClassMap.RegisterClassMap<TaskRemovedEvent>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator(nameof(TaskRemovedEvent));
        });

        BsonClassMap.RegisterClassMap<TaskTakenOnWorkEvent>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator(nameof(TaskTakenOnWorkEvent));
        });
        
        BsonClassMap.RegisterClassMap<TaskUpdatedDeadlineEvent>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator(nameof(TaskUpdatedDeadlineEvent));
        });
        
        BsonClassMap.RegisterClassMap<TaskUpdatedTaskEvent>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator(nameof(TaskUpdatedTaskEvent));
        });
        
        BsonClassMap.RegisterClassMap<TaskUpdatedTitleEvent>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator(nameof(TaskUpdatedTitleEvent));
        });
        
        BsonClassMap.RegisterClassMap<TaskReturnToNewState>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator(nameof(TaskReturnToNewState));
        });
        
        return services;
    }
}
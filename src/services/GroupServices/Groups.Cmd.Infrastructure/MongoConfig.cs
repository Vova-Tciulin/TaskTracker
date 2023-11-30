using Group.Common.Events;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;

namespace Groups.Cmd.Infrastructure;

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
            cm.AddKnownType(typeof(GroupCreatedEvent));
            cm.AddKnownType(typeof(GroupRemovedEvent));
            cm.AddKnownType(typeof(GroupTaskAddedEvent));
            cm.AddKnownType(typeof(GroupTaskRemovedEvent));
            cm.AddKnownType(typeof(GroupUserAddedEvent));
            cm.AddKnownType(typeof(GroupUserRemovedEvent));
        });

        BsonClassMap.RegisterClassMap<GroupCreatedEvent>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator(nameof(GroupCreatedEvent));
        });
        
        BsonClassMap.RegisterClassMap<GroupRemovedEvent>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator(nameof(GroupRemovedEvent));
        });
        
        BsonClassMap.RegisterClassMap<GroupTaskAddedEvent>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator(nameof(GroupTaskAddedEvent));
        });

        BsonClassMap.RegisterClassMap<GroupTaskRemovedEvent>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator(nameof(GroupTaskRemovedEvent));
        });
        
        BsonClassMap.RegisterClassMap<GroupUserAddedEvent>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator(nameof(GroupUserAddedEvent));
        });
        
        BsonClassMap.RegisterClassMap<GroupUserRemovedEvent>(cm =>
        {
            cm.AutoMap();
            cm.SetDiscriminator(nameof(GroupUserRemovedEvent));
        });
        
        return services;
    }
}
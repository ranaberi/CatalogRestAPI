
using Catalog.Repositories;
using Catalog.Settings;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Catalog
{
    public static class RegisterServices
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            //to serialize any Guid or DateTimeOffset into a string in the database
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Since .net removes the async suffix at run time, which is unwanted behabior
            builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);

            //construsting this dependency by regestering it as a singleton 
            //so that only one copy of the instance of InMemItemsRepository across the entire lifetime
            //will be constructed and reused whenever it is needed
            builder.Services.AddSingleton<IItemsRepository, InMemItemsRepository>();
            builder.Services.AddSingleton<IItemsRepository, MongoDbItemsRepository>();
            var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
            builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                
                return new MongoClient(mongoDbSettings.ConnectionString);

            });
            //health check to see if the database is reachable
            builder.Services.AddHealthChecks()
                            .AddMongoDb(mongoDbSettings.ConnectionString, name:"mongodb", timeout: TimeSpan.FromSeconds(3), tags: new[]{"ready"});

            builder.Services.AddHttpsRedirection(options =>
            {
                options.HttpsPort =5001;
            });
            
        }
    }
}
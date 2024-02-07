
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
            builder.Services.AddControllers();

            //construsting this dependency by regestering it as a singleton 
            //so that only one copy of the instance of InMemItemsRepository across the entire lifetime
            //will be constructed and reused whenever it is needed
            builder.Services.AddSingleton<IItemsRepository, InMemItemsRepository>();
            builder.Services.AddSingleton<IItemsRepository, MongoDbItemsRepository>();
            builder.Services.AddSingleton<IMongoClient>(ServiceProvider =>
            {
                var settings = builder.Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                return new MongoClient(settings.ConnectionString);

            });
            
        }
    }
}
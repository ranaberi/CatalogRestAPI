using Catalog.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

//construsting this dependency by regestering it as a singleton 
//so that only one copy of the instance of InMemItemsRepository across the entire lifetime
//will be constructed and reused whenever it is needed
builder.Services.AddSingleton<IItemsRepository, InMemItemsRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
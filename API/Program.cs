var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionstr = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlite(connectionstr);
});


var app = builder.Build();

using var scopes = app.Services.CreateScope();
var context = scopes.ServiceProvider.GetRequiredService<StoreContext>();
var logger = scopes.ServiceProvider.GetRequiredService<ILogger<Program>>();

try
{
    context.Database.Migrate();
    DBInitializer.Initialise(context);
}
catch (Exception ex)
{
    logger.LogError(ex, "Problem Migration data");
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); //for development purpose only, 

app.UseAuthorization();

app.MapControllers();

app.Run();

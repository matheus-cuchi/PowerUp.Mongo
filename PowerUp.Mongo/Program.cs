using PowerUp.Mongo.Modulos;

var builder = WebApplication.CreateBuilder(args);

builder
   .WebHost
   .ConfigureKestrel(kestrel => kestrel.ListenAnyIP(9090))
   .UseKestrel();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AdicionarMongo(builder.Configuration);

var app = builder.Build();

app.UseCors(
    x => x.AllowAnyHeader()
       .AllowAnyMethod()
       .AllowAnyOrigin());

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
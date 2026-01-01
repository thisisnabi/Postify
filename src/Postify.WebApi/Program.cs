var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediaModule(builder.Configuration);
builder.Services.AddNotifyModule(builder.Configuration);
builder.Services.AddProfileModule(builder.Configuration);
builder.Services.AddProofModule(builder.Configuration);
builder.Services.AddShortakModule(builder.Configuration);

builder.Services.AddControllers();

builder.Services.Configure<AppSettings>(builder.Configuration);

var app = builder.Build();
 
app.MapControllers();

app.Run();
 
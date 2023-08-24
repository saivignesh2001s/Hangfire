using Hangfire;
using Unconnectedwebapi.Models;
using Unconnectedwebapi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(cfg =>
{
    cfg.Filters.Add(typeof(ExceptionFilter));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHangfire(config => config
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("default")));
builder.Services.AddHangfireServer();
builder.Services.AddTransient<IUsermethods, Usermethods>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseHangfireDashboard();
app.MapHangfireDashboard();
RecurringJob.AddOrUpdate<IUsermethods>(x => x.GetUsers(), cronExpression: "0 * * ? * *");



app.Run();

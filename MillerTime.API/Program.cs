using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using MillerTime.API.Repositories;
using MillerTime.Services;
using MillerTime.Services.Interfaces;
using MillerTime.DAL.Context;
using MillerTime.DAL.Helpers;
using MillerTime.DAL.Repositories;
using MillerTime.DAL.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MillerTimeAPI", Version = "v1" });
});
builder.Services.AddControllers();
builder.Services.AddTransient<IVideoService, VideoService>();
builder.Services.AddTransient<IVideoRepository, VideoRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddDbContext<MillerTimeContext>(options =>
{
    options.UseSqlServer(builder.Configuration["AZURE_SQL_CONNECTIONSTRING"], sqlOptions =>
    {
        sqlOptions.CommandTimeout(90);
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("DEFAULT", builder =>
    {
        builder
            .WithOrigins("http://localhost:3000", "https://wes-mitchell.github.io")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await DockerInitializer.StartDocker();
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<MillerTimeContext>();
        await SeedData.Seed(context);
    }
}

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MillerTimeAPI v1"));

app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("DEFAULT");

//app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

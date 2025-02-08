
using Clean.Architecture.Web.Configurations;
using HackernNews.Api.Configurations;

namespace HackernNews.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatr();
            builder.Services.AddLayers(builder.Configuration);
            builder.Services.AddCorsPolicy();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseCorsPolicy();
            app.UseAppMiddleware();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

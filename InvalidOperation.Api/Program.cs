using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using InvalidOperation.Api.Data;
using Npgsql;

namespace InvalidOperation.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("postgres"));
            dataSourceBuilder.EnableDynamicJson();
            dataSourceBuilder.EnableRecordsAsTuples();
            dataSourceBuilder.EnableUnmappedTypes();
            var dataSource = dataSourceBuilder.Build();
            builder.Services.AddDbContext<InvalidOperationApiContext>(options =>
                options.UseNpgsql(dataSource ?? throw new InvalidOperationException("Connection string 'InvalidOperationApiContext' not found.")));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
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

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<InvalidOperationApiContext>();
                db.Database.Migrate();
                using var connection = (NpgsqlConnection)db.Database.GetDbConnection();
                connection.Open();
                connection.ReloadTypes();
            }

            app.Run();
        }
    }
}

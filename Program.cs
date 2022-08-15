
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;
using single_api.Models;
using Microsoft.OpenApi.Models;


internal class Program
{
    private static void Main(string[] args)
    {

        var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        var builder = WebApplication.CreateBuilder(args);

         builder.Services.AddCors(options =>
                                    {
                                        options.AddPolicy(name: MyAllowSpecificOrigins,
                                                            policy  =>
                                                            {
                                                                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                                                            });
                                    });

        builder.WebHost.UseUrls("http://*:80;http://*:5110");

        builder.WebHost.UseSetting("https_port", "5001");

        builder.Services.AddDbContext<MovieContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MiniKubeDb")));
        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
                                        {
                                            options.SwaggerDoc("v1", new OpenApiInfo
                                            {
                                                Version = "v1",
                                                Title = "ToDo API",
                                                Description = "An ASP.NET Core Web API for managing ToDo items",
                                                TermsOfService = new Uri("https://example.com/terms"),
                                                Contact = new OpenApiContact
                                                {
                                                    Name = "Example Contact",
                                                    Url = new Uri("https://example.com/contact")
                                                },
                                                License = new OpenApiLicense
                                                {
                                                    Name = "Example License",
                                                    Url = new Uri("https://example.com/license")
                                                }
                                            });
                                        });

        builder.Services.AddTransient<IMemoryLoad<Movie>, MemoryLoad>();

       

        CreateEntityDataBase(builder.Services);

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseSwagger();
        app.UseSwaggerUI(options =>
                                {
                                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                                    options.RoutePrefix = string.Empty;
                                });


       // app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseCors(MyAllowSpecificOrigins);

        app.Run();
    }


    private static void CreateEntityDataBase(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var movieContext = serviceProvider.GetService<MovieContext>();
        movieContext?.Database.EnsureDeleted();
        movieContext?.Database.EnsureCreated();
        movieContext?.Database.Migrate();
        ContextLoader.Save(movieContext);
    }
}
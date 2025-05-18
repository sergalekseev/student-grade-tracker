using StudentGradeTrackerServer.Services;

namespace StudentGradeTrackerServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Services
            //builder.Services.AddSingleton<IStudentsStore, StudentsStore>();
            //builder.Services.AddSingleton<ISubjectsStore, SubjectsStore>();
            //builder.Services.AddSingleton<IGradesStore, GradesStore>();


            builder.Services.AddScoped<IStudentsStore, StudentsDbStore>();
            builder.Services.AddScoped<ISubjectsStore, SubjectsDbStore>();
            builder.Services.AddScoped<IGradesStore, GradesDbStore>();

            // Add services to the container.
            builder.Services.AddControllers();

            // swagger
            builder.Services.AddSwaggerGen(options =>
            {
                // configure if needed
            });

            // database
            builder.Services.AddDbContext<StudentsTrackerDbContext>();

            var app = builder.Build();

            // swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}

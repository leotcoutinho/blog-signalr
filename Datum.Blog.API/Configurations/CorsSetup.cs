namespace Datum.Blog.API.Configurations
{
    public static class CorsSetup
    {
        public static void AddCorsSetup(this IServiceCollection service)
        {
            // definindo política de cors
            service.AddCors(
                s => s.AddPolicy("DefaultPolicy",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                }));

        }

        public static void UseCorsSetup(this IApplicationBuilder app)
        {
            app.UseCors("DefaultPolicy");
        }
    }
}

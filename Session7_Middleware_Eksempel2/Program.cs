namespace Session7_Middleware_Eksempel2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // tilføjer RoutingMiddleware 
            app.UseRouting();

            String name = "Michael";
            // definer endpoint for request på "/hello" og hvad der skal returneres
            app.MapGet("/hello", () => $"Hello {name}");

            app.UseAuthorization();
            app.MapRazorPages();
            app.Run();
        }
    }
}

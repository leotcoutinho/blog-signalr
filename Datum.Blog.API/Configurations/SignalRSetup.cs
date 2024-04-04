namespace Datum.Blog.API.Configurations
{
    public static class SignalRSetup
    {
        public static void AddSignalRSetup(this IServiceCollection service)
        {
            service.AddSignalR();
        }
    }
}

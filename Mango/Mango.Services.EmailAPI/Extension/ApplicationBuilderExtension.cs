using Mango.Services.EmailAPI.Messages;

namespace Mango.Services.EmailAPI.Extension
{
    public static class ApplicationBuilderExtension
    {
        private static IAzureServiceBusConsumer ServiceBusConsumer { get; set; }
        public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder app)
        {
            ServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
            var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostApplicationLife.ApplicationStarted.Register(OnStart);
            hostApplicationLife.ApplicationStopping.Register(onStop);

            return app;
        }

        private static void OnStart() { ServiceBusConsumer.Start(); }
        private static void onStop() {  ServiceBusConsumer.Stop(); }
    }
}

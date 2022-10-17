
namespace Demo.Services.BackGroundService
{
    public class BackGround : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public BackGround(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = scopeFactory.CreateScope())
            {

            }
            while (!stoppingToken.IsCancellationRequested)
            {

                Console.WriteLine("TESTE");

                await Task.Delay(120000, stoppingToken);//3.600.000 milisegundos equivalem a 1 hora.. 60 000 = 1 min
            }

        }


    }
}

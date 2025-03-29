using System.Diagnostics;
using FipeConsumer.Application.Services;

namespace FipeConsumer.Jobs
{
    public class FipeUpsertJob(FipeDataSyncService dataSyncService) : IFipeUpsertJob
    {
        private readonly FipeDataSyncService _dataSyncService = dataSyncService;

        public async Task ExecuteAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            Console.WriteLine($"********** Job started {DateTime.Now:dd/MM/yyyy HH:mm:ss}");

            try
            {
                await _dataSyncService.AcquireFipeDataAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"********** Error when executing job: {ex.Message}");
            }
            finally
            {
                stopwatch.Stop();
                Console.WriteLine($"********** Job execution ended, duration: {stopwatch.ElapsedMilliseconds / 1000} seconds");
            }
        }
    }
}
using System.Diagnostics;
using FipeConsumer.Application.Services;
using FipeConsumer.Domain.Enums;

namespace FipeConsumer.Jobs
{
    public class FipeUpsertJob(FipeDataSyncService dataSyncService, WorkerService workerService) : IFipeUpsertJob
    {
        private readonly FipeDataSyncService _dataSyncService = dataSyncService;
        private readonly WorkerService _workerService = workerService;

        public async Task ExecuteAsync()
        {
            if (await _workerService.AddJobAsync(JobType.SyncFipeData))
            {
                var stopwatch = Stopwatch.StartNew();
                Console.WriteLine($"********** Job started {DateTime.Now:dd/MM/yyyy HH:mm:ss}");

                try
                {
                    await _dataSyncService.AcquireFipeDataAsync();
                }
                catch (Exception ex)
                {
                    await _workerService.UpdateJobStatusAsync(JobStatus.Failed, ex.Message);
                    Console.WriteLine($"********** Error when executing job: {ex.Message}");
                }
                finally
                {
                    stopwatch.Stop();
                    var jobDuration = $"{stopwatch.ElapsedMilliseconds / 1000} seconds";
                    await _workerService.UpdateJobStatusAsync(JobStatus.Finished, jobDuration: $"{jobDuration} seconds");
                    Console.WriteLine($"********** Job execution ended, duration: {jobDuration} seconds");
                }
            }
        }
    }
}
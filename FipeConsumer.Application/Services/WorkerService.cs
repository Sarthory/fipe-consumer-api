using FipeConsumer.Domain.Enums;
using FipeConsumer.Domain.Interfaces;

namespace FipeConsumer.Application.Services
{
    public class WorkerService(IWorkerRepository workerRepository)
    {
        private readonly IWorkerRepository _workerRepository = workerRepository;

        public async Task<bool> AddJobAsync(JobType jobType)
        {
            return await _workerRepository.AddJobAsync(jobType);
        }

        public async Task UpdateJobStatusAsync(JobStatus status, string? jobDuration = null, string? errorMessage = null)
        {
            await _workerRepository.UpdateJobStatusAsync(status, jobDuration, errorMessage);
        }
    }

}

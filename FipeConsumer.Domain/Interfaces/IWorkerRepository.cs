using FipeConsumer.Domain.Enums;

namespace FipeConsumer.Domain.Interfaces
{
    public interface IWorkerRepository
    {
        Task<bool> AddJobAsync(JobType jobType);
        Task UpdateJobStatusAsync(JobStatus status, string? jobDuration, string? errorMessage = null);
    }
}
using Microsoft.EntityFrameworkCore;
using FipeConsumer.Domain.Entities;
using FipeConsumer.Domain.Interfaces;
using FipeConsumer.Infrastructure.Data;
using FipeConsumer.Domain.Enums;

namespace FipeConsumer.Infrastructure.Repositories
{
    public class Workerrepository(FipeConsumerDbContext context) : IWorkerRepository
    {
        private readonly FipeConsumerDbContext _context = context;

        public async Task<bool> AddJobAsync(JobType jobType)
        {
            Job? lastJob = await _context.Jobs.OrderBy(j => j.JobId).LastOrDefaultAsync();

            if (lastJob == null || lastJob.Status != JobStatus.Running)
            {
                var newJob = new Job
                {
                    Type = jobType,
                    Status = JobStatus.Running,
                    CreatedAt = DateTime.Now,
                };

                await _context.Jobs.AddAsync(newJob);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task UpdateJobStatusAsync(JobStatus status, string? jobDuration, string? errorMessage)
        {
            Job? job = await _context.Jobs.OrderBy(j => j.JobId).LastOrDefaultAsync();

            if (job != null)
            {
                job.Status = status;
                job.UpdatedAt = DateTime.Now;

                if (status == JobStatus.Finished)
                {
                    job.FinishedAt = DateTime.Now;
                    job.JobDuration = jobDuration;
                }

                if (errorMessage != string.Empty)
                {
                    job.ErrorMessage = errorMessage;
                }

                _context.Jobs.Update(job);
                await _context.SaveChangesAsync();
            }
        }
    }
}

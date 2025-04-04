using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FipeConsumer.Domain.Enums;
using FipeConsumer.Domain.Exceptions;

namespace FipeConsumer.Domain.Entities
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobId { get; private set; }

        public JobType Type { get; private set; }

        public JobStatus Status { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        public DateTime? FinishedAt { get; private set; }

        public string? JobDuration { get; private set; }

        public string? ErrorMessage { get; private set; }

        private Job() { }

        public Job(JobType type)
        {
            SetType(type);
            SetStatus(JobStatus.Running);
            SetCreatedAt(DateTime.Now);
        }

        public void SetType(JobType type)
        {
            Type = type;
        }

        public void SetStatus(JobStatus status)
        {
            Status = status;
            SetUpdatedAt(DateTime.Now);
        }

        public void SetCreatedAt(DateTime createdAt)
        {
            if (createdAt == DateTime.MinValue)
            {
                throw new DomainException("CreatedAt cannot be the minimum DateTime value.");
            }

            CreatedAt = createdAt;
        }

        public void SetUpdatedAt(DateTime updatedAt)
        {
            UpdatedAt = updatedAt;
        }

        public void SetFinishedAt(DateTime finishedAt)
        {
            FinishedAt = finishedAt;
        }

        public void SetJobDuration(string jobDuration)
        {
            JobDuration = jobDuration;
        }

        public void SetErrorMessage(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        
        public void FinishJob(string jobDuration)
        {
            SetStatus(JobStatus.Finished);
            SetFinishedAt(DateTime.Now);
            SetJobDuration(jobDuration);
        }

        public void FailJob(string errorMessage)
        {
            SetStatus(JobStatus.Failed);
            SetFinishedAt(DateTime.Now);
            SetUpdatedAt(DateTime.Now);
            SetErrorMessage(errorMessage);
        }
    }
}

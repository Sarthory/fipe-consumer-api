using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FipeConsumer.Domain.Enums;

namespace FipeConsumer.Domain.Entities
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobId { get; set; }

        public required JobType Type { get; set; }

        public required JobStatus Status { get; set; }

        public required DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime FinishedAt { get; set; }

        public string? JobDuration { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
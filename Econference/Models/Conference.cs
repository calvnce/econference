using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Econference.Models
{
    [Table("conferences")]
    public class Conference
    {
        [Key]
        public int Id { get; set; }
       
        [ForeignKey(nameof(HallId))]
        public int? HallId { get; set; }
        [Required]
        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int ParticipantCount { get; set; }
        [Required]
        public string Equipment { get; set;}
        [Required]
        public DateOnly StartDate { get; set; }
        [Required]
        public TimeOnly StartTime { get; set; }
        [Required]
        public DateOnly EndDate { get; set;}
        [Required]
        public TimeOnly EndTime { get; set; }
        [Required]
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Hall Hall { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}

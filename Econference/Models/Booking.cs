using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Econference.Models
{
    [Table("bookings")]
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        [Required]
        [ForeignKey(nameof(ConferenceId))]
        public int ConferenceId { get; set; }
        [ForeignKey(nameof(CateringProviderId))]
        public int CateringProviderId {  get; set; }
        [Required]
        public string Status { get; set;}
        [Required]
        public DateTime BookedAt { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Conference Conference { get; set; }
        public virtual CateringProvider Cater { get; set; }
    }
}

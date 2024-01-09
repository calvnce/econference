using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Econference.Models
{
    [Table("halls")]
    public class Hall
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string RoomNumber { get; set; }
        [Required]
        public string BuildingName { get; set; }
        [Required]
        public string BuildingFloor { get; set; }
        [Required]
        public string Capacity { get; set; }
        [Required]
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Conference> Conferences { get; set; }

    }
}

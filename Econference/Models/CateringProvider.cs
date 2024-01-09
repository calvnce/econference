using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Econference.Models
{
    [Table("catering_providers")]
    public class CateringProvider
    {
        [Key]
        public int Id { get; set; }
        public string ProviderName { get; set; }
        public string Menu { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

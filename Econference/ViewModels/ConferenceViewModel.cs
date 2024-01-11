using System.ComponentModel.DataAnnotations.Schema;

namespace Econference.ViewModels
{
    public class ConferenceViewModel
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ParticipantCount { get; set; }
        public string Equipment { get; set; }
        public string StartDate { get; set; }
        public string StarTime { get; set; }
        public string EndDate { get; set; }
        public string EndTime { get; set; }
        public string Status { get; set; }
    }
}

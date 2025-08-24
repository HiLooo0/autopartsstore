using System; 
using System.ComponentModel.DataAnnotations;

namespace AutoPartsStore.Models
{
    public class Application
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string DesiredPosition { get; set; }

        public string Department { get; set; }

        public string Comment { get; set; }

        [Display(Name = "Дата подання")]
        public DateTime SubmissionDate { get; set; }
    }
}
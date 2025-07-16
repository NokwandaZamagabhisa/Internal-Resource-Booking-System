using System.ComponentModel.DataAnnotations;

namespace ResourceBooking.Models
{
    public class ResourceDto
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Resource Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be a positive number")]
        [Display(Name = "Capacity")]
        public int Capacity { get; set; }
    }
}

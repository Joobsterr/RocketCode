using System.ComponentModel.DataAnnotations;

namespace SpaceshipService.Models
{
    public class Manufacturer
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string ManufacturerName { get; set; }
    }
}

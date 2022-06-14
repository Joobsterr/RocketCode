using System.ComponentModel.DataAnnotations;

namespace SpaceshipService.Models
{
    public class Spaceship
    {
        [Required]
        public string ShipName { get; set; }

        [Required]
        public string Torque { get; set; }

        [Required]
        public DateTime ConstructionDate { get; set; }

        [Required]
        public int Speed { get; set; }

        [Required]
        public int ManufacturerID { get; set; }

        [Required]
        [Key]
        public int ID { get; set; }

    }
}
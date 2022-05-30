using System.ComponentModel.DataAnnotations;

namespace SpaceshipService.Models
{
    public class Spaceship
    {
        [Required]
        public string ShipName { get; set; }

        [Required]
        public Boolean Torque { get; set; }

        [Required]
        public DateTime ConstructionDate { get; set; }

        [Required]
        public long Speed { get; set; }

        [Required]
        public int ManufacturerID { get; set; }

    }
}
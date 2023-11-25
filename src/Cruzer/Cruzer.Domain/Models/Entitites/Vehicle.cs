using System.ComponentModel.DataAnnotations;

namespace Cruzer.Domain.Models.Entitites
{
    public class Vehicle
    {
        public int Id { get; set; }
    
        public Customer Customer { get; set; } = null!;
    
        public int Year { get; set; }

        [MaxLength(50)] public string Make { get; set; } = string.Empty;

        [MaxLength(50)] public string Model { get; set; } = string.Empty;

        public int Odometer { get; set; }
    }
}
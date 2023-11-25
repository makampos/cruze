using Cruzer.Domain.Models.Entitites;

namespace Cruzer.Domain.Services
{
    public class WarrantyService
    {
        public List<Warranty> Warranties { get; } = new();
    
        public bool IsCovered(Vehicle vehicle, Repair repair)
        {
            return Warranties.Any(x => x.IsCovered(vehicle, repair));
        }
    
    }
}
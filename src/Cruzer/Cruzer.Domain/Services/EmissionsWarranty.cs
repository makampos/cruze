using Cruzer.Domain.Models.Entitites;

namespace Cruzer.Domain.Services
{
    public class EmissionsWarranty : Warranty
    {
        public EmissionsWarranty()
        {
            Duration = 10;
            Odometer = 150000;
        }
    
        public override bool IsCovered(Vehicle vehicle, Repair repair)
        {
            if (vehicle.Year + Duration < DateTime.Now.Year || vehicle.Odometer > Odometer)
            {
                return false;
            }

            return repair.Category switch
            {
                "Emissions" => true, _ => false,
            };
        }
    }
}
using Cruzer.Domain.Models.Context;
using Cruzer.Domain.Models.Entitites;
using Cruzer.Domain.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Cruzer.Tests
{
    [TestFixture]
    public class Given_a_QuoteService
    {
        protected RepairShopContext Context { get; set; } = null!;
        protected IQuoteService SUT { get; set; }
        protected RepairOrder RepairOrder { get; set; }
        protected WarrantyService WarrantyService { get; set; }

        [SetUp]
        public virtual void Setup()
        {
            Context = new RepairShopContext();
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

            WarrantyService = new WarrantyService()
            {
                Warranties =
                {
                    new BumperToBumperWarranty(),
                    new PowertrainWarranty(),
                    new EmissionsWarranty()
                }
            };

            Context.Repairs
                .Include(x => x.Parts)
                .Single(x => x.Code == "OC001");

            RepairOrder = new RepairOrder
            {
                Vehicle = new Vehicle
                {
                    Customer = new Customer
                    {
                        FirstName = "Jhon",
                        LastName = "Doe",
                        PhoneNumber = "987654321"
                    },
                },
                Repairs = { Context.Repairs.Include(x => x.Parts).Single(x => x.Code == "OC001") }
            };

            Context.RepairOrders.Add(RepairOrder);
            Context.SaveChanges();

            SUT = new QuoteService(Context, 100, WarrantyService);
        }
    }
}
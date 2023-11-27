using Cruzer.Domain.Models.Entitites;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Cruzer.Tests
{
    [TestFixture]
    public class When_Quoting_a_muffler_in_warranty : Given_a_QuoteService
    {
        private Quote? Quote { get; set; }
        
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            
            RepairOrder = new RepairOrder
            {
                Vehicle = new Vehicle
                {
                    Customer = new Customer
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        PhoneNumber = "987654321"
                    },
                    Year = DateTime.Now.Year,
                    Make = "Ford",
                    Model = "Focus",
                    Odometer = 100000
                },
                Repairs = { Context.Repairs.Include(x => x.Parts).Single(x => x.Code == "MR001") }
            };

            Context.RepairOrders.Add(RepairOrder);
            Context.SaveChanges();

            Quote = SUT.GenerateQuote(RepairOrder.Id);
        }
        
        [Test]
        public void Then_a_Quote_is_returned()
        {
            Assert.IsInstanceOf<Quote>(Quote);
        }

        [Test]
        public void Then_the_Labor_total_is_correct()
        {
            Assert.AreEqual(0, Quote!.LaborTotal);
        }

        [Test]
        public void Then_the_Parts_total_is_correct()
        {
            Assert.AreEqual(0, Quote!.PartTotal);
        }
    }
}
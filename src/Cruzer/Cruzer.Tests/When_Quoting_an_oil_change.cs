using Cruzer.Domain.Models.Entitites;
using NUnit.Framework;

namespace Cruzer.Tests
{
    public class When_Quoting_an_oil_change : Given_a_QuoteService
    {
        private Quote? Quote { get; set; }

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Quote = SUT.GenerateQuote(RepairOrder.Id);
        }

        [Test]
        public void Then_a_Quote_is_returned()
        {
            Assert.IsInstanceOf<Quote>(Quote);
        }

        [Test]
        public void Then_the_Labor_total_is_corret()
        {
            Assert.AreEqual(50, Quote!.LaborTotal);
        }

        [Test]
        public void Then_the_Parts_total_is_correct()
        {
            Assert.AreEqual(62.99M, Quote!.PartTotal);
        }
        
        [Ignore("Ignore a test")]
        [Test]
        public void Then_ToString_return_the_finished_Quote()
        {
            var expected = $@"Repair Quote #{Quote!.Id}, Valid through {Quote.ExpiryDate:D}
                            Prepared for Jhon Doe's 0 .
                            ---
                            Parts: 62.99
                            Labor 50.0
                            ---
                            Total 112.99";

            var actual = Quote!.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
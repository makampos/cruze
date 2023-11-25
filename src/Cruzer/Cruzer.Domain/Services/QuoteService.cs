using Cruzer.Domain.Models.Context;
using Cruzer.Domain.Models.Entitites;

namespace Cruzer.Domain.Services;

public class QuoteService : IQuoteService
{
    private readonly decimal laborRate;
    private readonly WarrantyService warrantyService;
    
    public QuoteService(RepairShopContext context, decimal laborRate, WarrantyService warrantyService)
    {
        Context = context;
        this.laborRate = laborRate;
        this.warrantyService = warrantyService;
    }

    public RepairShopContext Context { get; init; }

    public Quote? GenerateQuote(int repairOrderId)
    {
        var repairOrder = Context.RepairOrders.SingleOrDefault(x => x.Id == repairOrderId);

        if (repairOrder is null)
        {
            return null;
        }
        
        // var quote = new Quote
        // {
        //     RepairOrder = repairOrder,
        //     PartTotal = repairOrder.Repairs.Sum(x => x.Parts.Sum(y => y.Price)),
        //     LaborTotal = repairOrder.Repairs.Sum(x => x.Labor) * laborRate,
        //     ExpiryDate = DateTime.Today.AddDays(30)
        // };

        var quote = new Quote()
        {
            RepairOrder = repairOrder,
            PartTotal = repairOrder.Repairs.Sum(x =>
            warrantyService.IsCovered(repairOrder.Vehicle, x) 
                    ? 0 
                    : x.Parts.Sum(y => y.Price)),
            LaborTotal = repairOrder.Repairs.Sum(x => warrantyService.IsCovered(repairOrder.Vehicle, x) 
                     ? 0 
                     : x.Labor) * laborRate,
            ExpiryDate = DateTime.Today.AddDays(30)
        };
        
        Context.Quotes.Add(quote);
        Context.SaveChanges();

        return quote;
    }
}
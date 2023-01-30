using TosunlarYapiMarket.Entity.Abstract;

namespace TosunlarYapiMarket.Entity.Concrete
{
    public class PayOffDebt:EntityBase,IEntity
    {
        public decimal AmountPaid { get; set; }
        public DateTime PaidDate { get; set; } = DateTime.Now;

        public int DebtId { get; set; }
        public Debt? Debt { get; set; }
    }
}

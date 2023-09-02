using CRM.Enums;

namespace CRM.TransactionModels
{
    public class TransferTransactionModel
    {
        public decimal Amount { get; set; }
        public int AccountIdFrom { get; set; }
        public int AccountIdTo { get; set; }
        public  Currency CurrencyFrom { get; set; }
        public  Currency CurrencyTo { get; set; }
    }
}

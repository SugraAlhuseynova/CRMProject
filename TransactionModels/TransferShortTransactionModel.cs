namespace CRM.TransactionModels
{
    public class TransferShortTransactionModel
    {
        public decimal Amount { get; set; }
        public int AccountIdFrom { get; set; }
        public int AccountIdTo { get; set; }
    }
}

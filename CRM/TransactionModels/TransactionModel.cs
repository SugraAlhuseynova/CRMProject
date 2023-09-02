using CRM.Enums;

namespace CRM.TransactionModels;

public class TransactionModel
{
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
}
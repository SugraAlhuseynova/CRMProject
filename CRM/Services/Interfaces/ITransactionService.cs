using CRM.TransactionModels;

namespace CRM.Services.Interfaces;

public interface ITransactionService
{
    public Task<int> AddDeposit(TransactionModel transaction, int userId);
    public Task<int> AddTransfer(TransferTransactionModel transaction, int userId);
}
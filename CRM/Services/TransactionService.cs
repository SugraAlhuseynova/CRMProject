using CRM.Controllers;
using CRM.Repositories.Interfaces;
using CRM.Services.Interfaces;
using CRM.TransactionModels;
using RestSharp;
using System.IO;

namespace CRM.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IAccountsRepository _accountsRepository;

        public TransactionService(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }
        public async Task<int> AddDeposit(TransactionModel transaction, int userId)
        {
            //Check if account doesn't exist
            var entity = await _accountsRepository.GetAsync(x=>x.Id == transaction.AccountId);
            if (entity == null) { return 0; }
            var response = 0;
            //var response = await _requestHelper.SendTransactionPostRequest(TransactionEndpoints.Deposit, transactionModel);
            //_logger.LogInformation($"Request successful.");
            return response;
        }

        public Task<int> AddTransfer(TransferTransactionModel transaction, int userId)
        {
            throw new NotImplementedException();
        }




        //public void SendTransactionPostRequest()
        //{
        //    // Forming URL link
        //    var request = new RestRequest($"{_config[Microservice.MarvelousTransactionStore.ToString() + "Url"]}{TransactionEndpoints.ApiTransactions}{path}", Method.Post);
        //    request.AddBody(requestModel!); //Add request model
        //                                    //Parsing and checking responce
        //    var response = Convert.ToInt32((await ExecuteRequest(request)).Content);
        //    return response;
        //}
    }
}

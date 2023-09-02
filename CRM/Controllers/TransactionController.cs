using AutoMapper;
using CRM.Enums;
using CRM.Models;
using CRM.Services.Interfaces;
using CRM.TransactionModels;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Data;

namespace CRM.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly IRestClient _restClient;
    private readonly IMapper _mapper;

    public TransactionController(ITransactionService transactionService, IRestClient restClient,IMapper mapper)
    {
        _transactionService = transactionService;
        _restClient = restClient;
        _mapper = mapper;
    }

    [HttpPost("deposit")]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> AddDeposit([FromBody] TransactionModel transaction)
    {
        var user = GetIdentity();
        var response = await _transactionService.AddDeposit(transaction, user.Id);

        return Ok(response);
    }
    [HttpPost("transfer")]
    public async Task<ActionResult> AddTransfer([FromBody] TransferShortTransactionModel transaction)
    {
        var user = GetIdentity();  // Checking token
        var userId = (int)user.Id;
        var transactionModel = _mapper.Map<TransferTransactionModel>(transaction);
        var response = await _transactionService.AddTransfer(transactionModel, userId); //(transactionIdFrom, transactionIdTo)
        return StatusCode(201, response);
    }

    private IdentityModel GetIdentity()
    {
        var token = HttpContext.Request.Headers.Authorization.FirstOrDefault();
        var identity = GetUserIdentityByToken(token).Result;

        return identity;
    }

    private async Task<IdentityModel> GetUserIdentityByToken(string token)
    {
        var request = new RestRequest("/api/auth/checkToken");
        var response = await _restClient.ExecuteAsync<IdentityModel>(request);

        return response.Data;
    }





}

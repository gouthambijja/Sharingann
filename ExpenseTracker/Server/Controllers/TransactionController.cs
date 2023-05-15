using ExpressTrackerLogicLayer.Contracts;
using ExpressTrackerLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpGet]
        public async Task<List<BLTransaction>> Get(string UserId)
        {
            return await _transactionService.GetAll(UserId);
        }
        [HttpPost("AddTransaction")]
        public async Task<BLTransaction> Post(BLTransaction transaction)
        {
            Console.WriteLine(transaction.UserId + " " + transaction.Amount);   
            return await _transactionService.Add(transaction);
        }
    }
}

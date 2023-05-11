using ExpressTrackerLogicLayer.Contracts;
using ExpressTrackerLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}

using ExpressTrackerLogicLayer.Contracts;
using ExpressTrackerLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
        [HttpPost("AddTransactions")]
        public async Task<List<BLTransaction>> Post(List<BLTransaction> transactions)
        {
            return await _transactionService.AddMany(transactions);
        }
        [HttpGet("GetFiltered")]
        public async Task<List<BLTransaction>> GetFiltered(string UserId,string Name,string Category,string Description,DateTime StartDate,DateTime EndDate)
        {

            return await _transactionService.GetFiltered(UserId, Name,Category,Description,StartDate,EndDate);
        }
        [HttpGet("GetFilteredByDate")]
        public async Task<List<BLTransaction>> GetFilteredByDate(string UserId, DateTime StartDate, DateTime EndDate)
        {
            Console.WriteLine("helthere");
            return await _transactionService.GetFilteredByDateRange(UserId, StartDate, EndDate);

        }
        [HttpPut("EditTransaction")]
        public async Task<BLTransaction> UpdateTransaction(BLTransaction transaction)
        {
            return await _transactionService.Update(transaction);
                    
        }
        [HttpDelete("DeleteTransaction/{id}")]
        public async Task<bool> DeleteTransaction(string id)
        {
            return await _transactionService.Delete(id);
        }
    }

}

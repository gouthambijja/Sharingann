using ExpressTrackerDBAccessLayer.Contracts;
using ExpressTrackerDBAccessLayer.Models;
using ExpressTrackerLogicLayer.Contracts;
using ExpressTrackerLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressTrackerLogicLayer.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<BLTransaction> Add(BLTransaction transaction)
        {
            try
            {
                Console.WriteLine("tranasactions!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                transaction.TransactionId = Guid.NewGuid().ToString();
                var mapper = AutoMappers.InitializeTransactionAutoMapper();
                Transaction _transaction = mapper.Map<Transaction>(transaction);
                _transaction = await _transactionRepository.Add(_transaction);
                if (_transaction == null) return null;
                return transaction;
            }
            catch
            {
                return null;
            }
        }

        

        public async Task<BLTransaction> Delete(BLTransaction transaction)
        {
            try
            {
                var mapper = AutoMappers.InitializeTransactionAutoMapper();
                Transaction _transaction = mapper.Map<Transaction>(transaction);
                _transaction = await _transactionRepository.Delete(_transaction);
                if (_transaction == null) return null;
                return transaction;
            }
            catch
            {
                return null;
            }
        }

        

        public async Task<BLTransaction> Get(BLTransaction transaction)
        {
            throw new NotImplementedException();
        }

        

        public async Task<List<BLTransaction>> GetAll(string UserId)
        {
            try
            {
               var transactions =  await _transactionRepository.GetAll(UserId);
                if (transactions == null) return null;
               var _BLTransactions = new List<BLTransaction>();
                foreach (var transaction in transactions)
                {
                    _BLTransactions.Add(new BLTransaction()
                    {
                        Name = transaction.Name,
                        Amount = transaction.Amount,
                        Category = transaction.Category,
                        Description = transaction.Description,
                        Date = transaction.Date,
                        UserId = transaction.UserId,
                        TransactionId = transaction.TransactionId
                    });
                }
                return _BLTransactions;
            }
            catch
            {
                return null;
            }
        }

        public async Task<BLTransaction> Update(BLTransaction transaction)
        {
            try
            {
                var mapper = AutoMappers.InitializeTransactionAutoMapper();
                Transaction _transaction = mapper.Map<Transaction>(transaction);
                _transaction = await _transactionRepository.Update(_transaction);
                if (_transaction == null) return null;
                return transaction;
            }
            catch
            {
                return null;
            }
        }

    }
}

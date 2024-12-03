using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Interfaces.Repositories
{
    public interface ITransactionRepository
    : IGenericRepository<Transaction>
    {
        Task<int> CountTransactions();
        Task<int> CountTransactionsOfDay(DateTime day);
        Task<int> CountPayments();
        Task<int> CountPaymentsOfDay(DateTime day);
    }
}

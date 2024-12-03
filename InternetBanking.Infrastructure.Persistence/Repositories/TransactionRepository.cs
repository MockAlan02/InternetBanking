using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InternetBanking.Application.Extras;
using InternetBanking.Application.Interfaces.Repositories;
using InternetBanking.Domain.Entities;
using InternetBanking.Domain.Enums;
using InternetBanking.Infrastructure.Persistence.Contexts;

namespace InternetBanking.Infrastructure.Persistence.Repositories
{
    public class TransactionRepository: GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly MainContext _context;

        public TransactionRepository(MainContext context) : base(context)
        {
            _context = context;
        }

        public Task<int> CountPayments()
        => _context.Transactions.Where(t => t.TransactionType == TransactionType.Payments)
                                .Count()
                                .AsTask();

        public Task<int> CountPaymentsOfDay(DateTime day)
        => OfDay(day).Where(t => t.TransactionType == TransactionType.Payments)
                     .Count()
                     .AsTask();

        public Task<int> CountTransactions()
        => _context.Transactions.Count()
                                .AsTask();
        public Task<int> CountTransactionsOfDay(DateTime day)
        => OfDay(day).Count()
                     .AsTask();
        protected virtual IQueryable<Transaction> OfDay(DateTime day)
        => _context.Transactions.Where(t => t.DateTransanction.Year == day.Year
                                            && t.DateTransanction.Month == day.Month
                                            && t.DateTransanction.Day == day.Day);
    }
}

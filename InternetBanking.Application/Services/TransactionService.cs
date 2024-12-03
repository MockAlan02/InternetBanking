using AutoMapper;

using InternetBanking.Application.Dto.Transaction;
using InternetBanking.Application.Interfaces.Repositories;
using InternetBanking.Application.Interfaces.Services;
using InternetBanking.Application.ViewModels.Trasanction;
using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Services
{
    public class TransactionService : GenericService<TransactionSaveViewModel, TransactionViewModel, Transaction>, ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper) : base(transactionRepository, mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<TransactionStatisticsDTO> CalculateStatistics()
        {
            DateTime now = DateTime.UtcNow;
            return new()
            {
                NumberOfTransactionsInCurrentDay = await _transactionRepository.CountTransactionsOfDay(now),
                TotalNumberOfTransactions = await _transactionRepository.CountTransactions(),
                NumberOfPaymentsInCurrentDay = await _transactionRepository.CountPaymentsOfDay(now),
                TotalNumberOfPayments = await _transactionRepository.CountPayments()

            };
        }
    }
}

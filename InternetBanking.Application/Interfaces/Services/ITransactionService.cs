using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InternetBanking.Application.Dto.Transaction;
using InternetBanking.Application.ViewModels.Trasanction;
using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Interfaces.Services
{
    public interface ITransactionService:IGenericService<TransactionSaveViewModel, TransactionViewModel, Transaction>
    {
        Task<TransactionStatisticsDTO> CalculateStatistics();
    }
}

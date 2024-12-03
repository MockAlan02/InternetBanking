using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InternetBanking.Application.ViewModels.Loan;
using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Interfaces.Services
{
    public interface ILoanService: IProductService<LoanSaveViewModel, LoanViewModel, Loan>
    {
    }
}

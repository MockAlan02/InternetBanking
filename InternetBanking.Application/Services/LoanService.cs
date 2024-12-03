using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using InternetBanking.Application.Interfaces.Repositories;
using InternetBanking.Application.Interfaces.Services;
using InternetBanking.Application.ViewModels.Loan;
using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Services
{
    public class LoanService: ProductService<LoanSaveViewModel, LoanViewModel, Loan>, ILoanService
    {
        private readonly ILoanRepository _repository;
        private readonly IMapper _mapper;

        public LoanService(ILoanRepository _repository, IMapper _mapper): base(_repository, _mapper) 
        {
            this._repository = _repository;
            this._mapper = _mapper;
        }
    }
}

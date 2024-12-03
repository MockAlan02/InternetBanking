using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using InternetBanking.Application.Interfaces.Repositories;
using InternetBanking.Application.Interfaces.Services;
using InternetBanking.Application.ViewModels.Beneficiarie;
using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Services
{
    public class BeneficiarieService: GenericService<BeneficiarieSaveViewModel, BeneficiarieViewModel, Beneficiaries>,  IBeneficiarieService
    {
        private readonly IBeneficiareRepository _beneficiareRepository;
        private readonly IMapper _mapper;

        public BeneficiarieService(IBeneficiareRepository _beneficiareRepository, IMapper _mapper) : base(_beneficiareRepository, _mapper) 
        {
            this._mapper = _mapper;
            this._beneficiareRepository = _beneficiareRepository;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InternetBanking.Application.ViewModels.Beneficiarie;
using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Interfaces.Services
{
    public interface IBeneficiarieService: IGenericService<BeneficiarieSaveViewModel, BeneficiarieViewModel, Beneficiaries>
    {
    }
}

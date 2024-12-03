using InternetBanking.Application.Dto.Payment;
using InternetBanking.Application.Extras.ResultObject;
using InternetBanking.Application.ViewModels.Payment;
using InternetBanking.Application.ViewModels.SavingsAccount;
using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Interfaces.Services
{
    public interface ISavingsAccountService: IProductService<SavingsAccountSaveViewModel, SavingsAccountViewModel, SavingsAccount>
    {
        Task<Result<PaymentResponse>> ExpressPayment(ExpressPaymentSaveViewModel vm);
    }
}

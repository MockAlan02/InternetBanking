using InternetBanking.Application.Dto.Payment;
using InternetBanking.Application.Extras.ResultObject;
using InternetBanking.Application.ViewModels.CreditCard;
using InternetBanking.Domain.Entities;

namespace InternetBanking.Application.Interfaces.Services
{
    public interface ICreditCardService: IProductService<CreditCardSaveViewModel, CreditCardViewModel, CreditCard>
    {
        Task<Result<PaymentResponse>> PayCreditCard(PayCreditCardViewModel vm);
    }
}

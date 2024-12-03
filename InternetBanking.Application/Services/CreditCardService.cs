using AutoMapper;

using InternetBanking.Application.Dto.Payment;
using InternetBanking.Application.Enums;
using InternetBanking.Application.Extras.ResultObject;
using InternetBanking.Application.Interfaces.Repositories;
using InternetBanking.Application.Interfaces.Services;
using InternetBanking.Application.ViewModels.CreditCard;
using InternetBanking.Application.ViewModels.Payment;
using InternetBanking.Application.ViewModels.SavingsAccount;
using InternetBanking.Domain.Entities;
using InternetBanking.Domain.Enums;
using InternetBanking.Domain.Extension;

namespace InternetBanking.Application.Services
{
    public class CreditCardService : ProductService<CreditCardSaveViewModel, CreditCardViewModel, CreditCard>, ICreditCardService
    {
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly ITransactionRepository _transactionRepository;

        private readonly IMapper _mapper;

        public CreditCardService(ICreditCardRepository creditCardRepository,
            IMapper mapper,
            ISavingsAccountService savingsAccountService,
            ITransactionRepository transactionRepository) : base(creditCardRepository, mapper)
        {
            _creditCardRepository = creditCardRepository;
            _mapper = mapper;
            _savingsAccountService = savingsAccountService;
            _transactionRepository = transactionRepository;
        }

        public async Task<Result<PaymentResponse>> PayCreditCard(PayCreditCardViewModel vm)
        {

            CreditCard? creditCard = await _creditCardRepository.FindByNumberAccount(vm.CreditCardNumberAccount!);
            SavingsAccountViewModel? savingAccount = await _savingsAccountService.FindByNumberAccount(vm.SavingAccountNumber!);

            if (creditCard is null)
            {
                return ErrorType.NotFound.Because("No existe una tarjeta de credito con ese identificador");
            }

            if (savingAccount is null)
            {
                return AppErrors.AccountDoesntExist;
            }

            if(vm.Amount <=0)

            {
                return ErrorType.Amount.Because("El monto a pagar, no puede ser menor o igual a 0");
            }
            if (savingAccount.Amount < vm.Amount)
            {
                return ErrorType.Amount.Because("El monto a pagar, excede el saldo disponible");
            }

            if (vm.Amount > creditCard.LoanAmount)
            {
                savingAccount.Amount -= creditCard.LoanAmount;
                creditCard.LoanAmount = 0;
            }
            else
            {
                savingAccount.Amount -= vm.Amount;
                creditCard.LoanAmount -= vm.Amount;
            }

            await Update(_mapper.Map<CreditCardSaveViewModel>(creditCard), creditCard.Id);
            await _savingsAccountService.Update(_mapper.Map<SavingsAccountSaveViewModel>(savingAccount), savingAccount.Id);
            await _transactionRepository.AddAsync(TransactionType.Payments.Create());
            return new PaymentResponse();

        }

        public async Task<Result<PaymentResponse>> ProcessCashAdvance(ExpressPaymentSaveViewModel vm)
        {
            var creditCard = await FindByNumberAccount(vm.IdentifierAccount!);
            if(creditCard == null)
            {
                return AppErrors.AccountDoesntExist;
            }
            var savingAccount = await _savingsAccountService.FindByNumberAccount(vm.AccountPaymentFrom);
            
            if(savingAccount == null)
            {
                return AppErrors.AccountDoesntExist;
            }

            decimal creditcardRealAmount = creditCard.Amount - creditCard.LoanAmount;
            if (vm.Amount <= 0)
            {
                return ErrorType.Amount.Because("El monto a pagar, no puede ser menor o igual a 0");
            }

            if (creditcardRealAmount < vm.Amount)
            {
                return ErrorType.Amount.Because("El monto a transferir, excede el saldo disponible");
            }

            creditCard.LoanAmount += AddTaxes(vm.Amount);
            savingAccount.Amount += vm.Amount;

            await Update(_mapper.Map<CreditCardSaveViewModel>(creditCard), creditCard.Id);
            await _savingsAccountService.Update(_mapper.Map<SavingsAccountSaveViewModel>(savingAccount), savingAccount.Id);
            
            return new PaymentResponse();
        }
        private static decimal AddTaxes(decimal amount)
        {
            return amount + (amount * 0.0625m);
        }
    }
}

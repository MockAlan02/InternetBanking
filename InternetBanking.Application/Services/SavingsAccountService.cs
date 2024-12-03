using AutoMapper;

using InternetBanking.Application.Dto.Payment;
using InternetBanking.Application.Enums;
using InternetBanking.Application.Extras.ResultObject;
using InternetBanking.Application.Interfaces.Repositories;
using InternetBanking.Application.Interfaces.Services;
using InternetBanking.Application.ViewModels.Payment;
using InternetBanking.Application.ViewModels.SavingsAccount;
using InternetBanking.Domain.Entities;
using InternetBanking.Domain.Enums;
using InternetBanking.Domain.Extension;

namespace InternetBanking.Application.Services
{
    public class SavingsAccountService : ProductService<SavingsAccountSaveViewModel, SavingsAccountViewModel, SavingsAccount>, ISavingsAccountService
    {
        private readonly ISavingsAccountRepository _savingAccountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public SavingsAccountService(ISavingsAccountRepository savingAccountRepository,
                                     IMapper mapper,
                                     ITransactionRepository transactionRepository)
        : base(savingAccountRepository, mapper)
        {
            _savingAccountRepository = savingAccountRepository;
            _mapper = mapper;
            _transactionRepository = transactionRepository;
        }

        public async Task<Result<PaymentResponse>> ExpressPayment(ExpressPaymentSaveViewModel vm)
        {
            var savingAccount1 = await _savingAccountRepository.FindByNumberAccount(vm.IdentifierAccount);
            var savingAccount2 = await _savingAccountRepository.FindByNumberAccount(vm.AccountPaymentFrom);

            if (savingAccount1 == null)
            {
                return AppErrors.AccountDoesntExist;
            }

            if (savingAccount2 == null)
            {
                return AppErrors.AccountDoesntExist;
            }

            if (savingAccount2.Amount < vm.Amount)
            {
                return ErrorType.Amount.Because("No cuentas con ese saldo disponible");
            }

            savingAccount1.Amount += vm.Amount;
            savingAccount2.Amount -= vm.Amount;
            await _savingAccountRepository.UpdateAsync(savingAccount1, savingAccount1.Id);
            await _savingAccountRepository.UpdateAsync(savingAccount2, savingAccount2.Id);
            await _transactionRepository.AddAsync(TransactionType.Payments.Create());
            return new PaymentResponse();
        }
    }
}

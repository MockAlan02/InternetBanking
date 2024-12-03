using System.Security.Claims;

using AutoMapper;

using InternetBanking.Application.Dto.Product;
using InternetBanking.Application.Dto.Transaction;
using InternetBanking.Application.Dto.User;
using InternetBanking.Application.Interfaces.Services;
using InternetBanking.Application.ViewModels.CreditCard;
using InternetBanking.Application.ViewModels.Customer;
using InternetBanking.Application.ViewModels.Loan;
using InternetBanking.Application.ViewModels.Product;
using InternetBanking.Application.ViewModels.SavingsAccount;
using InternetBanking.Application.ViewModels.User;
using InternetBanking.Domain.Enums;
using InternetBanking.Presentation.Helpers;
using InternetBanking.Presentation.Interfacces;
using InternetBanking.Presentation.Models;

using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Presentation.Controllers
{
    // [Authorize(Roles = nameof(UserType.Admin))]
    public class AdminController
    : Controller
    {
        private readonly IUserService _userService;
        private readonly ISavingsAccountService _savingsAccountService;
        private readonly ILoanService _loanService;
        private readonly ICreditCardService _creditCardService;
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly ILoginStore _loginStore;

        public AdminController(IUserService userService,
                                ISavingsAccountService savingsAccountService,
                                ILoanService loanService,
                                ICreditCardService creditCardService,
                                IMapper mapper,
                                ITransactionService transactionService,
                                IProductService productService,
                                ICustomerService customerService,
                                ILoginStore loginStore)
        {
            _userService = userService;
            _loanService = loanService;
            _savingsAccountService = savingsAccountService;
            _creditCardService = creditCardService;
            _mapper = mapper;
            _transactionService = transactionService;
            _productService = productService;
            _customerService = customerService;
            _loginStore = loginStore;
        }

        public async Task<IActionResult> Index()
        {
            TransactionStatisticsDTO transactionStatistics = await _transactionService.CalculateStatistics();
            ProductsStatisticsDTO productsStatisticsDTO = await _productService.CalculateStatistics();
            UserStatisticsDTO userStatistics = await _customerService.CalculateUserStatistics();

            return View(new AdminIndexViewModel()
            {
                TotalTransactions = transactionStatistics.TotalNumberOfTransactions,
                DailyTransactions = transactionStatistics.NumberOfTransactionsInCurrentDay,
                TotalPayments = transactionStatistics.TotalNumberOfPayments,
                DailyPayments = transactionStatistics.NumberOfTransactionsInCurrentDay,
                ActiveClients = userStatistics.NumberOfActiveCustomer,
                InactiveClients = userStatistics.NumberOfUnactiveCustomer,
                TotalProducts = productsStatisticsDTO.TotalProducts
            });
        }

        public async Task<IActionResult> Users(UserFilterDTO? dto)
        {
            string userId = await _loginStore.Gets(c => c.Id);
            UserFilterDTO userFilterDTO = dto ?? new UserFilterDTO();
            UsersInGroupsViewModel usersInGroupsViewModel = await _userService.GetAllUsersInGroup(
                userFilterDTO
            );
            // Temporal, as in permanent until I remember this.
            usersInGroupsViewModel.ActiveAdmins = usersInGroupsViewModel.ActiveAdmins
                                                                        .Where(a => a.Id != userId)
                                                                        .ToList();
            return View(new AdminUsersViewModel()
            {
                Users = usersInGroupsViewModel,
                Filters = userFilterDTO
            });
        }

        public async Task<IActionResult> ToggleActive(bool activate, string userId)
        {
            await _userService.SetIsActiveUser(activate, userId);
            return this.RedirectBack();
        }

        public IActionResult EditCustomer()
        {

            return View("Customer/EditCustomer", new CustomerSaveViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Addproduct(ProductSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("EditCustomer", vm);
            }

            switch (vm.ProductType)
            {
                case ProductType.SavingAccount:
                    var savingAccount = _mapper.Map<SavingsAccountSaveViewModel>(vm);
                    await _savingsAccountService.Add(savingAccount);
                    break;
                case ProductType.Loan:
                    var loanAccount = _mapper.Map<LoanSaveViewModel>(vm);
                    await _loanService.Add(loanAccount);
                    break;
                case ProductType.CreditCard:
                    var creditCardAccount = _mapper.Map<CreditCardSaveViewModel>(vm);
                    await _creditCardService.Add(creditCardAccount);
                    break;
            }

            return RedirectToAction($"EditCustomer/{vm.CustomerId}", vm);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteProduct(ProductSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("EditCustomer", vm);
            }
            switch (vm.ProductType)
            {
                case ProductType.SavingAccount:
                    await _savingsAccountService.Delete(vm.Id);
                    break;
                case ProductType.Loan:
                    await _loanService.Delete(vm.Id);
                    break;
                case ProductType.CreditCard:
                    await _creditCardService.Delete(vm.Id);
                    break;
            }
            return RedirectToAction($"EditCustomer/{vm.CustomerId}", vm);
        }
    }
}
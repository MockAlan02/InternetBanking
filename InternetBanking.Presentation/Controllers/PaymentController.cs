using InternetBanking.Application.Enums;
using InternetBanking.Application.Extras.ResultObject;
using InternetBanking.Application.Interfaces.Services;
using InternetBanking.Application.ViewModels.CreditCard;
using InternetBanking.Application.ViewModels.Payment;

using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Presentation.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ICreditCardService _creditCardService;
        private readonly IUserService _userService;
        private readonly ISavingsAccountService _savingsAccountService;
        public PaymentController(ICreditCardService creditCardService,
                                IUserService userService,
                                ISavingsAccountService savingsAccountService)
        {
            _creditCardService = creditCardService;
            _userService = userService;
            _savingsAccountService = savingsAccountService;
        }

        public IActionResult PayCreditCard()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PayCreditCard(PayCreditCardViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var result = await _creditCardService.PayCreditCard(vm);

            if (result.IsSuccess)
            {
                return RedirectToAction("payment/PayCreditCard");
            }

            foreach (AppError error in result.Errors)
            {
                switch (error.Type)
                {
                    case ErrorType.Amount:
                        ModelState.AddModelError(nameof(PayCreditCardViewModel.Amount), error.Message);
                        break;
                }
            }

            return View(vm);
        }

        public IActionResult ExpressPayment()
        {
            return View(new ExpressPaymentSaveViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> ExpressPayment(ExpressPaymentSaveViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var result = await _savingsAccountService.ExpressPayment(vm);

            if (result.IsSuccess)
            {
                return RedirectToAction("payment/PayCreditCard");
            }

            foreach (AppError error in result.Errors)
            {
                switch (error.Type)
                {
                    case ErrorType.Amount:
                        ModelState.AddModelError(nameof(vm.Amount), error.Message);
                        break;
                    case ErrorType.NotFound:
                        ModelState.AddModelError(nameof(vm.IdentifierAccount), error.Message);
                        break;
                }
            }

            return RedirectToAction("ExpressPayment");
        }
    }
}

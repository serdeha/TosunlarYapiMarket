using Microsoft.AspNetCore.Mvc;
using TosunlarYapiMarket.Business.Abstract;

namespace TosunlarYapiMarket.WebUI.Areas.Admin.ViewComponents
{
    public class DashboardCardViewComponent:ViewComponent
    {
        private readonly IStockService _stockService;
        private readonly ICustomerService _customerService;
        private readonly IDebtService _debtService;
        private readonly IInvoiceService _invoiceService;

        public DashboardCardViewComponent(IStockService stockService, ICustomerService customerService, IDebtService debtService, IInvoiceService invoiceService)
        {
            _stockService = stockService;
            _customerService = customerService;
            _debtService = debtService;
            _invoiceService = invoiceService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.StockCount = await _stockService.GetCountAsync(x => x.IsActive && !x.IsDeleted && x.StockAnyDetail > 0);
            ViewBag.CustomerCount = await _customerService.GetCountAsync(x => x.IsActive && !x.IsDeleted);
            ViewBag.DebtCount = await _debtService.GetCountAsync(x => x.IsActive && !x.IsDeleted && x.PaymentAmount > 0);
            ViewBag.InvoiceCount = await _invoiceService.GetCountAsync(x => x.IsActive && !x.IsDeleted);
            return View();
        }
    }
}

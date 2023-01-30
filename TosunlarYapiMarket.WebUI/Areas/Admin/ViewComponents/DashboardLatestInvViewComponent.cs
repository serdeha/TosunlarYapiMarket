using Microsoft.AspNetCore.Mvc;
using TosunlarYapiMarket.Business.Abstract;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.WebUI.Areas.Admin.ViewComponents
{
    public class DashboardLatestInvViewComponent:ViewComponent
    {
        private readonly IInvoiceService _invoiceService;

        public DashboardLatestInvViewComponent(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Invoice> getLastFive = await _invoiceService.GetAllWithFilterAsync(x => x.IsActive && !x.IsDeleted, x => x.Customer!)!;
            return View(getLastFive.TakeLast(5));
        }
    }
}

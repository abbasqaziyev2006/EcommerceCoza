using EcommerceCoza.BLL.ViewModels;
using ECommerceCoza.DAL.DataContext.Entities;

namespace EcommerceCoza.BLL.Services.Contracts
{
    public interface ICurrencyService:ICrudService<Currency, CurrencyViewModel, CurrencyCreateViewModel, CurrencyUpdateViewModel> { };
}

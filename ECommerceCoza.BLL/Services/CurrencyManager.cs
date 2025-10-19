using AutoMapper;
using EcommerceCoza.BLL.Services;
using EcommerceCoza.BLL.Services.Contracts;
using EcommerceCoza.BLL.ViewModels;
using EcommerceCoza.DAL.DataContext.Repositories.Contracts;
using ECommerceCoza.DAL.DataContext.Entities;



namespace ECommerceProject.BL.Services
{
    public class CurrencyManager:CrudManager<Currency, CurrencyViewModel, CurrencyCreateViewModel, CurrencyUpdateViewModel>,
        ICurrencyService
    {
        public CurrencyManager(IRepository<Currency> repository, IMapper mapper)
            :base(repository, mapper)
        {
            
        }
    }
}

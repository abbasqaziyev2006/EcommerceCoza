using AutoMapper;
using EcommerceCoza.BLL.Services.Contracts;
using EcommerceCoza.BLL.ViewModels;
using EcommerceCoza.DAL.DataContext.Repositories.Contracts;
using ECommerceCoza.DAL.DataContext.Entities;

namespace EcommerceCoza.BLL.Services
{
    public class LanguageManager : CrudManager<Language, LanguageViewModel, LanguageCreateViewModel, LanguageUpdateViewModel>,
        ILanguageService
    {
        public LanguageManager(IRepository<Language> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}

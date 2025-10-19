using EcommerceCoza.BLL.ViewModels;
using ECommerceCoza.DAL.DataContext.Entities;

namespace EcommerceCoza.BLL.Services.Contracts
{
    public interface ILanguageService:ICrudService<Language, LanguageViewModel, LanguageCreateViewModel, LanguageUpdateViewModel> { }
}

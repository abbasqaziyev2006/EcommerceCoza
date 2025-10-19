using EcommerceCoza.DAL.DataContext.Repositories.Contracts;
using ECommerceCoza.DAL.DataContext;
using ECommerceCoza.DAL.DataContext.Entities;

namespace EcommerceCoza.DAL.DataContext.Repositories
{
    public class CurrencyRepository : EFCoreRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
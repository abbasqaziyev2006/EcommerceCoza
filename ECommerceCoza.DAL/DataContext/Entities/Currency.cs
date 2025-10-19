namespace ECommerceCoza.DAL.DataContext.Entities
{
    public class Currency : TimeStample
    {
        public string CurrencyName { get; set; } = null!;
        public string Symbol { get; set; } = null!;
        public string CountryName { get; set; } = null!;
        public string IconName { get; set; } = null!;
    }

 
}

namespace TesteBMP.Domain.Adapters
{
    public interface IApiAdapter
    {
        Task<decimal> GetPriceIsin(string isin);
    }
}

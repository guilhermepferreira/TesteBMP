using TesteBMP.Domain.Models;

namespace TesteBMP.Domain.Adapters
{
    public interface IDbAdapter
    {
        Task SaveInDataBase(IsinModel isin);
    }
}

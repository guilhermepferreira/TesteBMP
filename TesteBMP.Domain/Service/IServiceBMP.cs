namespace TesteBMP.Domain.Service
{
    public interface IServiceBMP
    {
        Task SaveIsin(List<string> isin);
    }
}

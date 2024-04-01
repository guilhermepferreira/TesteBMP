using Serilog;
using TesteBMP.Domain.Adapters;
using TesteBMP.Domain.Models;
using TesteBMP.Domain.Service;

namespace TesteBMP
{
    public class ServiceBMP : IServiceBMP
    {
        private readonly IApiAdapter _apiAdapter;
        private readonly IDbAdapter _dbAdapter;

        public ServiceBMP(IApiAdapter apiAdapter, IDbAdapter dbAdapter)
        {
            _apiAdapter = apiAdapter ?? 
                throw new ArgumentNullException(nameof(apiAdapter));
            _dbAdapter = dbAdapter ?? 
                throw new ArgumentNullException(nameof(dbAdapter));
        }

        public async Task SaveIsin(List<string> isin)
        {
            if(isin == null)
            {
                Log.Error("Isin is null");
                return;
            }

            foreach (var item in isin)
            {
                try
                {
                    if (string.IsNullOrEmpty(item) || item.Length != 12)
                    {
                        Log.Error($"Isin is null or empty or wrong size {item}");
                        continue;
                    }

                    var price = await _apiAdapter.GetPriceIsin(item);
                    var isinModel = new IsinModel
                    {
                        Price = price,
                        Isin = item
                    };
                    await _dbAdapter.SaveInDataBase(isinModel);
                }
                catch (Exception ex)
                {
                    Log.Error(ex,"messageTemplate");
                }
            }
        }
    }
}
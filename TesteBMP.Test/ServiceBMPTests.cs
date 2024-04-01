using Moq;
using TesteBMP.Domain.Adapters;
using TesteBMP.Domain.Models;
using TesteBMP.Domain.Service;
using Xunit;

namespace TesteBMP.Test
{
    public class ServiceBMPTests
    {
        private readonly Mock<IDbAdapter> mockDBAdapter;
        private readonly Mock<IApiAdapter> mockApiAdapter;
        private readonly IServiceBMP serviceBMP;
        public ServiceBMPTests()
        {
            mockDBAdapter = new Mock<IDbAdapter>();
            mockApiAdapter = new Mock<IApiAdapter>();
            serviceBMP = new ServiceBMP(mockApiAdapter.Object, mockDBAdapter.Object);
        }


        [Fact]
        public async void SuccessTest_SaveInDataBase()
        {

            var isinList = new List<string>()
            {
                "123456789012",
                "123456789012",
                "123456789012",
                "123456789012",
                "123456789012",
                "123456789012",
                "123456789012",
                "123456789012",
                "123456789012",
                "123456789012",
                "123456789012",
                "123456789012",
                "123456789012",
            };

            mockApiAdapter.Setup(x => x.GetPriceIsin(It.IsAny<string>())).ReturnsAsync(10)
                .Callback<string>(x => Assert.Equal("123456789012", x));

            mockDBAdapter.Setup(x => x.SaveInDataBase(It.IsAny<IsinModel>())).Returns(Task.CompletedTask);

            await serviceBMP.SaveIsin(isinList);
        }
    }
}

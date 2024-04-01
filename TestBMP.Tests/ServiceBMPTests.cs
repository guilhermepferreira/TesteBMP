using Moq;
using TesteBMP.Domain.Adapters;
using TesteBMP.Domain.Models;
using TesteBMP.Domain.Service;

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

            mockApiAdapter.Verify(x => x.GetPriceIsin(It.IsAny<string>()), Times.Exactly(13));
            mockDBAdapter.Verify(x => x.SaveInDataBase(It.IsAny<IsinModel>()), Times.Exactly(13));

        }

        [Fact]
        public async void ErrorTest_SaveInDataBase()
        {
            var isinList = new List<string>();

            mockApiAdapter.Verify(x => x.GetPriceIsin(It.IsAny<string>()), Times.Never);
            mockDBAdapter.Verify(x => x.SaveInDataBase(It.IsAny<IsinModel>()), Times.Never);
            
            await serviceBMP.SaveIsin(isinList);

        }

        [Fact]
        public async void ErrorTest_IncorrectlyFormattedIsin()
        {
            var isinList = new List<string>()
            { "123123123"};

           mockApiAdapter.Verify(x => x.GetPriceIsin(It.IsAny<string>()), Times.Never);
           mockDBAdapter.Verify(x => x.SaveInDataBase(It.IsAny<IsinModel>()), Times.Never);
    
           await serviceBMP.SaveIsin(isinList);
        }

        [Fact]
        public async void ErrorTest_ApiAdapterResponse()
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

            mockApiAdapter.Setup(x => x.GetPriceIsin(It.IsAny<string>())).Throws<Exception>();

            mockDBAdapter.Verify(x => x.SaveInDataBase(It.IsAny<IsinModel>()), Times.Never);

            mockApiAdapter.Verify(x => x.GetPriceIsin(It.IsAny<string>()), Times.Never);

            await serviceBMP.SaveIsin(isinList);

        }

        [Fact]
        public async void ErrorTest_DBAdapterResponse()
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

            mockDBAdapter.Setup(x => x.SaveInDataBase(It.IsAny<IsinModel>())).Throws<Exception>();

            mockApiAdapter.Verify(x => x.GetPriceIsin(It.IsAny<string>()), Times.Never);
            mockDBAdapter.Verify(x => x.SaveInDataBase(It.IsAny<IsinModel>()), Times.Never);

            await serviceBMP.SaveIsin(isinList);

        }

        [Fact]
        public async void ErrorTest_EmptyIsinList()
        {
            List<string> isinList = null;


            mockApiAdapter.Verify(x => x.GetPriceIsin(It.IsAny<string>()), Times.Never);
            mockDBAdapter.Verify(x => x.SaveInDataBase(It.IsAny<IsinModel>()), Times.Never);

            await serviceBMP.SaveIsin(isinList);
        }
    }
}
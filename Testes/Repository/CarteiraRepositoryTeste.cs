using Moq;
using Moq.AutoMock;
using TechChallenge_Application.DTOs;
using TechChallenge_Application.Interfaces;
using TechChallenge_Application.Services.Carteira;
using TechChallenge_Domain.Entities;
using TechChallenge_Domain.Interfaces;

namespace Testes.Repository
{
    public class CarteiraRepositoryTeste
    {
        private readonly AutoMocker _mocker;
        private readonly Mock<ICarteiraUseCase> _mockCarteiraUseCase;
        private readonly Mock<ICarteiraRepository> _mockCarteiraRepository;
        private readonly Mock<IAcaoRepository> _mockAcaoRepository;
        private readonly Mock<IServiceBusUseCase> _mockServiceBusUseCase;


        public CarteiraRepositoryTeste() 
        {
            _mocker = new AutoMocker();
            _mockCarteiraRepository = new Mock<ICarteiraRepository>();
            _mockCarteiraUseCase = new Mock<ICarteiraUseCase>();
            _mockServiceBusUseCase = new Mock<IServiceBusUseCase>();
            _mockAcaoRepository = new Mock<IAcaoRepository>();
        }

        [Fact]
        public void AdicionarValorCarteiraTeste()
        {
            //Arrange

            //Act
            _mockCarteiraRepository.Setup(x => x.AdicionarValorCarteira(It.IsAny<int>(), It.IsAny<float>()));
           
            //Assert
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetCarteiraByUsuarioIDTeste(int id)
        {
            //Arrange
            var service = new CarteiraUseCase(_mockCarteiraRepository.Object,
                _mockServiceBusUseCase.Object,
                _mockAcaoRepository.Object);

            _mockCarteiraRepository.Setup(x => x.GetCarteiraByUsuarioID(It.IsAny<int>())).Returns(new Carteira(id, id, 1000));

            //Act
            var carteira = service.GetCarteiraByUsuarioID(id);

            
            //Assert
            Assert.NotNull(carteira.Id);
            Assert.Equal(carteira.Id, id);
            
        }

        [Theory]
        [InlineData(1, 150)]
        [InlineData(1, 1500)]
        [InlineData(1, 9999)]
        public void RemoverSaldoTeste(int carteiraId, float saldoRemover)
        {
            //Arrange
            var service = new CarteiraUseCase(_mockCarteiraRepository.Object,
                _mockServiceBusUseCase.Object,
                _mockAcaoRepository.Object);
            var carteira = new Carteira(1, 1, 20000);
            _mockCarteiraRepository.Setup(x => x.RemoverValorCarteira(It.IsAny<int>(), It.IsAny<float>())).Returns(true);

            //Assert

        }


        [Theory]
        [InlineData(1, 1, 150)]
        [InlineData(2, 1, 1500)]
        [InlineData(3, 3, 9999)]
        public void ComprarAcoesTeste(int IdUsuario, int IdAcao, int quantidade)
        {
            var service = new CarteiraUseCase(_mockCarteiraRepository.Object,
                _mockServiceBusUseCase.Object,
                _mockAcaoRepository.Object);
            _mockCarteiraRepository.Setup(x => x.ComprarAcoes(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(),It.IsAny<bool>()));
        }

        [Theory]
        [InlineData(1, 1, 150)]
        [InlineData(2, 1, 1500)]
        [InlineData(3, 3, 9999)]
        public void VenderAcoesTeste(int IdUsuario, int IdAcao, int quantidade)
        {
            var service = new CarteiraUseCase(_mockCarteiraRepository.Object,
                _mockServiceBusUseCase.Object,
                _mockAcaoRepository.Object);
            _mockCarteiraRepository.Setup(x => x.VenderAcoes(It.IsAny<int>(),It.IsAny<int>(), It.IsAny<int>()));
        }
    }
}

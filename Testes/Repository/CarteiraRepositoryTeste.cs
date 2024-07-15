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

        private readonly List<Carteira> carteiras;


        public CarteiraRepositoryTeste() 
        {
            _mocker = new AutoMocker();
            _mockCarteiraRepository = new Mock<ICarteiraRepository>();
            _mockCarteiraUseCase = new Mock<ICarteiraUseCase>();
            _mockServiceBusUseCase = new Mock<IServiceBusUseCase>();
            _mockAcaoRepository = new Mock<IAcaoRepository>();

            carteiras = new List<Carteira>();

            carteiras.Add(new Carteira { Id = 1, Saldo = 10000, UsuarioId = 1 });
            carteiras.Add(new Carteira { Id = 2, Saldo = 20000, UsuarioId = 2 });
        }

        [Theory]
        [InlineData(1, 150)]
        [InlineData(1, 1500)]
        [InlineData(1, 9999)]
        public void AdicionarValorCarteiraTeste(int id, float valor)
        {
            //Arrange
            var service = new CarteiraUseCase(_mockCarteiraRepository.Object,
                _mockServiceBusUseCase.Object,
                _mockAcaoRepository.Object);


            var carteira = carteiras.First(x => x.Id == id);


            //Act
            carteira.Saldo += valor;

            //Assert
            Assert.NotNull(carteira.Id);

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

            _mockCarteiraRepository.Setup(x => x.GetCarteiraByUsuarioID(It.IsAny<int>())).Returns(carteiras.First(x=> x.Id == id));


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


            var carteira = carteiras.First(x => x.Id == carteiraId);

            if (carteiras.First(x => x.Id == carteiraId) != null)
                _mockCarteiraRepository.Setup(x => x.RemoverValorCarteira(It.IsAny<int>(), It.IsAny<float>())).Returns(true);
            else
                _mockCarteiraRepository.Setup(x => x.RemoverValorCarteira(It.IsAny<int>(), It.IsAny<float>())).Returns(false);

            if (service.RemoverValorCarteira(1, 1500) == true)
                carteira.Saldo -= saldoRemover;



            //Assert
            Assert.NotNull(carteira.Id);
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

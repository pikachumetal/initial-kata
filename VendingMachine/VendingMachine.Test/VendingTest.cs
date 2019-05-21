using FluentAssertions;
using VendingMachine.App.Biz;
using VendingMachine.App.Exceptions;
using Xunit;

namespace VendingMachine.Test
{
    public class VendingTest
    {
        private readonly Vending _vending;

        public VendingTest()
        {
            _vending = new Vending();
            _vending.Reset();
        }

        [Fact]
        public void When__Reset__Then_Initial_State()
        {
            // Arrange

            // Act
            _vending.Reset();

            // Assert
            _vending.Product.Should().Be(ProductEnum.None);
            _vending.Credit.Should().Be(0);
        }

        [Fact]
        public void Given_NoProducts__When_Buy__Then_VendingException()
        {
            // Arrange

            // Act

            // Assert
            _vending
                .Invoking(o => o.BuyAndRefund())
                .Should().Throw<VendingException>()
                .WithMessage($"{VendorErrorEnum.NoProduct}");
        }

        [Fact]
        public void When_Product_Then_ProductIsSelected()
        {
            // Arrange
            var productName = ProductEnum.Candy;

            // Act
            _vending.Select(productName);

            // Assert
            _vending.Product.Should().Be(productName);
        }

        [Fact]
        public void When_ProductNoExist_Then_VendingException()
        {
            // Arrange
            const string productName = "Other";

            // Act

            // Assert
            _vending
                .Invoking(o => o.Select(productName))
                .Should().Throw<VendingException>()
                .WithMessage($"{VendorErrorEnum.ProductNoAvailable}");
        }

        [Fact]
        public void Given_ProductSelected_When_BuyWithNoEnoughCredit_Then_VendingException()
        {
            // Arrange

            // Act
            _vending.Select(ProductEnum.Candy);

            // Assert
            _vending
                .Invoking(o => o.BuyAndRefund())
                .Should().Throw<VendingException>()
                .WithMessage($"{VendorErrorEnum.NoEnoughCredit}");
        }
        
        [Fact]
        public void When_CoinNoExist_Then_VendingException()
        {
            // Arrange
            const string coin = "TWO_POUNDS";

            // Act

            // Assert
            _vending
                .Invoking(o => o.AddCoin(coin))
                .Should().Throw<VendingException>()
                .WithMessage($"{VendorErrorEnum.NoValidCoin}");
        }

        [Fact]
        public void When_ValidCoin_Then_CreditIsUpdated()
        {
            // Arrange
            _vending.AddCoin(CoinEnum.OnePence);

            // Act
            _vending.AddCoin(CoinEnum.OnePound);

            // Assert
            _vending.Credit.Should().Be(1.01M);
        }
        
        [Fact]
        public void Given_CandyWithCredit_When_Buy_Then_ReturnRefund()
        {
            // Arrange
            _vending.AddCoin(CoinEnum.OnePound);
            _vending.AddCoin(CoinEnum.OnePound);

            _vending.Select(ProductEnum.Coke);

            // Act
            var refund = _vending.BuyAndRefund();

            // Assert
            refund.Should().Be(0.5M);
            _vending.Credit.Should().Be(0);
            _vending.Product.Should().Be(ProductEnum.None);
        }
        
        [Fact]
        public void Given_Credit_When_Refund_Then_ReturnRefund()
        {
            // Arrange
            _vending.AddCoin(CoinEnum.OnePound);
            _vending.AddCoin(CoinEnum.OnePound);
            
            // Act
            var refund = _vending.Refund();

            // Assert
            refund.Should().Be(2M);
            _vending.Credit.Should().Be(0);
            _vending.Product.Should().Be(ProductEnum.None);
        }
        
        [Fact]
        public void Given_NoCredit_When_Refund_Then_ReturnZero()
        {
            // Arrange
            
            // Act
            var refund = _vending.Refund();

            // Assert
            refund.Should().Be(0);
            _vending.Credit.Should().Be(0);
            _vending.Product.Should().Be(ProductEnum.None);
        }
    }
}

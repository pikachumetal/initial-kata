using System.Collections.Generic;
using VendingMachine.App.Exceptions;

namespace VendingMachine.App.Biz
{
    public class Vending
    {
        public decimal Credit { get; private set; } = 0;
        public string Product { get; private set; } = "";
        
        private readonly IDictionary<string, decimal> _products;
        private readonly IDictionary<string, decimal> _coins;

        public Vending()
        {
            _products = LoadInitialProducts();
            _coins = LoadAvailableCoins();
        }

        private static IDictionary<string, decimal> LoadInitialProducts()
        {
            var result = new Dictionary<string, decimal>
            {
                {ProductEnum.BottleWater, 1M},
                {ProductEnum.Coke, 1.5M},
                {ProductEnum.Nuts, 0.75M},
                {ProductEnum.Snack, 0.5M},
                {ProductEnum.Candy, 0.1M}
            };
            return result;
        }

        private static IDictionary<string, decimal> LoadAvailableCoins()
        {
            var result = new Dictionary<string, decimal>
            {
                {CoinEnum.OnePound, 1M},
                {CoinEnum.FiftyPence, 0.5M},
                {CoinEnum.TwentyPence, 0.2M},
                {CoinEnum.FivePence, 0.05M},
                {CoinEnum.OnePence, 0.01M}
            };
            return result;
        }

        public decimal BuyAndRefund()
        {
            if (string.IsNullOrWhiteSpace(Product)) throw new VendingException(VendorErrorEnum.NoProduct);

            var price = _products[Product];
            if (price > Credit) throw new VendingException(VendorErrorEnum.NoEnoughCredit);

            var refund = Credit - price;
            Reset();

            return refund;
        }

        public void Reset()
        {
            Product = ProductEnum.None;
            Credit = 0;
        }

        public void Select(string productName)
        {
            if (!_products.ContainsKey(productName)) throw new VendingException(VendorErrorEnum.ProductNoAvailable);
            Product = productName;
        }

        public void AddCoin(string coinName)
        {
            if (!_coins.ContainsKey(coinName)) throw new VendingException(VendorErrorEnum.NoValidCoin);
            var coinValue = _coins[coinName];
            Credit += coinValue;
        }

        public decimal Refund()
        {
            var refund = Credit;
            Reset();
            return refund;
        }
    }
}

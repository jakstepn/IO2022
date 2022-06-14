using System;

namespace ShopModule.Generators
{
    public class CurrencyGenerator
    {
        private static CurrencyGenerator _instance;
        private Random _random;

        private CurrencyGenerator()
        {
            _random = new Random();
        }

        public static CurrencyGenerator GetGenerator()
        {
            if (_instance == null) _instance = new CurrencyGenerator();
            return _instance;
        }

        public string GenerateCurrency()
        {
            int v = _random.Next();
            string[] currencies = { "PLN", "USD", "CZK", "GBP", "EUR" };
            return currencies[v % currencies.Length];
        }
    }
}

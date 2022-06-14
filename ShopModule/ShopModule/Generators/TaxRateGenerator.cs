using ShopModule.Models;
using System;

namespace ShopModule.Generators
{
    public class TaxRateGenerator
    {
        private static TaxRateGenerator _instance;
        private Random _random;

        private TaxRateGenerator()
        {
            _random = new Random();
        }

        public static TaxRateGenerator GetGenerator()
        {
            if(_instance == null) _instance = new TaxRateGenerator();
            return _instance;
        }

        public decimal GenerateTaxRate()
        {
            var mintax = StaticData.minTaxRate;
            var maxtax = StaticData.maxTaxRate;
            return _random.Next(mintax, maxtax);
        }
    }
}

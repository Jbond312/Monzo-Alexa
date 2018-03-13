using System;

namespace MonzoAlexa.Helpers
{
    public class CurrencyHelper
    {
        public static CurrencyAmount GetAmountString(int poundsPence)
        {
            var amount = string.Empty;

            var amountParts = (poundsPence / (double) 100).ToString().Split(".");

            var major = Convert.ToInt32(amountParts[0]);

            var minorAmount = "0";

            if (amountParts.Length == 2)
            {
                minorAmount = amountParts[1];
            }
            var minor = Convert.ToInt32(minorAmount);

            if (amountParts.Length == 2 && amountParts[1].Length == 1 && !amountParts[1].Contains("0"))
            {
                minor = minor * 10;
            }

            if (major != 0)
            {
                amount += $"{major} pound";

                if (major > 1)
                {
                    amount += "s";
                }
            }

            if (major != 0 && minor != 0)
            {
                amount += $" and {minor} pence";
            }

            if (major == 0 && minor != 0)
            {
                amount = $"{minor} pence";
            }

            return new CurrencyAmount
            {
                Amount = amount,
                Major = major,
                Minor = minor
            };
        }
    }
}

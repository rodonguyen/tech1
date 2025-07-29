using Converter.Constants;

namespace Converter.Services;

public class ConvertService
{
    public string ConvertCurrencyAmountToWords(decimal number)
    {
        ValidateInput(number);

        // Convert to words
        long integerPart = (long)number;
        long decimalPart = (long)((number - integerPart) * 100);

        string integerPartWords = ConvertIntegerToWords(integerPart);
        string decimalPartWords = ConvertIntegerToWords(decimalPart);

        integerPartWords = integerPartWords.Trim();
        decimalPartWords = decimalPartWords.Trim();

        string dollars = integerPart > 1 ? "DOLLARS" : "DOLLAR";
        string cents = decimalPart > 1 ? "CENTS" : "CENT";

        if (decimalPart > 0 && integerPart > 0)
            return $"{integerPartWords} {dollars} AND {decimalPartWords} {cents}";
        else if (integerPart > 0 && decimalPart == 0)
            return $"{integerPartWords} {dollars}";
        else if (integerPart == 0 && decimalPart > 0)
            return $"{decimalPartWords} {cents}";
        else
            return $"{NumberToWordsConstants.ZERO} {dollars}";
    }

    private string ConvertIntegerToWords(long aLong)
    {
        string result = "";

        // Convert trillions, billions, millions, thousands
        if (aLong >= Numbers.ONE_TRILLION)
        {
            int trillions = (int)(aLong / Numbers.ONE_TRILLION);
            result += ConvertIntegerToWords(trillions) + " TRILLION";
            aLong %= Numbers.ONE_TRILLION;
        }
        if (aLong >= Numbers.ONE_BILLION)
        {
            int billions = (int)(aLong / Numbers.ONE_BILLION);
            result += ConvertIntegerToWords(billions) + " BILLION";
            aLong %= Numbers.ONE_BILLION;
        }
        if (aLong >= Numbers.ONE_MILLION)
        {
            int millions = (int)(aLong / Numbers.ONE_MILLION);
            result += ConvertIntegerToWords(millions) + " MILLION";
            aLong %= Numbers.ONE_MILLION;
        }
        if (aLong >= Numbers.ONE_THOUSAND)
        {
            int thousands = (int)(aLong / Numbers.ONE_THOUSAND);
            result += ConvertIntegerToWords(thousands) + " THOUSAND";
            aLong %= Numbers.ONE_THOUSAND;
        }

        // Convert hundreds, tens, and units
        if (aLong > 0)
        {
            int hundreds = (int)(aLong / Numbers.ONE_HUNDRED);
            if (hundreds > 0)
                result += " " + NumberToWordsConstants.Units[hundreds] + " HUNDRED";

            // Convert tens and units in 3 paths: 0-9, 10-19, 20-99
            int remainder = (int)(aLong % Numbers.ONE_HUNDRED);
            if (remainder >= 0 && remainder <= 9)
            {
                result += " " + NumberToWordsConstants.Units[remainder];
            }
            else if (remainder >= 10 && remainder <= 19)
            {
                result += " " + NumberToWordsConstants.Teens[remainder - 10];
            }
            else if (remainder >= 20 && remainder <= 99)
            {
                int tens = (int)(remainder / 10);
                result += " " + NumberToWordsConstants.Tens[tens];
                int ones = (int)(remainder % 10);
                result += " " + NumberToWordsConstants.Units[ones];
            }
        }
        return result;
    }

    private void ValidateInput(decimal number)
    {
        if (number < 0)
            throw new ArgumentException("Invalid input: Negative numbers are not allowed");

        string decimalPart =
            number.ToString().Split('.').Length > 1 ? number.ToString().Split('.')[1] : "";
        if (decimalPart.Length > 2)
            throw new ArgumentException(
                "Invalid input: Only 2 decimal places are allowed for currency amounts"
            );

        if (number >= Numbers.ONE_QUADRILLION)
            throw new ArgumentException(
                "Invalid input: Number is too large, please enter a number less than 1,000,000,000,000,000"
            );
    }
}

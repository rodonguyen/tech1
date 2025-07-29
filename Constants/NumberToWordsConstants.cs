namespace Converter.Constants
{
    public static class NumberToWordsConstants
    {
        public static readonly string[] Units =
        {
            "", // 0 unit does not need a conversion
            "ONE",
            "TWO",
            "THREE",
            "FOUR",
            "FIVE",
            "SIX",
            "SEVEN",
            "EIGHT",
            "NINE"
        };
        public static readonly string[] Teens =
        {
            "TEN",
            "ELEVEN",
            "TWELVE",
            "THIRTEEN",
            "FOURTEEN",
            "FIFTEEN",
            "SIXTEEN",
            "SEVENTEEN",
            "EIGHTEEN",
            "NINETEEN"
        };
        public static readonly string[] Tens =
        {
            "", // 0 ten does not need a conversion
            "", // 10 does not need a conversion
            "TWENTY",
            "THIRTY",
            "FORTY",
            "FIFTY",
            "SIXTY",
            "SEVENTY",
            "EIGHTY",
            "NINETY"
        };
        public static readonly string[] Thousands =
        {
            "", // 0 thousand does not need a conversion
            "THOUSAND",
            "MILLION",
            "BILLION",
            "TRILLION"
        };
    }
}

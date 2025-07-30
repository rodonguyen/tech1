using System.Globalization;
using Converter.Services;
using FluentAssertions;
using Xunit;

namespace Converter.Tests.Services;

public class ConvertServiceTests
{
    private readonly ConvertService _convertService;

    public ConvertServiceTests()
    {
        _convertService = new ConvertService();
    }

    #region Basic components

    [Theory]
    [InlineData("0", "ZERO DOLLAR")]
    [InlineData("1", "ONE DOLLAR")]
    [InlineData("2", "TWO DOLLARS")]
    [InlineData("3", "THREE DOLLARS")]
    [InlineData("4", "FOUR DOLLARS")]
    [InlineData("5", "FIVE DOLLARS")]
    [InlineData("6", "SIX DOLLARS")]
    [InlineData("7", "SEVEN DOLLARS")]
    [InlineData("8", "EIGHT DOLLARS")]
    [InlineData("9", "NINE DOLLARS")]
    public void ConvertToWords_SingleDigits_ReturnsCorrectWords(string input, string expected)
    {
        var result = _convertService.ConvertCurrencyAmountToWords(
            decimal.Parse(input, CultureInfo.InvariantCulture)
        );
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("10", "TEN DOLLARS")]
    [InlineData("11", "ELEVEN DOLLARS")]
    [InlineData("12", "TWELVE DOLLARS")]
    [InlineData("13", "THIRTEEN DOLLARS")]
    [InlineData("14", "FOURTEEN DOLLARS")]
    [InlineData("15", "FIFTEEN DOLLARS")]
    [InlineData("16", "SIXTEEN DOLLARS")]
    [InlineData("17", "SEVENTEEN DOLLARS")]
    [InlineData("18", "EIGHTEEN DOLLARS")]
    [InlineData("19", "NINETEEN DOLLARS")]
    public void ConvertToWords_TeenNumbers_ReturnsCorrectWords(string input, string expected)
    {
        var result = _convertService.ConvertCurrencyAmountToWords(
            decimal.Parse(input, CultureInfo.InvariantCulture)
        );
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("20", "TWENTY DOLLARS")]
    [InlineData("30", "THIRTY DOLLARS")]
    [InlineData("40", "FORTY DOLLARS")]
    [InlineData("50", "FIFTY DOLLARS")]
    [InlineData("60", "SIXTY DOLLARS")]
    [InlineData("70", "SEVENTY DOLLARS")]
    [InlineData("80", "EIGHTY DOLLARS")]
    [InlineData("90", "NINETY DOLLARS")]
    public void ConvertToWords_TensNumbers_ReturnsCorrectWords(string input, string expected)
    {
        var result = _convertService.ConvertCurrencyAmountToWords(
            decimal.Parse(input, CultureInfo.InvariantCulture)
        );
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("1000", "ONE THOUSAND DOLLARS")]
    [InlineData("1000000", "ONE MILLION DOLLARS")]
    [InlineData("1000000000", "ONE BILLION DOLLARS")]
    [InlineData("1000000000000", "ONE TRILLION DOLLARS")]
    public void ConvertToWords_ThousandsNumbers_ReturnsCorrectWords(string input, string expected)
    {
        var result = _convertService.ConvertCurrencyAmountToWords(
            decimal.Parse(input, CultureInfo.InvariantCulture)
        );
        result.Should().Be(expected);
    }
    #endregion

    #region Complex numbers
    [Theory]
    [InlineData(
        "1234567890",
        "ONE BILLION TWO HUNDRED THIRTY FOUR MILLION FIVE HUNDRED SIXTY SEVEN THOUSAND EIGHT HUNDRED NINETY DOLLARS"
    )]
    [InlineData(
        "922345678901024.33",
        "NINE HUNDRED TWENTY TWO TRILLION THREE HUNDRED FORTY FIVE BILLION SIX HUNDRED SEVENTY EIGHT "
            + "MILLION NINE HUNDRED ONE THOUSAND TWENTY FOUR DOLLARS AND THIRTY THREE CENTS"
    )]
    [InlineData(
        "999898989898989.09",
        "NINE HUNDRED NINETY NINE TRILLION EIGHT HUNDRED NINETY EIGHT BILLION NINE HUNDRED EIGHTY NINE MILLION "
            + "EIGHT HUNDRED NINETY EIGHT THOUSAND NINE HUNDRED EIGHTY NINE DOLLARS AND NINE CENTS"
    )]
    [InlineData("0.12", "TWELVE CENTS")]
    [InlineData("1.55", "ONE DOLLAR AND FIFTY FIVE CENTS")]
    [InlineData("4.12", "FOUR DOLLARS AND TWELVE CENTS")]
    [InlineData("123.45", "ONE HUNDRED TWENTY THREE DOLLARS AND FORTY FIVE CENTS")]
    [InlineData("421.09", "FOUR HUNDRED TWENTY ONE DOLLARS AND NINE CENTS")]
    [InlineData("934.01", "NINE HUNDRED THIRTY FOUR DOLLARS AND ONE CENT")]
    public void ConvertToWords_ComplexNumbers_ReturnsCorrectWords(string input, string expected)
    {
        var result = _convertService.ConvertCurrencyAmountToWords(
            decimal.Parse(input, CultureInfo.InvariantCulture)
        );
        result.Should().Be(expected);
    }
    #endregion

    #region Invalid inputs
    [Theory]
    [InlineData("-4.44")] // Negative numbers
    [InlineData("0.005")] // Does not allow 3rd decimal place
    [InlineData("1144000000000000")] // Too large number
    [InlineData("1000000000000000")] // Too large number
    public void ConvertToWords_InvalidInputs_ThrowsException(string input)
    {
        var act = () =>
            _convertService.ConvertCurrencyAmountToWords(
                decimal.Parse(input, CultureInfo.InvariantCulture)
            );
        act.Should().Throw<ArgumentException>().WithMessage("Invalid input: *");
    }
    #endregion
}

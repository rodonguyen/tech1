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
    [InlineData(0, "ZERO DOLLAR")]
    [InlineData(1, "ONE DOLLAR")]
    [InlineData(2, "TWO DOLLARS")]
    [InlineData(3, "THREE DOLLARS")]
    [InlineData(4, "FOUR DOLLARS")]
    [InlineData(5, "FIVE DOLLARS")]
    [InlineData(6, "SIX DOLLARS")]
    [InlineData(7, "SEVEN DOLLARS")]
    [InlineData(8, "EIGHT DOLLARS")]
    [InlineData(9, "NINE DOLLARS")]
    public void ConvertToWords_SingleDigits_ReturnsCorrectWords(decimal input, string expected)
    {
        var result = _convertService.ConvertCurrencyAmountToWords(input);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(10, "TEN DOLLARS")]
    [InlineData(11, "ELEVEN DOLLARS")]
    [InlineData(12, "TWELVE DOLLARS")]
    [InlineData(13, "THIRTEEN DOLLARS")]
    [InlineData(14, "FOURTEEN DOLLARS")]
    [InlineData(15, "FIFTEEN DOLLARS")]
    [InlineData(16, "SIXTEEN DOLLARS")]
    [InlineData(17, "SEVENTEEN DOLLARS")]
    [InlineData(18, "EIGHTEEN DOLLARS")]
    [InlineData(19, "NINETEEN DOLLARS")]
    public void ConvertToWords_TeenNumbers_ReturnsCorrectWords(decimal input, string expected)
    {
        var result = _convertService.ConvertCurrencyAmountToWords(input);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(20, "TWENTY DOLLARS")]
    [InlineData(30, "THIRTY DOLLARS")]
    [InlineData(40, "FORTY DOLLARS")]
    [InlineData(50, "FIFTY DOLLARS")]
    [InlineData(60, "SIXTY DOLLARS")]
    [InlineData(70, "SEVENTY DOLLARS")]
    [InlineData(80, "EIGHTY DOLLARS")]
    [InlineData(90, "NINETY DOLLARS")]
    public void ConvertToWords_TensNumbers_ReturnsCorrectWords(decimal input, string expected)
    {
        var result = _convertService.ConvertCurrencyAmountToWords(input);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(1000, "ONE THOUSAND DOLLARS")]
    [InlineData(1000000, "ONE MILLION DOLLARS")]
    [InlineData(1000000000, "ONE BILLION DOLLARS")]
    [InlineData(1000000000000, "ONE TRILLION DOLLARS")]
    public void ConvertToWords_ThousandsNumbers_ReturnsCorrectWords(decimal input, string expected)
    {
        var result = _convertService.ConvertCurrencyAmountToWords(input);
        result.Should().Be(expected);
    }
    #endregion

    #region Complex numbers
    [Theory]
    [InlineData(
        1234567890,
        "ONE BILLION TWO HUNDRED THIRTY FOUR MILLION FIVE HUNDRED SIXTY SEVEN THOUSAND EIGHT HUNDRED NINETY DOLLARS"
    )]
    [InlineData(0.12, "TWELVE CENTS")]
    [InlineData(1.55, "ONE DOLLAR AND FIFTY FIVE CENTS")]
    [InlineData(4.12, "FOUR DOLLARS AND TWELVE CENTS")]
    [InlineData(421.09, "FOUR HUNDRED TWENTY ONE DOLLARS AND NINE CENTS")]
    [InlineData(934.01, "NINE HUNDRED THIRTY FOUR DOLLARS AND ONE CENT")]
    public void ConvertToWords_ComplexNumbers_ReturnsCorrectWords(decimal input, string expected)
    {
        var result = _convertService.ConvertCurrencyAmountToWords(input);
        result.Should().Be(expected);
    }
    #endregion

    #region Invalid inputs
    [Theory]
    [InlineData(-4.44)] // Negative numbers
    [InlineData(0.005)] // Does not allow 3rd decimal place
    [InlineData(1144000000000000)] // Too large number
    [InlineData(1000000000000000)] // Too large number
    public void ConvertToWords_InvalidInputs_ThrowsException(decimal input)
    {
        var act = () => _convertService.ConvertCurrencyAmountToWords(input);
        act.Should().Throw<ArgumentException>().WithMessage("Invalid input: *");
    }
    #endregion
}

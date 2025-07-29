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
    [InlineData(0, "Zero Dollar")]
    [InlineData(1, "One Dollar")]
    [InlineData(2, "Two Dollars")]
    [InlineData(3, "Three Dollars")]
    [InlineData(4, "Four Dollars")]
    [InlineData(5, "Five Dollars")]
    [InlineData(6, "Six Dollars")]
    [InlineData(7, "Seven Dollars")]
    [InlineData(8, "Eight Dollars")]
    [InlineData(9, "Nine Dollars")]
    public void ConvertToWords_SingleDigits_ReturnsCorrectWords(decimal input, string expected)
    {
        var result = _convertService.ConvertCurrencyAmountToWords(input);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(10, "Ten Dollars")]
    [InlineData(11, "Eleven Dollars")]
    [InlineData(12, "Twelve Dollars")]
    [InlineData(13, "Thirteen Dollars")]
    [InlineData(14, "Fourteen Dollars")]
    [InlineData(15, "Fifteen Dollars")]
    [InlineData(16, "Sixteen Dollars")]
    [InlineData(17, "Seventeen Dollars")]
    [InlineData(18, "Eighteen Dollars")]
    [InlineData(19, "Nineteen Dollars")]
    public void ConvertToWords_TeenNumbers_ReturnsCorrectWords(decimal input, string expected)
    {
        var result = _convertService.ConvertCurrencyAmountToWords(input);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(20, "Twenty Dollars")]
    [InlineData(30, "Thirty Dollars")]
    [InlineData(40, "Forty Dollars")]
    [InlineData(50, "Fifty Dollars")]
    [InlineData(60, "Sixty Dollars")]
    [InlineData(70, "Seventy Dollars")]
    [InlineData(80, "Eighty Dollars")]
    [InlineData(90, "Ninety Dollars")]
    public void ConvertToWords_TensNumbers_ReturnsCorrectWords(decimal input, string expected)
    {
        var result = _convertService.ConvertCurrencyAmountToWords(input);
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(1000, "One Thousand Dollars")]
    [InlineData(1000000, "One Million Dollars")]
    [InlineData(1000000000, "One Billion Dollars")]
    [InlineData(1000000000000, "One Trillion Dollars")]
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
        "One Billion Two Hundred Thirty Four Million Five Hundred Sixty Seven Thousand Eight Hundred Ninety Dollars"
    )]
    [InlineData(0.12, "Twelve Cents")]
    [InlineData(1.55, "One Dollar And Fifty Five Cents")]
    [InlineData(4.12, "Four Dollars And Twelve Cents")]
    [InlineData(421.09, "Four Hundred Twenty One Dollars And Nine Cents")]
    [InlineData(934.01, "Nine Hundred Thirty Four Dollars And One Cent")]
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
    public void ConvertToWords_InvalidInputs_ThrowsException(decimal input)
    {
        var act = () => _convertService.ConvertCurrencyAmountToWords(input);
        act.Should().Throw<ArgumentException>().WithMessage("Invalid input");
    }
    #endregion
}

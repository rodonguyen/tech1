using Converter.Models;
using Converter.Services;
using Microsoft.AspNetCore.Mvc;

namespace Converter.Controllers;

public class ConvertController : Controller
{
    private readonly ConvertService _convertService;

    public ConvertController(ConvertService convertService)
    {
        _convertService = convertService;
    }

    [HttpGet]
    [Route("convert/{number}")]
    public ActionResult<NumberToWordsResult> ConvertApi(decimal number)
    {
        try
        {
            var words = _convertService.ConvertCurrencyAmountToWords(number);
            return Ok(new NumberToWordsResult { Number = number, Words = words });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}

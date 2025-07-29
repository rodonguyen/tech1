using Converter.Services;
using Microsoft.AspNetCore.Mvc;

namespace Converter.Controllers;

public class NumberToWordsController : Controller
{
    private readonly NumberToWordsService _numberToWordsService;

    public NumberToWordsController(NumberToWordsService numberToWordsService)
    {
        _numberToWordsService = numberToWordsService;
    }

    [HttpGet]
    [Route("convert/{number}")]
    public IActionResult ConvertApi(decimal number)
    {
        try
        {
            var result = _numberToWordsService.ConvertToWords(number);
            return Json(new { number = number, words = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}

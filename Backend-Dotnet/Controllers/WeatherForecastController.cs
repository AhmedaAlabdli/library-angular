using Bogus;
using Library1.Helpers;
using Library1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceReadingService _invoiceReadingService;
        public InvoiceController(InvoiceReadingService invoiceReadingService)
        {
            _invoiceReadingService = invoiceReadingService;
        }
        [HttpGet("GeneratePdf")]
        public ActionResult GeneratePdf() 
        {
            var invoice = new Faker<Invoice>()
                .RuleFor(i => i.InvoiceDate, f => f.Date.Recent(30))
                .RuleFor(i => i.InvoiceNumber, f => f.Random.Number(10000, 99999).ToString()).Generate();
            invoice.client = new Faker<Clinet>()
                .RuleFor(i => i.ClientName, f => f.Company.CompanyName())
                .RuleFor(i => i.ClientAddress, f => f.Address.FullAddress()).Generate();

            invoice.InvoiceItem = new();
            for (var i = 0; i < 15; i++)
            {
                invoice.InvoiceItem.Add(new Faker<InvoiceItem>()
                    .RuleFor(i => i.Description, f => f.Commerce.ProductName())
                    .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
                    .RuleFor(i => i.UnitPrice, f => decimal.Parse(f.Commerce.Price())).Generate());
            }

            var document = _invoiceReadingService.GenerateInvoicePdf(invoice);
            return File(document, "application/pdf", "invoice.pdf"); //storage place , file name
        }

        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //private readonly ILogger<WeatherForecastController> _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
    }
}

using CovidChart.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CovidChart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CovidsController : ControllerBase
    {
        private readonly CovidService _service;

        public CovidsController(CovidService covidService)
        {
            _service = covidService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveCovid(Covid covid)
        {
            await _service.SaveCovid(covid);
            
            //IQueryable<Covid> covidList = _service.GetList();
            

            return Ok(_service.GetCovidChartList());
        }

        [HttpGet]
        public IActionResult InitializeCovid()
        {
            Random rnd = new Random();

            Enumerable.Range(1, 10).ToList().ForEach(x =>//Bu ifade, 1 ila 10 arasında bir dizi tamsayı oluşturur, bu sayıları bir liste haline getirir ve her bir tamsayı için bir işlem yapar. Her tamsayı, x adlı değişkenle temsil edilir.
            {
                foreach (ECity item in Enum.GetValues(typeof(ECity)))
                {
                    var newcovid = new Covid { City = item, Count = rnd.Next(100, 1000), CovidDate = DateTime.Now.AddDays(x) };
                    _service.SaveCovid(newcovid).Wait();//Wait() metodu ise bu işlemin tamamlanmasını bekler. Bu, asenkron bir işlemi senkron hale getirir ve kayıt işleminin tamamlanmasını bekler.
                    Thread.Sleep(4000);//Bu satır, kodun 1 saniye boyunca duraklamasını sağlar. Yani, her Covid nesnesi kaydedildikten sonra 1 saniye beklenir, sonra bir sonraki Covid nesnesi oluşturulur ve kaydedilir.
                }
            });
            return Ok("Covid19 dataları veritabanına kaydedildi");
        }
    }
}

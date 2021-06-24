namespace MyCS.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Database;
    using InputModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Services;

    [ApiController]
    public class RestController : ControllerBase
    {
        private readonly ISeedService seedService;
        private readonly MyCsDbContext context;

        public RestController(ISeedService seedService, MyCsDbContext context)
        {
            this.seedService = seedService;
            this.context = context;
        }

        [HttpPost("csv/calculate")]
        [Consumes("multipart/form-data")]
        public async Task CalculateScoreCsv(CsvFile csv)
        {
            await this.seedService.SeedRecords(csv);
        }

        [HttpGet("test")]
        public async Task<ICollection<Client>> Test()
        {
            var results = await this.context.Clients.Where(x => x.Age_of_Applicant >= 70).ToListAsync();
            return results;
        }

        /*[HttpPost("manual/calculate")]
         public ActionResult CalculateScoreManual()
         {

         }*/
    }
}

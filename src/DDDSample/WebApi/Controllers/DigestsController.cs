using System.Threading.Tasks;
using ApplicationMediator.Dto._4Digests;
using ApplicationMediator.Services;
using Digests.Data.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DigestsController : ControllerBase
    {
        private readonly DigestsService _digestsService;



        public DigestsController( DigestsService digestsService)
        {
            _digestsService = digestsService;
        }



        #region Methods

        //GET api/Digests
        [HttpGet]
        public async Task<IActionResult> GetCompanys()
        {
            var companys = await _digestsService.GetCompanys();
            return new JsonResult(companys);
        }


        //POST api/Digests
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompanyDto companyDto)
        {
            var res= await _digestsService.AddNewCompany(companyDto);
            return res.IsFailure ? (IActionResult) BadRequest(res.Error) : Ok();
        }

        #endregion
    }
}
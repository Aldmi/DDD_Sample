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
        private readonly IUnitOfWorkDigests _unitOfWorkDigests;
        private readonly DigestsService _digestsService;



        public DigestsController(IUnitOfWorkDigests unitOfWorkDigests, DigestsService digestsService)
        {
            _unitOfWorkDigests = unitOfWorkDigests;
            _digestsService = digestsService;
        }



        // GET
        [HttpGet]
        public async Task<IActionResult> GetCompanys()
        {
            var companys = await _digestsService.GetCompanys();
            return new JsonResult(companys);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CompanyDto companyDto)
        {
           var res= await _digestsService.AddNewCompany(companyDto);
           return res.IsFailure ? (IActionResult) BadRequest(res.Error) : Ok();
        }
    }
}
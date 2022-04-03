using GenericApi.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GenericApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class GenericApiController : ControllerBase
    {
        private readonly IGenericApiService _genericApiService;

        public GenericApiController(IGenericApiService genericApiService)
        {
            _genericApiService = genericApiService;
        }

        //[HttpGet]
        //[ActionName("get")]
        //public async Task<IActionResult> Get(string asd)
        //{
        //    var data = await Task.Run(() => _genericApiService.GimmeAdvice(asd));

        //    return Ok(data);
        //} 
        
        //[Route("generic")]
        [HttpPatch]
        [ActionName("get")]
        public async Task<IActionResult>  GenericPicker(Dictionary<string, string> dic)
        {
            var data = await Task.Run(() => _genericApiService.GenericPicker(dic));

            return Ok(data);
        }
    }
}

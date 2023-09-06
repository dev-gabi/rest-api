using BL;
using Entities.Request;
using Entities.Response;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace JonInterview2022_08Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NamesController : ControllerBase
    {
        private readonly INameService _namesService;

        public NamesController(INameService namesService)
        {
            _namesService = namesService;
        }

        [HttpGet]
        public IActionResult GetNames()
        {
            NamesResponse response = _namesService.GetNamesWithPersonId();
            if (response.StatusCode != StatusCodes.Status200OK)
            {
                return Problem(detail: response.StatusDescription);
            }
            return Ok(response);
        }
        //todo: add get by id
        [HttpPost]
        public IActionResult AddName(AddNameReq name) {
            return Ok(_namesService.AddName(name));
        }

        [HttpPut]
        public IActionResult UpdateName(UpdateNameReq name)
        {
            return Ok(_namesService.UpdateName(name));
        }

       
        [HttpPatch("{id:int}")]
        public IActionResult UpdatePatchName([FromRoute]int id, [FromBody]JsonPatchDocument patch)
        {
            return Ok(_namesService.UpdatePatchName(id, patch));
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteName(int id)
        {
            return Ok(_namesService.DeleteName(id));
        }
    }
}

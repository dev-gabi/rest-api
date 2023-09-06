using BL;
using Entities.Request;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class NamesController : ControllerBase
    {
        private readonly NamesBl _bl;
        public NamesController(NamesBl bl)
        {
            _bl = bl;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bl.GetCoolNames());
        }
        //todo: add get by id
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostNameRequest name)
        {
            return Ok(await _bl.PostNameAsync(name));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PutNameRequest name)
        {
            return Ok(await _bl.PutNameAsync(id, name));
        }
        [HttpPatch]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument patch)
        {
            return Ok(await _bl.PatchNameAsync(id, patch));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _bl.DeleteNameAsync(id));
        }
    }
}

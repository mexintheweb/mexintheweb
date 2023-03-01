using mexintheweb.Models;
using mexintheweb.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace mexintheweb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MexloginController : ControllerBase
    {
        private ILoginService LoginService { get; }

        public MexloginController(ILoginService loginService)
        {
            LoginService = loginService;
        }

        // GET: api/<MexloginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MexloginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MexloginController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginModel login)
        {
            var result = new LoginResponseModel();
            var token = await LoginService.GetWebtokenByLogin(login);

            if(!string.IsNullOrWhiteSpace(token))
            {
                result.Username = login.Username;
                result.WebToken = token;                    
            }

            return Ok(result);
        }

        // PUT api/<MexloginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MexloginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

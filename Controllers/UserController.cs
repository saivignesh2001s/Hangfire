using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using Unconnectedwebapi.Models;
using Unconnectedwebapi.Repository;

namespace Unconnectedwebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsermethods method;
        public UserController(IUsermethods method)
        {
            this.method = method;
        }
        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var users = method.GetUsers();
            if (users != null)
            {
                BackgroundJob.Enqueue<IUsermethods>(x => x.sendmail("get",null));
                return Ok(users);
            }
            return NoContent();

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var users = method.GetUser(id);
            if (users != null) {
                BackgroundJob.Enqueue<IUsermethods>(x => x.sendmail("get",id));
              return  Ok(users);
               }     
               return NoContent();

        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]user value)
        {
            var users=method.postuser(value);
            if (users != null)
            {
                BackgroundJob.Enqueue<IUsermethods>(x => x.sendmail("post",value.id));
                return Ok(users);
            }
            else
            {
                return NoContent();
            }

        }
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody]user value)
        {
            bool k=method.updateuser(id, value);
            if (k)
            {
                BackgroundJob.Enqueue<IUsermethods>(x => x.sendmail("put",value.id));
                return Ok(k);
            }
            else
            {
                return NoContent(); 
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var k=method.deleteuser(id);
            if (k)
            {
                BackgroundJob.Enqueue<IUsermethods>(x => x.sendmail("delete",id));
                return Ok(k);
            }
            else
            {
                return NoContent();
            }
        }
            
    }
}

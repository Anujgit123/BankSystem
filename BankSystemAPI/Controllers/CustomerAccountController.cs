using BankSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAccountController : ControllerBase
    {
        // GET: api/<CustomerAccountController>
        [HttpGet]
        public IEnumerable<UserAccountModel> Get()
        {
            string jsonFile = @"C:\Users\anujy\source\repos\BankSystemAPI\DB.json";

            List<UserAccountModel>? items = new List<UserAccountModel>();
            string newJson = String.Empty;
            using (StreamReader r = new StreamReader(jsonFile))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<UserAccountModel>>(json);
            }

            //return View("CustomerAccount", items);
            return items;
            //return new string[] { "value1", "value2" };
        }

        // GET api/<CustomerAccountController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CustomerAccountController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CustomerAccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerAccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

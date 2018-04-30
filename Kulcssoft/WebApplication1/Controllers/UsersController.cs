using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            if (HttpContext.Session.GetString("KulcsSoft") != null)
            {
                var list = Program.service.GetUsers().Rows;
                return Json(list);
            }
            return null;
        }
        [HttpPost]
        public IActionResult Post([FromBody]string json)
        {
            if(HttpContext.Session.GetString("KulcsSoft") != null)
            {
                json = json.Substring(1, json.Length - 2);
                string[] items = json.Split(',');
                string email = items[1].Split(':')[1];
                if (EmailValidator(email))
                {
                    string name = items[0].Split(':')[1];
                    Program.service.AddUser(name, email);
                    return Json(Program.service.GetUser(email));
                }
            }
            return null;
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (HttpContext.Session.GetString("KulcsSoft") != null)
            {
                Program.service.DeletUser(id);
            }
        }
        private bool EmailValidator(string email)
        {
            string pattern = "[a-z0-9]+@[a-z]+[.][a-z]+";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("v1")]
        public async Task<ActionResult<dynamic>> Get([FromServices]DataContext context)
        {
            var employee = new User {Id = 1, UserName = "JoaoSilva", Password = "silva.joao", Role = "employee"};
            var manager = new User {Id = 2, UserName = "TonyStark", Password = "satrk.tony", Role = "manager"};
            var category = new Category{Id = 1, Title = "Periféricos"};
            var product = new Product{Id = 1, category = category, Description = "Teclado Mecanico", Price = 259};

            context.Users.Add(employee);
            context.Users.Add(manager);
            context.Categories.Add(category);
            context.Products.Add(product);
            await context.SaveChangesAsync();

            return Ok(new {message = "Dados Configurados"});
        }
    }
}
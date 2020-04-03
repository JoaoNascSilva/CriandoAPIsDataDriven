using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;


namespace Shop.Controllers
{
    [Route("products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> Get([FromServices]DataContext context)
        {
            //* .Include -> Inclui as Categorias na propriedade Category em Products (JOIN)
            var products = await context.Products.Include(x => x.category).AsNoTracking().ToListAsync();
            return products;
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> GetById([FromServices]DataContext context, int id)
        {
            //* .Include -> Inclui as Categorias na propriedade Category em Products (JOIN)
            var product = await context.Products.Include(x => x.category).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        [HttpGet] // products/categories/1 
        [Route("categories/{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> Get([FromServices]DataContext context, int id)
        {
            //* .Include -> Inclui as Categorias na propriedade Category em Products (JOIN)
            var products = await context
                                    .Products
                                    .Include(x => x.category)
                                    .AsNoTracking()
                                    .Where(x => x.CategoryId == id)
                                    .ToListAsync();

            if(products == null)
                return NotFound(new {message = "Produto(s) não encontrados pela categoria informarda."});

            return products;
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "employee")]
        public async Task<ActionResult<Product>> Post([FromServices]DataContext context, [FromBody]Product model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            context.Products.Add(model);
            await context.SaveChangesAsync();
            return model;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{

    //* EndPoint = URL
    //* http://localhost:5000
    //* https://localhost:5001
    //* https://meuapp.azurewebsites.com.br

    //* Routes 
    //* https://localhost:5001/category

    //* https://localhost:5001/category

    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get([FromServices]DataContext context)
        {
            var categories = await context.Categories.AsNoTracking().ToListAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> GetById(int id, [FromServices]DataContext context)
        {
            var categories = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (categories == null)
                return NotFound(new {message = "Categoria informada não foi encontrada."});

            return Ok(categories);            
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Post([FromBody]Category model, [FromServices]DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Categories.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (System.Exception e)
            {
                return BadRequest(new { message = $"Não foi possível gravar Categoria.\nError: {e.Message}" });
            }

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Category>>> Put(int id, [FromBody]Category model, [FromServices]DataContext context)
        {
            //Verifica se Id informado é o mesmo do modelo
            if (id != model.Id)
                return NotFound(new { message = "Categoria não encontrada." });

            // Verifica se os dados são válidos
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Category>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(new { message = $"Este registro já foi atualizado.\nError: {e.Message}" });
            }
            catch (System.Exception e)
            {
                return BadRequest(new { message = $"Não foi possível atualizar a categoria.\nError: {e.Message}" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Delete(int id, [FromServices]DataContext context)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound(new { message = "Categoria não encontrada." });

            try
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return Ok(new { message = "Categoria removida com sucesso." });
            }
            catch (System.Exception)
            {
                return BadRequest(new { message = "Não foi possível remover a categoria." });
            }
        }
    }
}
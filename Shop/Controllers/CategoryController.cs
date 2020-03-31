using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        [Route("Categories")]
        public string Get()
        {
            return "FUCK";
        }

        [HttpGet]
        [Route("Categories/{id:int}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            if (id == 1)
                return Ok();

            return NotFound();
        }

        [HttpPost]
        [Route("Categories")]
        public ActionResult<Category> Post([FromBody]Category model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }

        [HttpPut]
        [Route("Categories")]
        public async Task<ActionResult<List<Category>>> Put(int id, [FromBody]Category model) 
        {
            //Verifica se Id informado é o mesmo do modelo
            if (id != model.Id)
                return NotFound(new {message = "Categoria não encontrada."});

            // Verifica se os dados são válidos
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(model);
        }

        [HttpDelete]
        [Route("{Categories/{id:int}}")]
        public async Task<ActionResult<Category>> Delete()
        {
            return Ok();
        }
    }
}
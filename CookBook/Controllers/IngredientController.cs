using CookBookDAL.Data;
using CookBookDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientRepository ingredientRepository;

        public IngredientController(IIngredientRepository ingredientRepository)
        {
            this.ingredientRepository = ingredientRepository;
        }

        [HttpPost]
        public async Task<ActionResult<Ingredient>> CreateIngredient([FromBody] Ingredient ingredient)
        {
            try
            {
                if (ingredient == null)
                {
                    return BadRequest();
                }

                var result = await ingredientRepository.CreateIngredient(ingredient);
                return result;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new ingredient.");
            }
        }

        [HttpDelete("{ingredientId:int}")]
        public async Task<ActionResult<Ingredient>> DeleteIngredient([FromRoute] int ingredientId)
        {
            try
            {
                var result = await ingredientRepository.GetIngredient(ingredientId);

                if (result == null)
                {
                    return NotFound();
                }

                await ingredientRepository.DeleteIngredient(ingredientId);
                return Ok($"Ingredient with id={ingredientId} deleted");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting ingredient");
            }
        }

        [HttpGet("{ingredientId:int}")]
        public async Task<ActionResult<Ingredient>> GetIngredient(int ingredientId)
        {
            try
            {
                var result = await ingredientRepository.GetIngredient(ingredientId);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from databse");
            }

        }

        [HttpPut]
        public async Task<ActionResult<Ingredient>> UpdateIngredient([FromBody]Ingredient ingredient)
        {
            try
            {
                var ingredientToUpdate = await ingredientRepository.GetIngredient(ingredient.IngredientID);

                if(ingredientToUpdate == null)
                {
                    return NotFound();
                }

                var updatedIngredient = await ingredientRepository.UpdateIngredient(ingredient);

                return updatedIngredient;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating ingredient");
            }
        }
    }
}

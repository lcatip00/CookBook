using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBookDAL.Data;
using Microsoft.AspNetCore.Http;
using CookBookDAL.Models;
using CookBook.Factories;
using CookBook.Models;
using CookBook.Services;

namespace CookBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository recipeRepository;
        private readonly IRecipeModelFactory recipeModelFactory;
        private readonly IRecipeService recipeService;

        public RecipeController(IRecipeRepository RecipeRepository, IRecipeModelFactory recipeModelFactory, IRecipeService recipeService)
        {
            this.recipeRepository = RecipeRepository;
            this.recipeModelFactory = recipeModelFactory;
            this.recipeService = recipeService;
        }

        [HttpGet]
        public async Task<ActionResult> GetRecipes()
        {
            try
            {
                var recipe = await recipeRepository.GetRecipes();

                return Ok(recipe);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        [HttpGet("{recipeId:int}")]
        public async Task<ActionResult<RecipeModel>> GetRecipe(int recipeId)
        {
            try
            {
                var result = await recipeModelFactory.PrepareRecipeModel(recipeId);

                if(result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retriving data from the database errorcina={e.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Recipe>> CreateRecipe([FromBody]Recipe recipe)
        {
            try
            {
                if (recipe == null)
                {
                    return BadRequest();
                }
                var result = await recipeRepository.CreateRecipe(recipe);
                return CreatedAtAction(nameof(GetRecipe), new { id = result.RecipeId }, result);
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating new recipe error={e}");
            }


        }

        [HttpDelete("{recipeId:int}")]
        public async Task<ActionResult> DeleteRecipe([FromRoute]int recipeId)
        {
            try
            {
                var result = await recipeRepository.GetRecipe(recipeId);
                if(result == null)
                {
                    return NotFound($"Recipe with id={recipeId} not found");
                }

                await recipeRepository.DeleteRecipe(recipeId);
                return Ok($"Recipe with id={recipeId} deleted");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting recipe");
            }

        }

        [HttpPost("createRecipeModel")]
        public async Task<ActionResult> CreateRecipeModel([FromBody]RecipeModel recipeModel)
        {
            try
            {
                if(recipeModel == null)
                {
                    return NotFound();
                }

                var createdRecipe = await recipeService.CreateRecipeModel(recipeModel);

                return CreatedAtAction(nameof(GetRecipe), new { id = createdRecipe.RecipeId}, createdRecipe);
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating new recipe error={e}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Recipe>> UpdateRecipe([FromBody]Recipe recipe)
        {
            try
            {
                var result = await recipeRepository.GetRecipe(recipe.RecipeId);

                if (result == null)
                {
                    return BadRequest($"recipe with id={recipe.RecipeId} not found");
                }


                var newRecipe = await recipeRepository.UpdateRecipe(recipe);
                return Ok(newRecipe);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating recipe");
            }

        }
    }
}

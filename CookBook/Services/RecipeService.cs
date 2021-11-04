using CookBook.Factories;
using CookBook.Models;
using CookBookDAL;
using CookBookDAL.Data;
using CookBookDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly CookBookContext cookBookContext;
        private readonly IRecipeModelFactory recipeModelFactory;
        private readonly IStepRepository stepRepository;

        public RecipeService(CookBookContext cookBookContext, IRecipeModelFactory recipeModelFactory, IStepRepository stepRepository)
        {
            this.cookBookContext = cookBookContext;
            this.recipeModelFactory = recipeModelFactory;
            this.stepRepository = stepRepository;
        }
        public async Task<RecipeModel> CreateRecipeModel(RecipeModel recipeModel)
        {
            //create new recipe model
            var recipe = new Recipe
            {
                Title = recipeModel.Title,
                Description = recipeModel.Description,
                NumberOfServings = recipeModel.NumberOfServings,
                CategoryID = recipeModel.Category.CategoryId,
            };
            var resRecipe = await cookBookContext.Recipes.AddAsync(recipe);
            await cookBookContext.SaveChangesAsync();

            //get just created recipe id
            var id = resRecipe.Entity.RecipeId;

            //create recipe steps
            //var steps = new List<Step>();

            foreach (var step in recipeModel.Steps)
            {
                var s = new Step
                {
                    StepNumber = step.StepNumber,
                    StepDescription = step.StepDescription,
                    RecipeID = id
                };
                var addStep = await cookBookContext.Steps.AddAsync(s);
            }
            

            //save ingredients
            foreach (var ingredient in recipeModel.Ingredients)
            {
                var i = new Ingredient
                {
                    IngredientName = ingredient.IngredientName,
                    Amount = ingredient.Amount,
                    Measurment = ingredient.Measurment,
                    RecipeID = id,
                };
                var addIngredient = await cookBookContext.Ingredients.AddAsync(i);
            }

            //save all changes to the databasse
            await cookBookContext.SaveChangesAsync();

            //get new model 
            var newRecipeModel = await recipeModelFactory.PrepareRecipeModel(id);
            return newRecipeModel;

            //throw new NotImplementedException();
        }

        
    }
}

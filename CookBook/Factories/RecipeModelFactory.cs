using CookBook.Models;
using CookBookDAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Factories
{
    public class RecipeModelFactory : IRecipeModelFactory
    {
        private readonly IRecipeRepository recipeRepository;

        public RecipeModelFactory(IRecipeRepository recipeRepository)
        {
            this.recipeRepository = recipeRepository;
        }

        public async Task<RecipeModel> PrepareRecipeModel(int recipeId)
        {
            var recipe = await recipeRepository.GetRecipe(recipeId);

            if(recipe == null)
            {
                return null;
            }

            var recipeModel = new RecipeModel
            {
                RecipeId = recipe.RecipeId,
                Title = recipe.Title,
                Description = recipe.Description,
                NumberOfServings = recipe.NumberOfServings,
            };

            var categoryModel = new CategoryModel
            {
                CategoryId = recipe.Category.CategoryId,
                CategoryName = recipe.Category.CategoryName
            };

            recipeModel.Category = categoryModel;

            foreach (var step in recipe.Steps)
            {
                var stepModel = new StepModel
                {
                    StepId = step.StepId,
                    StepNumber = step.StepNumber,
                    StepDescription = step.StepDescription,
                };

                recipeModel.Steps.Add(stepModel);
            }

            foreach (var ingredient in recipe.Ingredients)
            {
                var ingredientModel = new IngredientModel
                {
                    IngredientID = ingredient.IngredientID,
                    IngredientName = ingredient.IngredientName,
                    Amount = ingredient.Amount,
                    Measurment = ingredient.Measurment
                };

                recipeModel.Ingredients.Add(ingredientModel);
            }

            return recipeModel;
        }
    }
}

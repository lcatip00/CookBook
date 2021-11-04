using CookBookDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace CookBookDAL.Data
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly CookBookContext cookBookContext;

        public RecipeRepository(CookBookContext cookBookContext)
        {
            this.cookBookContext = cookBookContext;
        }

        public async Task<Recipe> CreateRecipe( Recipe recipe)
        {
            var res  = await cookBookContext.Recipes.AddAsync(recipe);
            await cookBookContext.SaveChangesAsync();

            return res.Entity;
        }

        public async Task DeleteRecipe(int recipeId)
        {
            var result = await cookBookContext.Recipes.FirstOrDefaultAsync(r => r.RecipeId == recipeId);

            if(result != null)
            {
                cookBookContext.Recipes.Remove(result);
                await cookBookContext.SaveChangesAsync();
            }
        }


        public async Task<Recipe> GetRecipe(int recipeId)
        {
            var result = await cookBookContext.Recipes
                .Include(c => c.Category)
                .Include(s => s.Steps)
                .Include(i => i.Ingredients)
                .FirstOrDefaultAsync(r => r.RecipeId == recipeId);

            return result;
        }

        public async Task<IEnumerable<Recipe>> GetRecipes()
        {
            var result = await cookBookContext.Recipes
                .Include(i => i.Ingredients)
                .Include(c => c.Category.CategoryName)
                .Include(s => s.Steps)
                .ToListAsync();
            
            return result;
        }

        public async Task<Recipe> UpdateRecipe(Recipe recipe)
        {
            var oldRecipe = await cookBookContext.Recipes.FirstOrDefaultAsync(r => r.RecipeId == recipe.RecipeId);

            if(oldRecipe != null)
            {
                oldRecipe.Title = recipe.Title;
                oldRecipe.NumberOfServings = recipe.NumberOfServings;
                oldRecipe.Description = recipe.Description;

                await cookBookContext.SaveChangesAsync();

                return oldRecipe;
            }

            return null;
        }
    }
}

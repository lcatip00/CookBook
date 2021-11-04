using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBookDAL.Models;

namespace CookBookDAL.Data
{
    public interface IRecipeRepository
    {
        Task<Recipe> GetRecipe(int recipeId);
        Task<IEnumerable<Recipe>> GetRecipes();
        Task<Recipe> CreateRecipe(Recipe recipe);
        Task<Recipe> UpdateRecipe(Recipe recipe);
        Task DeleteRecipe(int recipeId);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook.Models;

namespace CookBook.Services
{
    public interface IRecipeService
    {
        Task<RecipeModel> CreateRecipeModel(RecipeModel recipeModel);
    }
}

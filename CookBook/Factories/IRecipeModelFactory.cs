using CookBook.Models;
using System.Threading.Tasks;

namespace CookBook.Factories
{
    public interface IRecipeModelFactory
    {
        Task<RecipeModel> PrepareRecipeModel(int recipeId);
    }
}
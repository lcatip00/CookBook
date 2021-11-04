using CookBookDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookDAL.Data
{
    public interface IIngredientRepository
    {
        Task<Ingredient> CreateIngredient(Ingredient ingredient);
        Task DeleteIngredient(int ingredientId);
        Task<Ingredient> GetIngredient(int ingredientId);
        Task<Ingredient> UpdateIngredient(Ingredient ingredient);
    }
}

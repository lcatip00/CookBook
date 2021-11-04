using CookBookDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookDAL.Data
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly CookBookContext cookBookContext;

        public IngredientRepository(CookBookContext cookBookContext)
        {
            this.cookBookContext = cookBookContext;
        }

        public async Task<Ingredient> CreateIngredient(Ingredient ingredient)
        {
            var newingredient = await cookBookContext.Ingredients.AddAsync(ingredient);
            await cookBookContext.SaveChangesAsync();

            return newingredient.Entity;
        }

        public async Task DeleteIngredient(int ingredientId)
        {
            var ingredient = await cookBookContext.Ingredients.FirstOrDefaultAsync(i => i.IngredientID == ingredientId);

            if(ingredient != null)
            {
                cookBookContext.Ingredients.Remove(ingredient);
                await cookBookContext.SaveChangesAsync();
            }
        }

        public async Task<Ingredient> GetIngredient(int ingredientId)
        {
            var ingredient = await cookBookContext.Ingredients.FirstOrDefaultAsync(i => i.IngredientID == ingredientId);
            return ingredient;
        }

        public async Task<Ingredient> UpdateIngredient(Ingredient ingredient)
        {
            var ingredientUpdate = await cookBookContext.Ingredients.FirstOrDefaultAsync(i => i.IngredientID == ingredient.IngredientID);

            if(ingredientUpdate != null)
            {
                ingredientUpdate.IngredientName = ingredient.IngredientName;
                ingredientUpdate.Measurment = ingredient.Measurment;
                ingredientUpdate.Amount = ingredient.Amount;

                await cookBookContext.SaveChangesAsync();
                return ingredientUpdate;
            }

            return null;
        }
    }
}

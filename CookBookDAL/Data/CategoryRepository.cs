using CookBookDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//using CookBookDAL;

namespace CookBookDAL.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CookBookContext cookBookContext;

        public CategoryRepository(CookBookContext cookBookContext)
        {
            this.cookBookContext = cookBookContext;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            var result = await cookBookContext.Categories.AddAsync(category);
            await cookBookContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteCategory(int categoryId)
        {
            var result =  await cookBookContext.Categories.FirstOrDefaultAsync(e =>
            e.CategoryId == categoryId);

            if(result != null)
            {
                cookBookContext.Categories.Remove(result);
                await cookBookContext.SaveChangesAsync();
            }
            
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var result = await cookBookContext.Categories.ToListAsync();
            return result;
        }

        public async Task<Category> GetCategory(int categoryId)
        {
            var result = await cookBookContext.Categories.FirstOrDefaultAsync(
                e => e.CategoryId == categoryId);

            return result;
        }

        public async Task<Category> GetCategoryDetails(int categoryId)
        {
            var result = await cookBookContext.Categories
                .Include(r => r.Recepies)
                .FirstOrDefaultAsync(r => r.CategoryId == categoryId);
            
            return result;
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var result = await cookBookContext.Categories.FirstOrDefaultAsync(
                c => c.CategoryId == category.CategoryId);

            if(result != null)
            {
                result.CategoryName = category.CategoryName;

                await cookBookContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}

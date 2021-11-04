using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBookDAL.Models;

namespace CookBookDAL.Data
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategory(Category category);
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int categoryId);
        Task<Category> UpdateCategory(Category category);
        Task DeleteCategory(int categoryId);
        Task<Category> GetCategoryDetails(int categoryId);
    }
}

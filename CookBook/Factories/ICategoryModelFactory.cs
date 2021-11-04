using CookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CookBook.Factories
{
    public interface ICategoryModelFactory
    {
        Task<CategoryModel> GetCategoryModel(int categoryId);
        Task<IEnumerable<CategoryModel>> GetCategoryModelList();
    }
}

using CookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBookDAL.Data;

namespace CookBook.Factories
{
    public class CategoryModelFactory : ICategoryModelFactory
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryModelFactory(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<CategoryModel> GetCategoryModel(int categoryId)
        {
            var category = await categoryRepository.GetCategory(categoryId);

            var categoryModel = new CategoryModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };

            return categoryModel;
        }

        public async Task<IEnumerable<CategoryModel>> GetCategoryModelList()
        {
            var categories =  await categoryRepository.GetCategories();

            List<CategoryModel> categoryModelList = new();

            foreach (var category in categories)
            {
                var list = new CategoryModel
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                };
                categoryModelList.Add(list);    
            }

            return categoryModelList;
        }
    }
}

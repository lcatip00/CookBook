using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBookDAL.Data;
using Microsoft.AspNetCore.Http;
using CookBookDAL.Models;
using CookBook.Factories;
using CookBook.Models;

namespace CookBook.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ICategoryModelFactory categoryModelFactory;

        public CategoryController(ICategoryRepository categoryRepository, ICategoryModelFactory categoryModelFactory)
        {
            this.categoryRepository = categoryRepository;
            this.categoryModelFactory = categoryModelFactory;
        }

        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            try
            {
                var categories = await categoryRepository.GetCategories();

                return Ok(categories);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            try
            {
                var result = await categoryRepository.GetCategory(id);
                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from the database");
            }
        }

        [HttpGet("categoryModel/{categoryId:int}")]
        public async Task<ActionResult<CategoryModel>> GetCategoryModel(int categoryId)
        {
            try
            {
                var categoryModel = await categoryModelFactory.GetCategoryModel(categoryId);

                if (categoryModel == null)
                {
                    return NotFound();
                }

                return categoryModel;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from database");
            }
        }

        [HttpGet("categoryDetails/{categoryId:int}")]
        public async Task<ActionResult> GetCategoryDetails(int categoryId)
        {
            try
            {
                var category = await categoryRepository.GetCategoryDetails(categoryId);
                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from database");
            }
        }

        [HttpGet("CategoryModelList")]
        public async Task<ActionResult<CategoryModel>> CategoryModelList()
        {
            try
            {
                var categories = await categoryModelFactory.GetCategoryModelList();
                if(categories == null)
                {
                    return NotFound();
                }
                return Ok(categories);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from databse");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            try
            {
                if (category == null)
                    return BadRequest();

                var createdCategory = await categoryRepository.CreateCategory(category);
                return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.CategoryId }, createdCategory);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new category");
            }

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Category>> UpdateCategory(int id, Category category)
        {
            try
            {
                if(id != category.CategoryId)
                {
                    return BadRequest("Category Id mismatch");
                }

                var categoryToUpdate = await categoryRepository.GetCategory(id);

                if (categoryToUpdate == null)
                {
                    return NotFound($"Category with Id = {id} not found");
                }

                return await categoryRepository.UpdateCategory(category);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating new category");
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                var result = await categoryRepository.GetCategory(id);
                if(result  == null)
                {
                    return NotFound($"Category with id = {id} not found delete");
                }

                await categoryRepository.DeleteCategory(id);

                return Ok($"Category with id={id} deleted");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting new category");
            }

        }


    }
}

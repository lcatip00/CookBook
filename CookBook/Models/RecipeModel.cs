using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Models
{
    public class RecipeModel
    {
        public RecipeModel()
        {
            Steps = new Collection<StepModel>();
            Ingredients = new Collection<IngredientModel>();
        }
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NumberOfServings { get; set; }
        public CategoryModel Category { get; set; }
        public virtual ICollection<StepModel> Steps { get; set; }
        public virtual ICollection<IngredientModel> Ingredients { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookDAL.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int NumberOfServings { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public virtual ICollection<Step> Steps { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}

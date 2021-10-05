using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBookDAL.Models
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        [Required]
        public string IngredientName { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Amount { get; set; }
        public string Measurment { get; set; }

        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }
    }
}

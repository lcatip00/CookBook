using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookDAL.Models
{
    public class Step
    {
        public int StepId { get; set; }
        public int StepNumber { get; set; }
        public string StepDescription { get; set; }

        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }
    }
}

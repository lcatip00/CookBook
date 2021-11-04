using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CookBookDAL.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required, StringLength(50), Display(Name ="Name")]
        public string CategoryName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; } = DateTime.Now;

        
        public virtual ICollection<Recipe> Recepies { get; set; }



    }
}

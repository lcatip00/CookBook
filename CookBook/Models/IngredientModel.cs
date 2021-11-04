namespace CookBook.Models
{
    public class IngredientModel
    {
        public int IngredientID { get; set; }
        public string IngredientName { get; set; }
        public decimal? Amount { get; set; }
        public string Measurment { get; set; }
    }
}
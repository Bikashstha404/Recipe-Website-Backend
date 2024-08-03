namespace RecipeManagementSystem.Models
{
    public class RecipeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }
        public string Desicription { get; set; }
        public string Category {  get; set; }

    }
}

using RecipeManagementSystemDomain.EntitesTypesModel;
using RecipeManagementSystemDomain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManagementSystemApplication.Models.RecipeModels
{
    public class RecipeEditModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan PrepTime { get; set; }
        public int Calories { get; set; }
        public MainCategory MainCategory { get; set; }
        public string SubCategory { get; set; }
        public string ImageUrl { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string Preparation { get; set; }
    }
}

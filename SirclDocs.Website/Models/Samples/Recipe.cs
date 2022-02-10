using System.Collections.Generic;

namespace SirclDocs.Website.Models.Samples
{
    public class Recipe
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<IngredientItem> Ingredients { get; set; } = new();

        public IngredientItem NewIngredient { get; set; }

        public List<string> AvailableIngredients { get; internal set; }
    }

    public class IngredientItem
    {
        public string Name { get; set; }

        public string Dosage { get; set; }
    }
}

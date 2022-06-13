namespace HealthCare_System.Core.Ingredients.Model
{
    public class IngredientDto
    {
        int id;
        string name;

        public IngredientDto(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}

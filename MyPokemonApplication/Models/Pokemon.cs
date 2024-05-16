namespace MyPokemonApplication.Models
{
    public class Pokemon
    {
        public string Name { get; set; }
        public List<Move> Moves { get; set; } 
        public List<Ability> Abilities { get; set; }
    }

}

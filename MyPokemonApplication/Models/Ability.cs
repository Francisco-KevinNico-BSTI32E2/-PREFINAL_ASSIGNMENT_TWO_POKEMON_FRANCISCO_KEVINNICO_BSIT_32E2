namespace MyPokemonApplication.Models
{
    public class Ability
    {
        public AbilityDetail ability { get; set; }
        public bool is_hidden { get; set; }
        public int slot { get; set; }

        public override string ToString()
        {
            var abilityDetails = new List<string>();

            if (ability != null && !string.IsNullOrEmpty(ability.name))
            {
                abilityDetails.Add($"Ability Name: {ability.name}");
            }

            abilityDetails.Add($"Is Hidden: {is_hidden}");
            abilityDetails.Add($"Slot: {slot}");

            return string.Join(", ", abilityDetails);
        }
    }

    public class AbilityDetail
    {
        public string name { get; set; }
        public string url { get; set; }

        public override string ToString()
        {
            return $"Name: {name}, URL: {url}";
        }
    }

}



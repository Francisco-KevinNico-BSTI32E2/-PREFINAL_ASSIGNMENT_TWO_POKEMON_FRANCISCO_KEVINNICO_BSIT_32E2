using System.Collections.Generic;

namespace MyPokemonApplication.Models
{
    public class Move
    {
        public MoveDetail move { get; set; }
        public List<VersionGroupDetail> version_group_details { get; set; }

        public override string ToString()
        {
            if (move != null && !string.IsNullOrEmpty(move.name))
            {
                return $"Move Name: {move.name}";
            }

            return string.Empty;
        }
    }
}

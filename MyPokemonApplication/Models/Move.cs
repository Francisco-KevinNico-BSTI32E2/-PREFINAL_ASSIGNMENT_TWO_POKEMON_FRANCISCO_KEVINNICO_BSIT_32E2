using System.Collections.Generic;

namespace MyPokemonApplication.Models
{
    public class Move
    {
        public MoveDetail move { get; set; }
        public List<VersionGroupDetail> version_group_details { get; set; }

        public override string ToString()
        {
            var moveDetails = new List<string>();

            if (move != null && !string.IsNullOrEmpty(move.name))
            {
                moveDetails.Add($"Move Name: {move.name}");
            }

            if (version_group_details != null && version_group_details.Any())
            {
                var versionGroupDetails = string.Join(", ", version_group_details.Select(v => v.ToString()));
                moveDetails.Add($"Version Group Details: {versionGroupDetails}");
            }

            return string.Join(", ", moveDetails);
        }
    }

    public class VersionGroupDetail
    {
        public int level_learned_at { get; set; }
        public MoveLearnMethod move_learn_method { get; set; }
        public VersionGroup version_group { get; set; }

        public override string ToString()
        {
            return $"Level Learned At: {level_learned_at}, Move Learn Method: {move_learn_method?.name}, Version Group: {version_group?.name}";
        }
    }
}


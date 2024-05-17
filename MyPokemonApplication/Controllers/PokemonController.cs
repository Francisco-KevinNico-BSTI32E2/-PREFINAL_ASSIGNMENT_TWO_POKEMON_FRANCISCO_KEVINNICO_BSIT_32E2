using Microsoft.AspNetCore.Mvc;
using MyPokemonApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyPokemonApplication.Controllers
{
    public class PokemonController : Controller
    {
        private readonly HttpClient _client;

        public PokemonController()
        {
            _client = new HttpClient();
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 20;

            var response = await _client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon?offset={(page - 1) * pageSize}&limit={pageSize}");
            var data = JsonConvert.DeserializeObject<dynamic>(response);

            int totalPokemon = data.count;
            int totalPages = (int)Math.Ceiling((double)totalPokemon / pageSize);

            var pokemon = ((IEnumerable<dynamic>)data.results).Select(x => new Pokemon { Name = x.name.ToString() }).ToList();

            return View(pokemon);
        }

        public async Task<IActionResult> Details(string name)
        {
            var response = await _client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{name}");
            var data = JsonConvert.DeserializeObject<dynamic>(response);

            var moves = data.moves != null ? ((Newtonsoft.Json.Linq.JArray)data.moves).ToObject<List<Move>>() : new List<Move>();
            var abilities = data.abilities != null ? ((Newtonsoft.Json.Linq.JArray)data.abilities).ToObject<List<Ability>>() : new List<Ability>();

            var pokemon = new Pokemon
            {
                Name = data.name,
                Moves = moves,
                Abilities = abilities
            };

            // Determine the image filename based on the Pokémon's name
            string imageName = $"{pokemon.Name.ToLower()}.png"; // Assuming images are named after Pokémon names in lowercase with .png extension
            pokemon.ImageFileName = imageName;

            return View(pokemon);
        }
    }
}

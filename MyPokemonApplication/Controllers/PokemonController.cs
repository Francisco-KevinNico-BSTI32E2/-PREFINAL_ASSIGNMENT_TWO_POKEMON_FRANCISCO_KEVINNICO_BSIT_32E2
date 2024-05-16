using Microsoft.AspNetCore.Mvc;
using MyPokemonApplication.Models;

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
            // Fetch data from the PokéAPI
            var response = await _client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon?offset={page * 20}&limit=20");
            var data = JsonConvert.DeserializeObject<dynamic>(response);

            // Convert the data to a list of Pokemon
            var pokemon = ((IEnumerable)data.results).Cast<dynamic>().Select(x => new Pokemon { Name = x.name.ToString() }).ToList();

            return View(pokemon);
        }

        public async Task<IActionResult> Details(string name)
        {
            // Fetch data from the PokéAPI
            var response = await _client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{name}");
            var data = JsonConvert.DeserializeObject<dynamic>(response);

            // Create a Pokemon object
            var pokemon = new Pokemon
            {
                Name = data.name,
                Moves = ((IEnumerable)data.moves).Cast<dynamic>().Select(x => x.move.name.ToString()).ToList(),
                Abilities = ((IEnumerable)data.abilities).Cast<dynamic>().Select(x => x.ability.name.ToString()).ToList()
            };

            return View(pokemon);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MyPokemonApplication.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

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
            var response = await _client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon?offset={(page - 1) * 20}&limit=20");
            var data = JsonConvert.DeserializeObject<dynamic>(response);

            // Convert the data to a list of Pokemon
            var pokemon = ((IEnumerable<dynamic>)data.results).Select(x => new Pokemon { Name = x.name.ToString() }).ToList();

            return View(pokemon);
        }

        public async Task<IActionResult> Details(string name)
        {
            // Fetch data from the PokéAPI
            var response = await _client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{name}");
            var data = JsonConvert.DeserializeObject<dynamic>(response);

            // Convert JArrays to List<object>
            var moves = ((Newtonsoft.Json.Linq.JArray)data.moves).ToObject<List<object>>();
            var abilities = ((Newtonsoft.Json.Linq.JArray)data.abilities).ToObject<List<object>>();

            // Create a Pokemon object
            var pokemon = new Pokemon
            {
                Name = data.name,
                Moves = moves.Select(m => m.ToString()).ToList(),
                Abilities = abilities.Select(a => a.ToString()).ToList()
            };

            return View(pokemon);
        }

    }
}

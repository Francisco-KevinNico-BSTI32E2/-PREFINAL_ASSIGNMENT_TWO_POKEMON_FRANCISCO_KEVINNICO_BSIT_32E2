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
            int pageSize = 20; // number of items per page

            // Fetch data from the PokéAPI
            var response = await _client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon?offset={(page - 1) * pageSize}&limit={pageSize}");
            var data = JsonConvert.DeserializeObject<dynamic>(response);

            // Get the total count of Pokemon
            int totalPokemon = data.count;

            int totalPages = (int)Math.Ceiling((double)totalPokemon / pageSize);

            // Convert the data to a list of Pokemon
            var pokemon = ((IEnumerable<dynamic>)data.results).Select(x => new Pokemon { Name = x.name.ToString() }).ToList();

            return View(pokemon);
        }



        public async Task<IActionResult> Details(string name)
        {
            // Fetch data from the PokéAPI
            var response = await _client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{name}");
            var data = JsonConvert.DeserializeObject<dynamic>(response);

            Console.WriteLine($"API Response: {response}"); // Log the API response

            // Convert JArrays to List<Move> and List<Ability>
            var moves = data.moves != null ? ((Newtonsoft.Json.Linq.JArray)data.moves).ToObject<List<Move>>() : new List<Move>();
            var abilities = data.abilities != null ? ((Newtonsoft.Json.Linq.JArray)data.abilities).ToObject<List<Ability>>() : new List<Ability>();

            // Create a Pokemon object
            var pokemon = new Pokemon
            {
                Name = data.name,
                Moves = moves,
                Abilities = abilities
            };

            Console.WriteLine($"Pokemon Object: {JsonConvert.SerializeObject(pokemon)}"); // Log the Pokemon object

            return View(pokemon);
        }
    }
}

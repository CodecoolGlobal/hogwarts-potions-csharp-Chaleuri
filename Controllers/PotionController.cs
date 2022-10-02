using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    [Route("/potions")]
    public class PotionController : Controller
    {
        private readonly HogwartsContext _context;

        public PotionController(HogwartsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Potion>> GetAllPotions()
        {
            return await _context.GetAllPotions();
        }

        [HttpPost]
        public async Task<IActionResult> BrewPotion([FromQuery] long studentId, [FromBody] Potion newPotion)
        {
            Potion brewedPotion = await _context.BrewPotion(studentId, newPotion);
            return Ok(brewedPotion);
        }
    }
}

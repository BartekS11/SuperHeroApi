using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SuperHeroController : Controller
    {
        private static List<SuperHero> heroes = new List<SuperHero> 
        {
            new SuperHero { 
            Id = Guid.NewGuid(),
            Name = "Spider Man",
            FirstName = "Peter",
            Lastname = "Parker",
            Place = "New York City"
            },
            
            new SuperHero { 
            Id = Guid.NewGuid(),
            Name = "IronMan",
            FirstName = "Tony",
            Lastname = "Stark",
            Place = "Long Island"
            }           
        };
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {

            return Ok(heroes);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<SuperHero>> Get(string name)
        {
            var hero = heroes.Find(f => f.Name == name);
            if(hero == null)
            {
                return BadRequest("hero not found");
            }
            return Ok(heroes);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            heroes.Add(hero);
            return Ok(heroes);
        }        
    }
}
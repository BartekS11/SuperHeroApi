using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            }   
        };
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<SuperHero>> Get(string name)
        {
            var hero = await _context.SuperHeroes.FirstAsync(f => f.Name == name);
            if(hero == null)
            {
                return BadRequest("hero not found");
            }
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }        

        [HttpPut]
        public async Task<ActionResult<SuperHero>> UpdateHero(SuperHero request)
        {
            var hero = await _context.SuperHeroes.FindAsync(request.Id);
            if(hero == null)
            {
                return BadRequest("Hero not found");
            }
            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.Lastname = request.Lastname;
            hero.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult<SuperHero>> DeleteHero(string name)
        {
            var hero = await _context.SuperHeroes.FirstAsync(f => f.Name == name);
            if(hero == null)
            {
                return BadRequest("hero not found");
            }
            _context.SuperHeroes.Remove(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }        
    }
}
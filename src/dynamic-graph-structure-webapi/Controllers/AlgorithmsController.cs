using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DynamicGraphStructure.WebApi.Database;
using DynamicGraphStructure.WebApi.Database.Models;

namespace DynamicGraphStructure.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlgorithmsController : ControllerBase
    {
        private readonly DGSContextDb _context;

        public AlgorithmsController(DGSContextDb context)
        {
            _context = context;
        }

        // GET: api/Algorithms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Algorithm>>> GetAlgorithms()
        {
            return await _context.Algorithms.ToListAsync();
        }

        // GET: api/Algorithms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Algorithm>> GetAlgorithm(int id)
        {
            var algorithm = await _context.Algorithms.FindAsync(id);

            if (algorithm == null)
            {
                return NotFound();
            }

            return algorithm;
        }

        // PUT: api/Algorithms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlgorithm(int id, Algorithm algorithm)
        {
            if (id != algorithm.Id)
            {
                return BadRequest();
            }

            _context.Entry(algorithm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlgorithmExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Algorithms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Algorithm>> PostAlgorithm(Algorithm algorithm)
        {
            _context.Algorithms.Add(algorithm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlgorithm", new { id = algorithm.Id }, algorithm);
        }

        // DELETE: api/Algorithms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlgorithm(int id)
        {
            var algorithm = await _context.Algorithms.FindAsync(id);
            if (algorithm == null)
            {
                return NotFound();
            }

            _context.Algorithms.Remove(algorithm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlgorithmExists(int id)
        {
            return _context.Algorithms.Any(e => e.Id == id);
        }
    }
}

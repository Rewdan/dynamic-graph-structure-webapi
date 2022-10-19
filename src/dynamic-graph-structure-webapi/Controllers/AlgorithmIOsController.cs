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
    public class AlgorithmIOsController : ControllerBase
    {
        private readonly DGSContextDb _context;

        public AlgorithmIOsController(DGSContextDb context)
        {
            _context = context;
        }

        // GET: api/AlgorithmIOs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlgorithmIO>>> GetAlgorithmIOs()
        {
            return await _context.AlgorithmIOs.ToListAsync();
        }

        // GET: api/AlgorithmIOs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlgorithmIO>> GetAlgorithmIO(int id)
        {
            var algorithmIO = await _context.AlgorithmIOs.FindAsync(id);

            if (algorithmIO == null)
            {
                return NotFound();
            }

            return algorithmIO;
        }

        // PUT: api/AlgorithmIOs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlgorithmIO(int id, AlgorithmIO algorithmIO)
        {
            if (id != algorithmIO.Id)
            {
                return BadRequest();
            }

            _context.Entry(algorithmIO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlgorithmIOExists(id))
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

        // POST: api/AlgorithmIOs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AlgorithmIO>> PostAlgorithmIO(AlgorithmIO algorithmIO)
        {
            _context.AlgorithmIOs.Add(algorithmIO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlgorithmIO", new { id = algorithmIO.Id }, algorithmIO);
        }

        // DELETE: api/AlgorithmIOs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlgorithmIO(int id)
        {
            var algorithmIO = await _context.AlgorithmIOs.FindAsync(id);
            if (algorithmIO == null)
            {
                return NotFound();
            }

            _context.AlgorithmIOs.Remove(algorithmIO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlgorithmIOExists(int id)
        {
            return _context.AlgorithmIOs.Any(e => e.Id == id);
        }
    }
}

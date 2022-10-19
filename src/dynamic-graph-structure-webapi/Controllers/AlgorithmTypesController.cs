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
    public class AlgorithmTypesController : ControllerBase
    {
        private readonly DGSContextDb _context;

        public AlgorithmTypesController(DGSContextDb context)
        {
            _context = context;
        }

        // GET: api/AlgorithmTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlgorithmType>>> GetAlgorithmType()
        {
            return await _context.AlgorithmType.ToListAsync();
        }

        // GET: api/AlgorithmTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlgorithmType>> GetAlgorithmType(int id)
        {
            var algorithmType = await _context.AlgorithmType.FindAsync(id);

            if (algorithmType == null)
            {
                return NotFound();
            }

            return algorithmType;
        }

        // PUT: api/AlgorithmTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlgorithmType(int id, AlgorithmType algorithmType)
        {
            if (id != algorithmType.Id)
            {
                return BadRequest();
            }

            _context.Entry(algorithmType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlgorithmTypeExists(id))
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

        // POST: api/AlgorithmTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AlgorithmType>> PostAlgorithmType(AlgorithmType algorithmType)
        {
            _context.AlgorithmType.Add(algorithmType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlgorithmType", new { id = algorithmType.Id }, algorithmType);
        }

        // DELETE: api/AlgorithmTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlgorithmType(int id)
        {
            var algorithmType = await _context.AlgorithmType.FindAsync(id);
            if (algorithmType == null)
            {
                return NotFound();
            }

            _context.AlgorithmType.Remove(algorithmType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlgorithmTypeExists(int id)
        {
            return _context.AlgorithmType.Any(e => e.Id == id);
        }
    }
}

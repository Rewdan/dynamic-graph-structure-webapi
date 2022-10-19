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
    public class AlgorithmAttributesController : ControllerBase
    {
        private readonly DGSContextDb _context;

        public AlgorithmAttributesController(DGSContextDb context)
        {
            _context = context;
        }

        // GET: api/AlgorithmAttributes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlgorithmAttribute>>> GetAlgorithmAttributes()
        {
            return await _context.AlgorithmAttributes.ToListAsync();
        }

        // GET: api/AlgorithmAttributes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlgorithmAttribute>> GetAlgorithmAttribute(int id)
        {
            var algorithmAttribute = await _context.AlgorithmAttributes.FindAsync(id);

            if (algorithmAttribute == null)
            {
                return NotFound();
            }

            return algorithmAttribute;
        }

        // PUT: api/AlgorithmAttributes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlgorithmAttribute(int id, AlgorithmAttribute algorithmAttribute)
        {
            if (id != algorithmAttribute.Id)
            {
                return BadRequest();
            }

            _context.Entry(algorithmAttribute).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlgorithmAttributeExists(id))
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

        // POST: api/AlgorithmAttributes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AlgorithmAttribute>> PostAlgorithmAttribute(AlgorithmAttribute algorithmAttribute)
        {
            _context.AlgorithmAttributes.Add(algorithmAttribute);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlgorithmAttribute", new { id = algorithmAttribute.Id }, algorithmAttribute);
        }

        // DELETE: api/AlgorithmAttributes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlgorithmAttribute(int id)
        {
            var algorithmAttribute = await _context.AlgorithmAttributes.FindAsync(id);
            if (algorithmAttribute == null)
            {
                return NotFound();
            }

            _context.AlgorithmAttributes.Remove(algorithmAttribute);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlgorithmAttributeExists(int id)
        {
            return _context.AlgorithmAttributes.Any(e => e.Id == id);
        }
    }
}

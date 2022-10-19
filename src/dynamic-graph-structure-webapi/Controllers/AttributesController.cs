using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DynamicGraphStructure.WebApi.Database;
using DynamicGraphStructure.WebApi.Database.Models;
using Attribute = DynamicGraphStructure.WebApi.Database.Models.Attribute;

namespace DynamicGraphStructure.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributesController : ControllerBase
    {
        private readonly DGSContextDb _context;

        public AttributesController(DGSContextDb context)
        {
            _context = context;
        }

        // GET: api/Attributes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attribute>>> GetAttributes()
        {
            return await _context.Attributes.ToListAsync();
        }

        // GET: api/Attributes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attribute>> GetAttribute(int id)
        {
            var attribute = await _context.Attributes.FindAsync(id);

            if (attribute == null)
            {
                return NotFound();
            }

            return attribute;
        }

        // PUT: api/Attributes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttribute(int id, Attribute attribute)
        {
            if (id != attribute.Id)
            {
                return BadRequest();
            }

            _context.Entry(attribute).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttributeExists(id))
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

        // POST: api/Attributes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Attribute>> PostAttribute(Attribute attribute)
        {
            _context.Attributes.Add(attribute);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttribute", new { id = attribute.Id }, attribute);
        }

        // DELETE: api/Attributes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttribute(int id)
        {
            var attribute = await _context.Attributes.FindAsync(id);
            if (attribute == null)
            {
                return NotFound();
            }

            _context.Attributes.Remove(attribute);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttributeExists(int id)
        {
            return _context.Attributes.Any(e => e.Id == id);
        }
    }
}

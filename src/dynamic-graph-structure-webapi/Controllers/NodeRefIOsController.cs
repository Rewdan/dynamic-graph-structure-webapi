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
    public class NodeRefIOsController : ControllerBase
    {
        private readonly DGSContextDb _context;

        public NodeRefIOsController(DGSContextDb context)
        {
            _context = context;
        }

        // GET: api/NodeRefIOs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NodeRefIO>>> GetNodeRefIOs()
        {
            return await _context.NodeRefIOs.ToListAsync();
        }

        // GET: api/NodeRefIOs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NodeRefIO>> GetNodeRefIO(int id)
        {
            var nodeRefIO = await _context.NodeRefIOs.FindAsync(id);

            if (nodeRefIO == null)
            {
                return NotFound();
            }

            return nodeRefIO;
        }

        // PUT: api/NodeRefIOs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNodeRefIO(int id, NodeRefIO nodeRefIO)
        {
            if (id != nodeRefIO.Id)
            {
                return BadRequest();
            }

            _context.Entry(nodeRefIO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NodeRefIOExists(id))
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

        // POST: api/NodeRefIOs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NodeRefIO>> PostNodeRefIO(NodeRefIO nodeRefIO)
        {
            _context.NodeRefIOs.Add(nodeRefIO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNodeRefIO", new { id = nodeRefIO.Id }, nodeRefIO);
        }

        // DELETE: api/NodeRefIOs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNodeRefIO(int id)
        {
            var nodeRefIO = await _context.NodeRefIOs.FindAsync(id);
            if (nodeRefIO == null)
            {
                return NotFound();
            }

            _context.NodeRefIOs.Remove(nodeRefIO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NodeRefIOExists(int id)
        {
            return _context.NodeRefIOs.Any(e => e.Id == id);
        }
    }
}

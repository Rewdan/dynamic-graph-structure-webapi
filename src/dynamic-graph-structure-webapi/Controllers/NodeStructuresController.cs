using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DynamicGraphStructure.WebApi.Database;
using DynamicGraphStructure.WebApi.Database.Models;
using Microsoft.Build.Framework;

namespace DynamicGraphStructure.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StructuresController : ControllerBase
    {
        private readonly DGSContextDb _context;

        public StructuresController(DGSContextDb context)
        {
            _context = context;
        }

        // GET: api/Nodes
        [HttpGet("Nodes/Billets/{graphName}")]
        public async Task<ActionResult<IEnumerable<Node>>> GetBilletNodeStructures(string graphName)
        {

            var lst = _context.Nodes
                .Where(x => x.Graph.Name == graphName)
                .Include(x => x.Graph)
                .Include(x => x.Algorithm).ThenInclude(x => x.Class)
                .Include(x => x.Algorithm).ThenInclude(x => x.AlgorithmAttributes).ThenInclude(x => x.Attribute)
                .Include(x => x.Algorithm).ThenInclude(x => x.AlgorithmIOs).ThenInclude(x => x.AlgorithmIOAttributes)
                .AsNoTracking()
                .ToArray()
                .Select(x => new NodeStructureDTO
                {
                    ClassId = x.Algorithm.Class.Name,
                    State = x.State,
                    NodeName = x.Name,
                    NodeId = x.Id,
                    NodeType = x.Algorithm.Name,
                    Attributes = x.Algorithm.AlgorithmAttributes.Select(x => x.Attribute.Name).ToArray(),
                    Inputs = x.Algorithm.AlgorithmIOs
                        .Where(x => x.TypeIO == 0)
                        .Select(x => new DataAlgorithm { 
                            I = x.Index,
                            IsNessery =  x.IsNecesse,
                            Attributes = x.AlgorithmIOAttributes.Select(x => x.Attribute.Name).ToArray()
                        }).ToArray()
                });

            return null;
        }

        // GET: api/Nodes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Node>> GetNode(int id)
        {
            var node = await _context.Nodes.FindAsync(id);

            if (node == null)
            {
                return NotFound();
            }

            return node;
        }

        // PUT: api/Nodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNode(Guid id, Node node)
        {
            if (id != node.Id)
            {
                return BadRequest();
            }

            _context.Entry(node).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NodeExists(id))
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

        // POST: api/Nodes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Node>> PostNode(Node node)
        {
            _context.Nodes.Add(node);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNode", new { id = node.Id }, node);
        }

        // DELETE: api/Nodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNode(int id)
        {
            var node = await _context.Nodes.FindAsync(id);
            if (node == null)
            {
                return NotFound();
            }

            _context.Nodes.Remove(node);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NodeExists(Guid id)
        {
            return _context.Nodes.Any(e => e.Id == id);
        }
    }
}

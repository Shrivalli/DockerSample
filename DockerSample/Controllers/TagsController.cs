using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DockerSample.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace DockerSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly orgContext _context;
        HttpClient cons = new HttpClient();
        public TagsController(orgContext context)
        {
            _context = context;
            cons.BaseAddress = new Uri("https://localhost:44340");
            cons.DefaultRequestHeaders.Accept.Clear();
            cons.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
           
        }
        [HttpGet]
        [Route("GetAllDetails")]
        public async Task<ActionResult<List<Product>>> getProdDetails()
        {
            using (cons)
            {
                HttpResponseMessage res = await cons.GetAsync("/api/wallpaper/getProd/");
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    List<Product> prd = await res.Content.ReadAsAsync<List<Product>>();
                    return prd;
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [Route("getDetails/{id}")]
        [HttpGet]
        public async Task<ActionResult<Product>> getProdDetails(int id)
        {
            using (cons)
            {
                HttpResponseMessage res = await cons.GetAsync("/api/wallpaper/getProd/"+id);
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    Product prd = await res.Content.ReadAsAsync<Product>();
                    return prd;
                }
                else
                {
                    return BadRequest();
                }
            }
        }

                    // GET: api/Tags
                    [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTag()
        {
            return await _context.Tag.ToListAsync();
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(string id)
        {
            var tag = await _context.Tag.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return tag;
        }

        
        // PUT: api/Tags/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(string id, Tag tag)
        {
            if (id != tag.TagId)
            {
                return BadRequest();
            }

            _context.Entry(tag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
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

        // POST: api/Tags
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Tag>> PostTag(Tag tag)
        {
            _context.Tag.Add(tag);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TagExists(tag.TagId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTag", new { id = tag.TagId }, tag);
        }

        // DELETE: api/Tags/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tag>> DeleteTag(string id)
        {
            var tag = await _context.Tag.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            _context.Tag.Remove(tag);
            await _context.SaveChangesAsync();

            return tag;
        }

        private bool TagExists(string id)
        {
            return _context.Tag.Any(e => e.TagId == id);
        }
    }
}

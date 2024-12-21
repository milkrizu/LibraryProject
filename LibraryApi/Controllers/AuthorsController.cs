using LibraryCore;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : Controller
    {
        private readonly Upravlenie_bibliotekoyEntities _context;

        public AuthorsController(Upravlenie_bibliotekoyEntities context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public IActionResult GetAuthors()
        {
            var authors = _context.Authors.ToList();
            return Ok(authors);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public IActionResult GetAuthor(int id)
        {
            var author = _context.Authors.FirstOrDefault(a => a.AuthorID == id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        // POST: api/Authors
        [HttpPost]
        public IActionResult PostAuthor([FromBody] Authors author)
        {
            if (author == null)
            {
                return BadRequest();
            }

            _context.Authors.Add(author);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.AuthorID }, author);
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        public IActionResult PutAuthor(int id, [FromBody] Authors author)
        {
            if (author == null || author.AuthorID != id)
            {
                return BadRequest();
            }

            var existingAuthor = _context.Authors.FirstOrDefault(a => a.AuthorID == id);
            if (existingAuthor == null)
            {
                return NotFound();
            }

            existingAuthor.FirstName = author.FirstName;
            existingAuthor.LastName = author.LastName;
            existingAuthor.BirthDate = author.BirthDate;
            existingAuthor.Country = author.Country;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = _context.Authors.FirstOrDefault(a => a.AuthorID == id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

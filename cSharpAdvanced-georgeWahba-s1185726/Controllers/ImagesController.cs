using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using cSharpAdvanced_georgeWahba_s1185726.Models;
using cSharpAdvanced_georgeWahba_s1185726.Repositories;

namespace cSharpAdvanced_georgeWahba_s1185726.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        // GET: api/Images
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetImage(CancellationToken cancellationToken)
        {
            var images = await _imageRepository.GetAllImages(cancellationToken);
            return Ok(images);
        }

        // GET: api/Images/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> GetImage(int id, CancellationToken cancellationToken)
        {
            var image = await _imageRepository.GetImageById(id, cancellationToken);
            if (image == null)
            {
                return NotFound();
            }
            return Ok(image);
        }

        // PUT: api/Images/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImage(int id, Image image, CancellationToken cancellationToken)
        {
            if (id != image.Id)
            {
                return BadRequest();
            }

            var updated = await _imageRepository.UpdateImage(image, cancellationToken);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Images
        [HttpPost]
        public async Task<ActionResult<Image>> PostImage(Image image, CancellationToken cancellationToken)
        {
            var createdImage = await _imageRepository.AddImage(image, cancellationToken);
            return CreatedAtAction("GetImage", new { id = createdImage.Id }, createdImage);
        }

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id, CancellationToken cancellationToken)
        {
            var deleted = await _imageRepository.DeleteImage(id, cancellationToken);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

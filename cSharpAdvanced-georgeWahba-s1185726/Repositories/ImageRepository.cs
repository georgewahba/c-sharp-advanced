using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cSharpAdvanced_georgeWahba_s1185726.Data;
using cSharpAdvanced_georgeWahba_s1185726.Models;
using Microsoft.EntityFrameworkCore;

namespace cSharpAdvanced_georgeWahba_s1185726.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly cSharpAdvanced_georgeWahba_s1185726Context _context;

        public ImageRepository(cSharpAdvanced_georgeWahba_s1185726Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Image>> GetAllImages()
        {
            return await _context.Image.ToListAsync();
        }

        public async Task<Image> GetImageById(int id)
        {
            return await _context.Image.FindAsync(id);
        }

        public async Task<Image> AddImage(Image image)
        {
            _context.Image.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }

        public async Task<bool> UpdateImage(Image image)
        {
            _context.Entry(image).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(image.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        public async Task<bool> DeleteImage(int id)
        {
            var image = await _context.Image.FindAsync(id);
            if (image == null)
            {
                return false;
            }

            _context.Image.Remove(image);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool ImageExists(int id)
        {
            return _context.Image.Any(e => e.Id == id);
        }
    }
}

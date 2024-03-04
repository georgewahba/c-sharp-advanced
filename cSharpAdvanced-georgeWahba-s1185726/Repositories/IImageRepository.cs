using System.Collections.Generic;
using System.Threading.Tasks;
using cSharpAdvanced_georgeWahba_s1185726.Models;

namespace cSharpAdvanced_georgeWahba_s1185726.Repositories
{
    public interface IImageRepository
    {
        Task<IEnumerable<Image>> GetAllImages();
        Task<Image> GetImageById(int id);
        Task<Image> AddImage(Image image);
        Task<bool> UpdateImage(Image image);
        Task<bool> DeleteImage(int id);
    }
}

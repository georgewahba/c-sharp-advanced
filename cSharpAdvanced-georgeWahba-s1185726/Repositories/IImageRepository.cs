using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using cSharpAdvanced_georgeWahba_s1185726.Models;

namespace cSharpAdvanced_georgeWahba_s1185726.Repositories
{
    public interface IImageRepository
    {
        Task<IEnumerable<Image>> GetAllImages(CancellationToken cancellationToken);
        Task<Image> GetImageById(int id, CancellationToken cancellationToken);
        Task<Image> AddImage(Image image, CancellationToken cancellationToken);
        Task<bool> UpdateImage(Image image, CancellationToken cancellationToken);
        Task<bool> DeleteImage(int id, CancellationToken cancellationToken);
    }
}

using DuplicateImagesFinder.Models;
using System.Drawing;
using System.Linq;

namespace DuplicateImagesFinder.Services
{
    public interface IDuplicateImageFinder
    {
        public IGrouping<string, Base64Image> Find(string path);

        public string GetBase64(Image image);
    }
}
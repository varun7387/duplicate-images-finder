using DuplicateImagesFinder.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DuplicateImagesFinder.Services
{
    public interface IDuplicateImageFinder
    {
        public void Process(string path);

        public IList<IGrouping<string, Base64Image>> GetDuplicateImageGroups(IList<Base64Image> images);

        public IList<Base64Image> GetBase64Images(string[] files);


        public bool IsImage(string file);

        public string GetBase64(Image image);
    }
}
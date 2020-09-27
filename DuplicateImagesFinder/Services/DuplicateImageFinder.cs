using DuplicateImagesFinder.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DuplicateImagesFinder.Services
{
    public class DuplicateImageFinder : IDuplicateImageFinder
    {
        public DuplicateImageFinder()
        {
        }

        public IGrouping<string, Base64Image> Find(string path)
        {
            throw new NotImplementedException();
        }

        public string GetBase64(Image image)
        {
            throw new NotImplementedException();
        }
    }
}

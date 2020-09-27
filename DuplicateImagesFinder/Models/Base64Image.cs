using System;
using System.Collections.Generic;
using System.Text;

namespace DuplicateImagesFinder.Models
{
    public class Base64Image
    {
        public string Path { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public string Base64 { get; private set; }

        public Base64Image(
            string path,
            int width,
            int height,
            string base64
        )
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (string.IsNullOrEmpty(base64))
            {
                throw new ArgumentNullException(nameof(base64));
            }

            if (width <= 0 || height <= 0)
            {
                throw new ArgumentNullException($"Width and height must be greater than 0.");
            }

            Path = path;
            Width = width;
            Height = height;
            Base64 = base64;
        }
    }
}

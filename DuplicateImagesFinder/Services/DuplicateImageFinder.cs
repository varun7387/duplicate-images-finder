using DuplicateImagesFinder.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;


namespace DuplicateImagesFinder.Services
{
    public class DuplicateImageFinder : IDuplicateImageFinder
    {
        private readonly string[] _imageFileExtensions = new string[] { 
            ".jpg", ".jpeg", ".png", ".tiff"
        };

        public void Process(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            var files = GetFiles(path);
            Console.WriteLine($"Found {files.Length} files.");

            var base64Images = GetBase64Images(files);
            Console.WriteLine($"Found {base64Images.Count()} images.");

            LogDuplicates(base64Images);
        }

        private string[] GetFiles(string path)
        {
            try
            {
                return Directory
                     .GetFiles(path, "*.*", SearchOption.AllDirectories);
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Something went wrong trying to fetch files. Please verify if the path is valid.");
                Console.WriteLine($"\nDetails: {ex.Message}");
                return new string[0];
            }
        }

        public bool IsImage(string file)
        {
            return _imageFileExtensions.Any(ext => file.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }

        public string GetBase64(Image image)
        {
            if (image == null)
            {
                return null;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                var imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        public IList<Base64Image> GetBase64Images(string[] files)
        {
            if (files == null || files.Length == 0)
            {
                return new List<Base64Image>();
            }

            var images = new List<Base64Image>();
            foreach (var file in files)
            {
                if (IsImage(file))
                {
                    using (var image = Image.FromFile(file))
                    {
                        var base64 = GetBase64(image);
                        if (!string.IsNullOrWhiteSpace(base64))
                        {
                            var base64Image = new Base64Image(file, image.Width, image.Height, base64);
                            images.Add(base64Image);
                        }
                    };
                }
            }

            return images.ToList();
        }

        public IList<IGrouping<string, Base64Image>> GetDuplicateImageGroups(IList<Base64Image> images)
        {
            var groupedImages = images
                            ?.GroupBy(i => i.Base64)
                            ?.ToList() ?? new List<IGrouping<string, Base64Image>>();

            Console.WriteLine($"Found {groupedImages.Count()} groups of images.");

            var duplicateImageGroups = groupedImages.Where(grp => grp.Count() > 1);
            Console.WriteLine($"Found {duplicateImageGroups.Count()} images with 2 or more copies");
            
            return duplicateImageGroups?.ToList() ?? new List<IGrouping<string, Base64Image>>();
        }

        private void LogDuplicates(IList<Base64Image> images)
        {
            var duplicateImageGroups = GetDuplicateImageGroups(images);

            foreach (var duplicateImageGroup in duplicateImageGroups)
            {
                Console.WriteLine("----------------------------------------------------------");
                foreach (var image in duplicateImageGroup)
                {
                    Console.WriteLine(image.Path);
                }
                Console.WriteLine("----------------------------------------------------------\n");
            }
        }
    }
}

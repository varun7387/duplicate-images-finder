using DuplicateImagesFinder.Models;
using DuplicateImagesFinder.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Xunit;

namespace DuplicateImagesFinder.Tests
{
    public class DuplicateImageFinderTests
    {
        private readonly IDuplicateImageFinder _duplicateImageFinder;

        public DuplicateImageFinderTests()
        {
            _duplicateImageFinder = new DuplicateImageFinder();
        }


        [Fact]
        public void Process_ShouldThrowArgumentNullException_OnNullPath()
        {
            Assert.Throws<ArgumentNullException>(() => _duplicateImageFinder.Process(null));
        }

        [Fact]
        public void Process_ShouldThrowArgumentNullException_OnEmptyPath()
        {
            Assert.Throws<ArgumentNullException>(() => _duplicateImageFinder.Process(""));
        }

        [Theory]
        [InlineData("C:\\Temp\\test.jpg", true)]
        [InlineData("C:\\Temp\\test.jpeg", true)]
        [InlineData("C:\\Temp\\test.png", true)]
        [InlineData("C:\\Temp\\test.tiff", true)]
        [InlineData("C:\\Temp\\test.JPG", true)]
        [InlineData("C:\\Temp\\test.JPeg", true)]
        [InlineData("C:\\Temp\\test.pNg", true)]
        [InlineData("C:\\Temp\\test.TIFF", true)]
        [InlineData("C:\\Temp\\test.j",false)]
        [InlineData("C:\\Temp\\test.pdf", false)]
        public void IsImage_Tests(string path, bool expectedResult)
        {
            var actualResult = _duplicateImageFinder.IsImage(path);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetBase64_ShouldReturnNull()
        {
            var base64 = _duplicateImageFinder.GetBase64(null);
            Assert.Null(base64);
        }

        [Fact]
        public void GetBase64_ShouldNotReturnNull()
        {
            var base64 = _duplicateImageFinder.GetBase64(Image.FromFile("test.jpg"));
            Assert.NotNull(base64);
        }

        [Fact]
        public void GetBase64Images_ShouldReturnEmptyListOfImages_OnNullFiles()
        {
            var images = _duplicateImageFinder.GetBase64Images(null);
            Assert.NotNull(images);
        }

        [Fact]
        public void GetBase64Images_ShouldReturnEmptyListOfImages_OnEmptyFiles()
        {
            var images = _duplicateImageFinder.GetBase64Images(new string[0]);
            Assert.NotNull(images);
            Assert.Empty(images);
        }

        [Fact]
        public void GetDuplicateImageGroups_MustReturnEmptySet_OnNullImageList()
        {
            var groups = _duplicateImageFinder.GetDuplicateImageGroups(null);
            Assert.NotNull(groups);
            Assert.Empty(groups);
        }

        [Fact]
        public void GetDuplicateImageGroups_MustReturnEmptySet_OnEmptyImageList()
        {
            var groups = _duplicateImageFinder.GetDuplicateImageGroups(new List<Base64Image>());
            Assert.NotNull(groups);
            Assert.Empty(groups);
        }

        [Fact]
        public void GetDuplicateImageGroups_MustReturnEmptySet_OnNoDuplicates()
        {
            var images = new List<Base64Image>
            {
                new Base64Image("C://temp//test1.jpeg", 256, 256, "DummyImageData1"),
                new Base64Image("C://temp//test3.jpeg", 256, 256, "DummyImageData2"),
                new Base64Image("C://temp//test4.jpeg", 256, 256, "DummyImageData3"),
            };
            var groups = _duplicateImageFinder.GetDuplicateImageGroups(images);
            Assert.NotNull(groups);
            Assert.Empty(groups);
        }


        [Fact]
        public void GetDuplicateImageGroups_MustReturnValidSet_OnDuplicates()
        {
            var images = new List<Base64Image>
            {
                new Base64Image("C://temp//test1.jpeg", 256, 256, "DummyImageData1"),
                new Base64Image("C://temp//test11.jpeg", 256, 256, "DummyImageData1"),
                new Base64Image("C://temp//test3.jpeg", 256, 256, "DummyImageData2"),
                new Base64Image("C://temp//test4.jpeg", 256, 256, "DummyImageData3"),
                new Base64Image("C://temp//test44.jpeg", 256, 256, "DummyImageData3"),
            };
            var groups = _duplicateImageFinder.GetDuplicateImageGroups(images);
            Assert.NotNull(groups);
            Assert.NotEmpty(groups);
            Assert.Equal(2, groups.Count());
        }
    }
}

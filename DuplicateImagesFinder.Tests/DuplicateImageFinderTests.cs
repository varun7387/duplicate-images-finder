using DuplicateImagesFinder.Services;
using System;
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
        public void Find_ShouldThrowArgumentNullException_OnNullPath()
        {
            Assert.Throws<ArgumentNullException>(() => _duplicateImageFinder.Find(null));
        }

        [Fact]
        public void Find_ShouldThrowArgumentNullException_OnEmptyPath()
        {
            Assert.Throws<ArgumentNullException>(() => _duplicateImageFinder.Find(""));
        }

        
    }
}

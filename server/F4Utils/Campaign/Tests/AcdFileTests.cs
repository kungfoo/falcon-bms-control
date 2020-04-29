using Common.IO.File;
using NUnit.Framework;
using System.IO;

namespace F4Utils.Campaign.Tests
{
    [TestFixture]
    public class AcdFileTests
    {
        private string _loadedAcdFilePath;
        private AcdFile _acdFile;
        private string _savedAcdFilePath;
        [Test]
        public void ShouldLoadACDFileWithoutThrowingExceptions()
        {
            GivenACDFilePath(TestDatalLocations.AcdFile);
            WhenFileIsLoaded();
            ThenNoExceptionsShouldBeThrown();
        }
        [Test]
        public void ShouldSaveACDFileWithoutThrowingExceptions()
        {
            GivenACDFilePath(TestDatalLocations.AcdFile);
            WhenFileIsLoaded();
            WhenFileIsSaved(TestDatalLocations.TestDataOutputFolder + "FALCON4.ACD");
            ThenNoExceptionsShouldBeThrown();
        }
        [Test]
        public void ShouldSaveIdenticalFileAsFileThatWasLoaded()
        {
            GivenACDFilePath(TestDatalLocations.AcdFile);
            WhenFileIsLoaded();
            WhenFileIsSaved(TestDatalLocations.TestDataOutputFolder + "FALCON4.ACD");
            ThenSavedFileShouldMatchOriginal();
        }
        private void GivenACDFilePath(string acdFilePath)
        {
            _loadedAcdFilePath = acdFilePath;
        }

        private void WhenFileIsSaved(string fileName) 
        {
            _savedAcdFilePath = fileName;
            _acdFile.Save(_savedAcdFilePath);
        }
       
        private void WhenFileIsLoaded()
        {
            _acdFile = new AcdFile(_loadedAcdFilePath);
        }
        private static void ThenNoExceptionsShouldBeThrown() { }
        private void ThenSavedFileShouldMatchOriginal()
        {
            var filesAreEqual = new FileComparer().Compare(_loadedAcdFilePath, _savedAcdFilePath);
            Assert.That(filesAreEqual);
        }
        [SetUp]
        public void SetUp()
        {
            if (!Directory.Exists(TestDatalLocations.TestDataOutputFolder))
            {
                Directory.CreateDirectory(TestDatalLocations.TestDataOutputFolder);
            }
        }
        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(TestDatalLocations.TestDataOutputFolder))
            {
                Directory.Delete(TestDatalLocations.TestDataOutputFolder,true);
            }
        }

    }
}

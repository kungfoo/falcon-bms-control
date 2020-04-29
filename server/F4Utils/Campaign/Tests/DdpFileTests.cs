using Common.IO.File;
using NUnit.Framework;
using System.IO;

namespace F4Utils.Campaign.Tests
{
    [TestFixture]
    public class DdpFileTests
    {
        private string _loadedDdpFilePath;
        private DdpFile _ddpFile;
        private string _savedDdpFilePath;
        [Test]
        public void ShouldLoadDDPFileWithoutThrowingExceptions()
        {
            GivenDDPFilePath(TestDatalLocations.DdpFile);
            WhenFileIsLoaded();
            ThenNoExceptionsShouldBeThrown();
        }
        [Test]
        public void ShouldSaveDDPFileWithoutThrowingExceptions()
        {
            GivenDDPFilePath(TestDatalLocations.DdpFile);
            WhenFileIsLoaded();
            WhenFileIsSaved(TestDatalLocations.TestDataOutputFolder + "FALCON4.DDP");
            ThenNoExceptionsShouldBeThrown();
        }
        [Test]
        public void ShouldSaveIdenticalFileAsFileThatWasLoaded()
        {
            GivenDDPFilePath(TestDatalLocations.DdpFile);
            WhenFileIsLoaded();
            WhenFileIsSaved(TestDatalLocations.TestDataOutputFolder + "FALCON4.DDP");
            ThenSavedFileShouldMatchOriginal();
        }
        private void GivenDDPFilePath(string ddpFilePath)
        {
            _loadedDdpFilePath = ddpFilePath;
        }

        private void WhenFileIsSaved(string fileName)
        {
            _savedDdpFilePath = fileName;
            _ddpFile.Save(_savedDdpFilePath);
        }

        private void WhenFileIsLoaded()
        {
            _ddpFile = new DdpFile(_loadedDdpFilePath);
        }
        private static void ThenNoExceptionsShouldBeThrown() { }
        private void ThenSavedFileShouldMatchOriginal()
        {
            var filesAreEqual = new FileComparer().Compare(_loadedDdpFilePath, _savedDdpFilePath);
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
                Directory.Delete(TestDatalLocations.TestDataOutputFolder, true);
            }
        }

    }
}

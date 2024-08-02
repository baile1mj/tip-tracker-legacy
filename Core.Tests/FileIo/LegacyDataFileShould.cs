using Core.FileIo;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

namespace Core.Tests.FileIo
{
    public class LegacyDataFileShould
    {
        [Fact]
        public void CorrectlyLoadFileData()
        {
            const string TEST_PATH = @"C:\Test\Path\File.ttd";

            var fileSystem = Substitute.For<IFileSystem>();
            fileSystem.File
                .ReadAllText(TEST_PATH)
                .Returns(TestResources.LegacyFile);
            var expectedTipTypes = new[] { "Credit Card", "Room Charge", "Special Function", "Cash" };
            var expectedFunctions = new[] { "Wedding", "Party" };
            var testClass = new LegacyFileReader(fileSystem, TEST_PATH);

            var result = testClass.LoadData();

            var actualTipTypes = result.TipTypes.Select(t => t.Name);
            var actualFunctions = result.Functions.Select(f => f.Name);


            Assert.True(actualTipTypes.SequenceEqual(expectedTipTypes));
            Assert.True(actualFunctions.SequenceEqual(expectedFunctions));

        }
    }
}

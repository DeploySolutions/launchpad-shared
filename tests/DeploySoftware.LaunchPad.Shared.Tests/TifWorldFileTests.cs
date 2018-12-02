using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DeploySoftware.LaunchPad.Shared.Domain;
using DeploySoftware.LaunchPad.Shared.Util;
using FluentAssertions;
using Xunit;

namespace DeploySoftware.LaunchPad.Shared.Tests
{
    public class TifWorldFileTests
    {
        [Fact]
        public void FileExtension_Should_Be_DotTFW()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string fileName = Path.GetDirectoryName(path) + "\\" + "TifWorldFileTest.tfw";
            FileKey key = new FileKey(fileName);
            TifWorldFileParser<Guid> parser = new TifWorldFileParser<Guid>();
            TifWorldFile<Guid> file = parser.GetTifWorldFileFromMetadataFile(key);
            file.FileExtension.Should().Be(".tfw");
        }
       
        [Fact]
        public void A_Should_Equal_First_Line_In_File()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string fileName = Path.GetDirectoryName(path) + "\\" + "TifWorldFileTest.tfw";
            FileKey key = new FileKey(fileName);
            TifWorldFileParser<Guid> parser = new TifWorldFileParser<Guid>();
            TifWorldFile<Guid> file = parser.GetTifWorldFileFromMetadataFile(key);
            file.A.Should().Be(12.413247108000000m);
        }
        
        [Fact]
        public void D_Should_Equal_Second_Line_In_File()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string fileName = Path.GetDirectoryName(path) + "\\" + "TifWorldFileTest.tfw";
            FileKey key = new FileKey(fileName);
            TifWorldFileParser<Guid> parser = new TifWorldFileParser<Guid>();
            TifWorldFile<Guid> file = parser.GetTifWorldFileFromMetadataFile(key);
            file.D.Should().Be(0.000000000000000m);
        }

        [Fact]
        public void B_Should_Equal_Third_Line_In_File()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string fileName = Path.GetDirectoryName(path) + "\\" + "TifWorldFileTest.tfw";
            FileKey key = new FileKey(fileName);
            TifWorldFileParser<Guid> parser = new TifWorldFileParser<Guid>();
            TifWorldFile<Guid> file = parser.GetTifWorldFileFromMetadataFile(key);
            file.B.Should().Be(0.000000000000000m);
        }

        [Fact]
        public void E_Should_Equal_Fourth_Line_In_File()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string fileName = Path.GetDirectoryName(path) + "\\" + "TifWorldFileTest.tfw";
            FileKey key = new FileKey(fileName);
            TifWorldFileParser<Guid> parser = new TifWorldFileParser<Guid>();
            TifWorldFile<Guid> file = parser.GetTifWorldFileFromMetadataFile(key);
            file.E.Should().Be(-12.382885933000001m);
        }
        
        [Fact]
        public void C_Should_Equal_Fifth_Line_In_File()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string fileName = Path.GetDirectoryName(path) + "\\" + "TifWorldFileTest.tfw";
            FileKey key = new FileKey(fileName);
            TifWorldFileParser<Guid> parser = new TifWorldFileParser<Guid>();
            TifWorldFile<Guid> file = parser.GetTifWorldFileFromMetadataFile(key);
            file.C.Should().Be(511283.0285078580m);
        }
        
        [Fact]
        public void F_Should_Equal_Sixth_Line_In_File()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string fileName = Path.GetDirectoryName(path) + "\\" + "TifWorldFileTest.tfw";
            FileKey key = new FileKey(fileName);
            TifWorldFileParser<Guid> parser = new TifWorldFileParser<Guid>();
            TifWorldFile<Guid> file = parser.GetTifWorldFileFromMetadataFile(key);
            file.F.Should().Be(7755841.0522885220m);
        }
        
    }
}

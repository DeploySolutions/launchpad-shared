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
    public class TifWorldFileTests : IClassFixture<TfwWorldFileTestsFixture>
    {
        
        private readonly TfwWorldFileTestsFixture _fixture;

        public TifWorldFileTests(TfwWorldFileTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void FileExtension_Should_Be_DotTFW()
        {
            
            _fixture.TfwFile.FileExtension.Should().Be(".tfw");
        }
       
        [Fact]
        public void A_Should_Equal_First_Line_In_File()
        {
            _fixture.TfwFile.A.Should().Be(12.413247108000000m);
        }
        
        [Fact]
        public void D_Should_Equal_Second_Line_In_File()
        {
            _fixture.TfwFile.D.Should().Be(0.000000000000000m);
        }

        [Fact]
        public void B_Should_Equal_Third_Line_In_File()
        {
            _fixture.TfwFile.B.Should().Be(0.000000000000000m);
        }

        [Fact]
        public void E_Should_Equal_Fourth_Line_In_File()
        {
            _fixture.TfwFile.E.Should().Be(-12.382885933000001m);
        }
        
        [Fact]
        public void C_Should_Equal_Fifth_Line_In_File()
        {
            _fixture.TfwFile.C.Should().Be(511283.0285078580m);
        }
        
        [Fact]
        public void F_Should_Equal_Sixth_Line_In_File()
        {
            _fixture.TfwFile.F.Should().Be(7755841.0522885220m);
        }
        
    }
}

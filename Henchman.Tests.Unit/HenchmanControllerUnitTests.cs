using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using System.Net.Http;

namespace Henchman
{
    public class HenchmanControllerUnitTests
    {
        private HenchmanController _ctrl;

        public HenchmanControllerUnitTests()
        {
            var blcMock = new Mock<IHenchmanBLC>();
            blcMock.Setup(b => b.GetHenchmen())
                .Returns(new List<Henchman>
                {
                    new Henchman(),
                    new Henchman()
                });

            blcMock.Setup(b => b.GetHenchman(It.IsAny<string>()))
                .Returns((string name) => new Henchman { Name = name });

            blcMock.Setup(b => b.PostHenchman(It.IsAny<Henchman>()))
                .Returns((Henchman henchman) => henchman);

            blcMock.Setup(b => b.PostHenchmen(It.IsAny<List<Henchman>>()))
                .Returns((List<Henchman> henchmen) => henchmen);

            _ctrl = new HenchmanController(blcMock.Object);
        }

        [Fact]
        public void Get_ReturnsHenchmen()
        {
            var result = _ctrl.Get();

            Assert.True(result.Count > 1);
        }

        [Fact]
        public void Get_ValidName_ReturnsHenchman()
        {
            var name = "test";

            var result = _ctrl.Get(name);

            Assert.Equal(name, result.Name);
        }

        [Fact]
        public void Get_Null_ThrowsException()
        {
            Assert.Throws<HttpRequestException>(() => _ctrl.Get(null));
        }

        [Fact]
        public void Post_SingleHenchman_ReturnsHenchman()
        {
            var henchman = new Henchman { Name = "test" };

            var result = _ctrl.Post(henchman);

            Assert.Equal(henchman.Name, result.Name);
        }

        [Fact]
        public void Post_MultipleHenchmen_ReturnsHenchmen()
        {
            var henchmen = new List<Henchman>
            {
                new Henchman { Name = "test1" },
                new Henchman { Name = "test2" }
            };

            var result = _ctrl.Post(henchmen);

            Assert.True(result.All(m => henchmen.Any(mm => mm.Name == m.Name)));
        }
    }
}
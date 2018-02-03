using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;

namespace Henchman
{
    public class HenchmanBLCUnitTests
    {
        private const string NEW = "new";
        private const string EXISTING = "existing";
        private HenchmanBLC _blc;

        public HenchmanBLCUnitTests()
        {
            var daoMock = new Mock<IHenchmanDAO>();
            daoMock.Setup(d => d.GetHenchmen())
                .Returns(new List<Henchman>
                {
                    new Henchman(),
                    new Henchman()
                });

            daoMock.Setup(d => d.GetHenchman(It.IsAny<string>()))
                .Returns((string name) => 
                {
                    if(name == NEW)
                        return null;
                    else
                        return new Henchman 
                            {  
                                Name = name,
                                Edition = "oldEdition"
                            };
                });

            daoMock.Setup(d => d.InsertHenchman(It.IsAny<Henchman>()))
                .Returns((Henchman henchman) => henchman);

            daoMock.Setup(d => d.UpdateHenchman(It.IsAny<Henchman>()))
                .Returns((Henchman henchman) => henchman);

            _blc = new HenchmanBLC(daoMock.Object);
        }

        [Fact]
        public void GetHenchmen_ReturnsHenchmen()
        {
            var result = _blc.GetHenchmen();

            Assert.True(result.Count > 1);
        }

        [Fact]
        public void GetHenchman_ValidName_ReturnsHenchman()
        {
            var result = _blc.GetHenchman(EXISTING);

            Assert.Equal(EXISTING, result.Name);
        }

        [Fact]
        public void PostHenchman_ExistingHenchman_UpdatesHenchman()
        {
            var henchman = new Henchman
            {
                Name = EXISTING,
                Edition = "newEdition"
            };

            var result = _blc.PostHenchman(henchman);

            Assert.Equal(henchman.Edition, result.Edition);
        }

        [Fact]
        public void PostHenchman_NewHenchman_InsertsHenchman()
        {
            var henchman = new Henchman
            {
                Name = NEW
            };

            var result = _blc.PostHenchman(henchman);

            Assert.Equal(henchman.Name, result.Name);
        }

        [Fact]
        public void PostHenchmen_NewAndExistingHenchmen_InsertsAndUpdatesHenchmen()
        {
            var henchmen = new List<Henchman>
            {
                new Henchman { Name = NEW, Edition = "Core" },
                new Henchman { Name = EXISTING, Edition = "Core" }
            };

            var result = _blc.PostHenchmen(henchmen);

            Assert.True(result.All(m => henchmen.Any(mm => mm.Name == m.Name)));
        }
    }
}
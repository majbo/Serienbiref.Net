using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serienbrief.Net.serienbrief.domain;
using System.Collections.Generic;
using Serienbrief.Net.serienbrief.api;
using Serienbrief.Net.api;
using System.Linq;

namespace Serienbrief.Net.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        private SerienBriefServiceImpl testee;

        [TestInitialize]
        public void setup()
        {
            testee = new SerienBriefServiceImpl();
        }

        [TestMethod]
        public void testCreateElternAbend()
        {
            //Arrange
            string klassenId = "Sek A";

            //Act
            List<SerienBrief> elternAbende = testee.createElternAbend(klassenId);

            //Assert
            Assert.AreEqual(3, elternAbende.Count);
            elternAbende.ForEach(b =>
            {
                Assert.AreEqual(b.getValue("Klasse.Id"), klassenId);
                Assert.AreEqual(b.getValue("Lehrer.Name"), "Anette Halbestunde");
                Assert.AreEqual(b.Type, SerienBriefType.ElternAbend);
            });

            Assert.AreEqual("Furt", elternAbende[0].getValue("Eltern.Nachname"));
            Assert.AreEqual("Rasur", elternAbende[1].getValue("Eltern.Nachname"));
            Assert.AreEqual("Imal", elternAbende[2].getValue("Eltern.Nachname"));
        }


        [TestMethod]
        public void createElternAbendNoten()
        {
            //Arrange
            string klassenId = "Sek A";
            var noten = new Dictionary<Schueler, List<float>>();
            List<Schueler> schueler = testee.repo.getKlasse(klassenId).Schueler;
            noten.Add(schueler[0], new List<float> { 1f, 2.5f, 3.5f });
            noten.Add(schueler[1], new List<float> { 4.5f, 5f, 5f });
            noten.Add(schueler[2], new List<float> { 5.5f, 5.75f, 6f });

            //Act
            List<SerienBrief> notenBriefe = testee.createElternAbendNoten(noten);

            //Assert
            Assert.AreEqual(2, notenBriefe.Count);
            notenBriefe.ForEach(b =>
            {
                Assert.AreEqual(klassenId, b.getValue("Klasse.Id"));
                Assert.AreEqual("Anette Halbestunde", b.getValue("Lehrer.Name"));
            });

            Assert.AreEqual(notenBriefe[0].getValue("Eltern.Nachname"), "Furt");
            Assert.AreEqual(notenBriefe[0].Type, SerienBriefType.ElternGespraechSchlecht);
            Assert.AreEqual(notenBriefe[1].getValue("Eltern.Nachname"), "Imal");
            Assert.AreEqual(notenBriefe[1].Type, SerienBriefType.ElternGespraechGut);
        }

        [TestMethod]
        public void createInformationsAbend()
        {
            List<SerienBrief> briefe = testee.createInformationsAbend();
            Assert.AreEqual(5, briefe.Count);

            var names = briefe
                .Select(b => b.getValue("Eltern.Nachname"))
                .ToList();

            Assert.IsTrue(names.Contains("Furt"));
            Assert.IsTrue(names.Contains("Rasur"));
            Assert.IsTrue(names.Contains("Imal"));
            Assert.IsTrue(names.Contains("Fuk"));
            Assert.IsTrue(names.Contains("Gang"));
        }
    }
}

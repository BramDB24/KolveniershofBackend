using kolveniershofBackend.Enums;
using kolveniershofBackend.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace KolveniershofBackendTests.UnitTests.DomainTests
{
    public class GebruikerTest
    {
        [Theory]
        [InlineData("d@g.test")]
        [InlineData("string")]
        [InlineData("jonah@desmet")]
        [InlineData("jonah.desmet*hotmail.com")]
        [InlineData("")]
        public void TestOpInvalideGebruikerEmail(string email)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Gebruiker gebruiker =
                    new Gebruiker("voornaam", "achternaam", email, Sfeergroep.Sfeergroep1, "foto", "straat", "1", "4", "gemeente", "9506", GebruikerType.Cliënt);
            });
        }


        [Theory]
        [InlineData("geldig@hotmail.com")]
        [InlineData("geldig@telenet.be")]
        [InlineData("jonah@desmet.be.com")]
        [InlineData("geldig_underscore@telenet.be")]
        public void TestOpValidGebruikerEmail(string email)
        {
            Gebruiker gebruiker =
                new Gebruiker("voornaam", "achternaam", email, Sfeergroep.Sfeergroep1, "foto", "straat", "1", "4", "gemeente", "9506", GebruikerType.Cliënt);
            Assert.Equal(email, gebruiker.Email);
        }


        [Theory]
        [InlineData("0000")]
        [InlineData("9400")]
        public void TestOpValidPostcode(string postcode)
        {
            Gebruiker gebruiker =
               new Gebruiker("voornaam", "achternaam", "geldig@hotmail.com", Sfeergroep.Sfeergroep1, "foto", "straat", "1", "4", "gemeente", postcode, GebruikerType.Cliënt);
            Assert.Equal(postcode, gebruiker.Postcode);
        }


    }
}

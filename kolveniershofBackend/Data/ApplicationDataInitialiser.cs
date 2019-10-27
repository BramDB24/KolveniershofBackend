using kolveniershofBackend.Enums;
using kolveniershofBackend.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace kolveniershofBackend.Data
{
    public class ApplicationDataInitialiser
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<Gebruiker> _userManager;

        public ApplicationDataInitialiser(ApplicationDbContext dbContext, UserManager<Gebruiker> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                //gebruikers   
                //admin
                Gebruiker dina = new Gebruiker("dina", "dobbelaar", "dinadobbelaar@hotmail.com", Sfeergroep.Undefined,
                    "SomeURL", "kerkstraat", "55", "0", "Leuven", "4000", GebruikerType.Admin);

                Gebruiker jonah = new Gebruiker("jonah", "desmet", "jonahdesmet@hotmail.com", Sfeergroep.Sfeergroep1, "url", "straat", "10", "11", "MiddleOfNoWhere", "9000", GebruikerType.Admin);
                //begeleiders
                Gebruiker dieter = new Gebruiker("dieter", "dobbeleer", "dieterdobbeleer@hotmail.com", Sfeergroep.Undefined,
                    "SomeURL", "kerkstraat", "55", "0", "Leuven", "4000", GebruikerType.Begeleider);
                Gebruiker lucas = new Gebruiker("lucas", "vermeulen", "lucas@gmail.com", Sfeergroep.Undefined,
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", GebruikerType.Begeleider);
                //sfeergroep 1
                Gebruiker karo = new Gebruiker("karo", "dewez", "karo@hotmail.com", Sfeergroep.Sfeergroep1,
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", GebruikerType.Cliënt);
                Gebruiker thomas = new Gebruiker("thomas", "deweert", "thomas@hotmail.com", Sfeergroep.Sfeergroep1,
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", GebruikerType.Cliënt);
                Gebruiker frans = new Gebruiker("frans", "vermalen", "frans@gmail.com", Sfeergroep.Sfeergroep1,
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", GebruikerType.Cliënt);
                //sfeergroep 2
                Gebruiker jos = new Gebruiker("jos", "faas", "jos@hotmail.com", Sfeergroep.Sfeergroep2,
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", GebruikerType.Cliënt);
                Gebruiker laura = new Gebruiker("laura", "cramers", "laure@hotmail.com", Sfeergroep.Sfeergroep2,
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", GebruikerType.Cliënt);
                Gebruiker veerle = new Gebruiker("veerle", "denoode", "veerle@gmail.com", Sfeergroep.Sfeergroep2,
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", GebruikerType.Cliënt);
                //sfeergroep 3
                Gebruiker ken = new Gebruiker("ken", "deblezer", "ken@outlook.com", Sfeergroep.Sfeergroep3,
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", GebruikerType.Cliënt);
                Gebruiker nicolas = new Gebruiker("nicolas", "planckaer", "nicolas@gmail.com", Sfeergroep.Sfeergroep3,
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", GebruikerType.Cliënt);
                Gebruiker lisa = new Gebruiker("lisa", "janssens", "lisa@gmail.be", Sfeergroep.Sfeergroep3,
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", GebruikerType.Cliënt);

                var gebruikers = new List<Gebruiker> { dina, jonah, dieter, lucas, karo, thomas, frans,
                    jos, laura, veerle, ken, nicolas, lisa };
                foreach (Gebruiker g in gebruikers)
                {
                    await MaakGebruiker(g, "password1010");
                }


                //alle ateliers
                //gewone ateliers
                Atelier bakken = new Atelier(AtelierType.Gewoon, "bakken", "foto");
                Atelier feest = new Atelier(AtelierType.Gewoon, "feest", "foto"); ;
                Atelier koken = new Atelier(AtelierType.Gewoon, "koken", "foto");
                Atelier markt = new Atelier(AtelierType.Gewoon, "markt", "foto");
                Atelier praatcafe = new Atelier(AtelierType.Gewoon, "praatcafe", "foto");
                Atelier textiel = new Atelier(AtelierType.Gewoon, "textiel", "foto");
                Atelier tuin = new Atelier(AtelierType.Gewoon, "tuin", "foto");
                Atelier wandelen = new Atelier(AtelierType.Gewoon, "wandelen", "foto");
                Atelier yoga = new Atelier(AtelierType.Gewoon, "yoga", "foto");
                Atelier balanske = new Atelier(AtelierType.Gewoon, "balanske", "foto");
                Atelier crea = new Atelier(AtelierType.Gewoon, "crea", "foto");
                Atelier hout = new Atelier(AtelierType.Gewoon, "hout", "foto");
                Atelier kringgesprek = new Atelier(AtelierType.Gewoon, "kringgesprek", "foto");
                Atelier muziek = new Atelier(AtelierType.Gewoon, "muziek", "foto");
                Atelier provinciaalDomein = new Atelier(AtelierType.Gewoon, "provinciaal domein", "foto");
                Atelier snoezelen = new Atelier(AtelierType.Gewoon, "snoezelen", "foto");

                Atelier uitstap = new Atelier(AtelierType.Gewoon, "uitstap", "foto");
                Atelier zwemmen = new Atelier(AtelierType.Gewoon, "zwemmen", "foto");
                Atelier beleving = new Atelier(AtelierType.Gewoon, "beleving", "foto");
                Atelier digitaal = new Atelier(AtelierType.Gewoon, "digitaal", "foto");
                Atelier kaarsen = new Atelier(AtelierType.Gewoon, "kaarsen", "foto");
                Atelier kunst = new Atelier(AtelierType.Gewoon, "kunst", "foto");
                Atelier paardrijden = new Atelier(AtelierType.Gewoon, "paard rijden", "foto");
                Atelier spikEnSpan = new Atelier(AtelierType.Gewoon, "spik en span", "foto");
                Atelier tievo = new Atelier(AtelierType.Gewoon, "tievo", "foto");
                Atelier verhalen = new Atelier(AtelierType.Gewoon, "verhalen", "foto");
                Atelier werkplaats = new Atelier(AtelierType.Gewoon, "werkplaats", "foto");
                Atelier bib = new Atelier(AtelierType.Gewoon, "bib", "foto");
                Atelier expressie = new Atelier(AtelierType.Gewoon, "expressie", "foto");
                Atelier keukenEnAfwas = new Atelier(AtelierType.Gewoon, "keuken en afwas", "foto");
                Atelier levensboeken = new Atelier(AtelierType.Gewoon, "levensboeken", "foto");
                Atelier petanque = new Atelier(AtelierType.Gewoon, "petanque", "foto");
                Atelier sporten = new Atelier(AtelierType.Gewoon, "sporten", "foto");
                Atelier toneel = new Atelier(AtelierType.Gewoon, "toneel", "foto");
                Atelier vorming = new Atelier(AtelierType.Gewoon, "vorming", "foto");
                Atelier winkelen = new Atelier(AtelierType.Gewoon, "winkelen", "foto");

                //speciale ateliers
                Atelier afwezig = new Atelier(AtelierType.Afwezig, "afwezig", "foto");
                Atelier vervoer = new Atelier(AtelierType.VervoerAtelier, "vervoer", "foto");
                Atelier ziek = new Atelier(AtelierType.Ziek, "ziek", "foto");
                Atelier thuisVerlof = new Atelier(AtelierType.Thuis, "thuis verlof", "foto");

                var ateliers = new List<Atelier> {bakken, feest, koken, markt, praatcafe, textiel, tuin, wandelen, yoga, balanske, crea,
                hout, kringgesprek, muziek, provinciaalDomein, snoezelen, uitstap, zwemmen, beleving, digitaal, kaarsen, kunst,
                paardrijden, spikEnSpan, tievo, verhalen, werkplaats, bib, expressie, keukenEnAfwas, levensboeken, petanque,
                    sporten, toneel, vorming, winkelen};

                _dbContext.Ateliers.AddRange(ateliers);

                //dagAteliers
                DagAtelier kokenVoormiddag = new DagAtelier(DagMoment.Voormiddag, koken);
                DagAtelier zwemmenNamiddag = new DagAtelier(DagMoment.Namiddag, zwemmen);
                DagAtelier sportenVolledigeDag = new DagAtelier(DagMoment.VolledigeDag, sporten);
                DagAtelier expressieVoormiddag = new DagAtelier(DagMoment.Voormiddag, expressie);
                DagAtelier toneelNamiddag = new DagAtelier(DagMoment.Namiddag, toneel);
                DagAtelier winkelenVolledigeDag = new DagAtelier(DagMoment.VolledigeDag, winkelen);
                DagAtelier paardrijdenVoormiddag = new DagAtelier(DagMoment.Voormiddag, paardrijden);
                DagAtelier verhalenNamiddag = new DagAtelier(DagMoment.Namiddag, verhalen);
                DagAtelier petanqueVoormiddag = new DagAtelier(DagMoment.VolledigeDag, petanque);
                var dagAteliers = new List<DagAtelier> {kokenVoormiddag, zwemmenNamiddag, sportenVolledigeDag,
                    expressieVoormiddag,toneelNamiddag, winkelenVolledigeDag, paardrijdenVoormiddag,
                    verhalenNamiddag, petanqueVoormiddag };

                kokenVoormiddag.VoegGebruikerAanDagAtelierToe(karo);
                zwemmenNamiddag.VoegGebruikerAanDagAtelierToe(frans);
                sportenVolledigeDag.VoegGebruikerAanDagAtelierToe(lisa);
                expressieVoormiddag.VoegGebruikerAanDagAtelierToe(thomas);
                toneelNamiddag.VoegGebruikerAanDagAtelierToe(dieter);
                winkelenVolledigeDag.VoegGebruikerAanDagAtelierToe(dina);
                paardrijdenVoormiddag.VoegGebruikerAanDagAtelierToe(dieter);
                verhalenNamiddag.VoegGebruikerAanDagAtelierToe(veerle);
                petanqueVoormiddag.VoegGebruikerAanDagAtelierToe(dieter);
                //dagplanningTemplate
                //week1
                DagPlanningTemplate maandagWeek1 = new DagPlanningTemplate(1, Weekdag.Maandag);
                DagPlanningTemplate dinsdagWeek1 = new DagPlanningTemplate(1, Weekdag.Dinsdag);
                DagPlanningTemplate woensdagWeek1 = new DagPlanningTemplate(1, Weekdag.Woensdag);
                DagPlanningTemplate donderdagWeek1 = new DagPlanningTemplate(1, Weekdag.Donderdag);
                DagPlanningTemplate vrijdagWeek1 = new DagPlanningTemplate(1, Weekdag.Vrijdag);
                //week2
                DagPlanningTemplate maandagWeek2 = new DagPlanningTemplate(2, Weekdag.Maandag);
                DagPlanningTemplate dinsdagWeek2 = new DagPlanningTemplate(2, Weekdag.Dinsdag);
                DagPlanningTemplate woensdagWeek2 = new DagPlanningTemplate(2, Weekdag.Woensdag);
                DagPlanningTemplate donderdagWeek2 = new DagPlanningTemplate(2, Weekdag.Donderdag);
                DagPlanningTemplate vrijdagWeek2 = new DagPlanningTemplate(2, Weekdag.Vrijdag);
                //week3
                DagPlanningTemplate maandagWeek3 = new DagPlanningTemplate(3, Weekdag.Maandag);
                DagPlanningTemplate dinsdagWeek3 = new DagPlanningTemplate(3, Weekdag.Dinsdag);
                DagPlanningTemplate woensdagWeek3 = new DagPlanningTemplate(3, Weekdag.Woensdag);
                DagPlanningTemplate donderdagWeek3 = new DagPlanningTemplate(3, Weekdag.Donderdag);
                DagPlanningTemplate vrijdagWeek3 = new DagPlanningTemplate(3, Weekdag.Vrijdag);
                //week4
                DagPlanningTemplate maandagWeek4 = new DagPlanningTemplate(4, Weekdag.Maandag);
                DagPlanningTemplate dinsdagWeek4 = new DagPlanningTemplate(4, Weekdag.Dinsdag);
                DagPlanningTemplate woensdagWeek4 = new DagPlanningTemplate(4, Weekdag.Woensdag);
                DagPlanningTemplate donderdagWeek4 = new DagPlanningTemplate(4, Weekdag.Donderdag);
                DagPlanningTemplate vrijdagWeek4 = new DagPlanningTemplate(4, Weekdag.Vrijdag);


                var dagPlanningTemplates = new List<DagPlanningTemplate> {maandagWeek1, dinsdagWeek1, woensdagWeek1, donderdagWeek1, vrijdagWeek1,
                maandagWeek2, dinsdagWeek2, woensdagWeek2, donderdagWeek2, vrijdagWeek2,
                maandagWeek3, dinsdagWeek3, woensdagWeek3, donderdagWeek3, vrijdagWeek3,
                maandagWeek4, dinsdagWeek4, woensdagWeek4, donderdagWeek4, vrijdagWeek4,};

                _dbContext.DagPlanningen.AddRange(dagPlanningTemplates);

                maandagWeek1.VoegDagAtelierToeAanDagPlanningTemplate(kokenVoormiddag);
                maandagWeek1.VoegDagAtelierToeAanDagPlanningTemplate(zwemmenNamiddag);
                dinsdagWeek1.VoegDagAtelierToeAanDagPlanningTemplate(sportenVolledigeDag);
                woensdagWeek1.VoegDagAtelierToeAanDagPlanningTemplate(petanqueVoormiddag);
                donderdagWeek1.VoegDagAtelierToeAanDagPlanningTemplate(winkelenVolledigeDag);
                vrijdagWeek1.VoegDagAtelierToeAanDagPlanningTemplate(paardrijdenVoormiddag);
                vrijdagWeek1.VoegDagAtelierToeAanDagPlanningTemplate(verhalenNamiddag);

                //dagplanningen concreet
                DateTime dt = DateTime.Today;
                var vandaag = new DagPlanning(1, dt, "balletjes in tomatensaus en friet");
                _dbContext.DagPlanningen.Add(vandaag);
                for (int i = 1; i < 20; i++)
                {
                    var date = dt.AddDays(i);
                    var dp = new DagPlanning(2, date, "groenten, vlees en pasta");
                    _dbContext.DagPlanningen.Add(dp);
                }


                // _dbContext.DagPlanningen                                                  _dbContext.Gebruikers
                //      DAGPLANNINGEN       -->     DAGATELIERS     -->  DAGGEBRUIKERS    -->    GEBRUIKERS  
                //                                      |
                //                                  ATELIERS

                expressieVoormiddag.VoegGebruikerAanDagAtelierToe(jonah);
                expressieVoormiddag.VoegGebruikerAanDagAtelierToe(lucas);
                toneelNamiddag.VoegGebruikerAanDagAtelierToe(nicolas);
                vandaag.VoegDagAtelierToeAanDagPlanningTemplate(expressieVoormiddag);
                vandaag.VoegDagAtelierToeAanDagPlanningTemplate(toneelNamiddag);

                _dbContext.SaveChanges();

                //commentaar
                Commentaar commentaarBijGebruikerLaura1 = new Commentaar("tekst", CommentaarType.AlgemeenCommentaar);
                laura.addCommentaar(commentaarBijGebruikerLaura1);

                Commentaar commentaarBijGebruikerLaura2 = new Commentaar("tekst", CommentaarType.AlgemeenCommentaar);
                laura.addCommentaar(commentaarBijGebruikerLaura2);

                Commentaar commentaarBijGebruikerLucas1 = new Commentaar("tekst", CommentaarType.AlgemeenCommentaar);
                lucas.addCommentaar(commentaarBijGebruikerLucas1);

                var commentaar = new List<Commentaar> { commentaarBijGebruikerLaura1, commentaarBijGebruikerLaura2, commentaarBijGebruikerLucas1 };
                _dbContext.Commentaar.AddRange(commentaar);
                _dbContext.SaveChanges();

                Opmerking opmerking1 = new Opmerking(OpmerkingType.AteliersEnWeekschema, "atelier en weekschema test", DateTime.Today);
                Opmerking opmerking2 = new Opmerking(OpmerkingType.Begeleiding, "begeleiding test", DateTime.Today);
                Opmerking opmerking3 = new Opmerking(OpmerkingType.Cliënten, "clienten test", DateTime.Today);
                Opmerking opmerking4 = new Opmerking(OpmerkingType.Stagiairs, "stagiars test", DateTime.Today);
                Opmerking opmerking5 = new Opmerking(OpmerkingType.UurRegistratie, "uurregistratie", DateTime.Today);
                Opmerking opmerking6 = new Opmerking(OpmerkingType.Varia, "varia test", DateTime.Today);
                Opmerking opmerking7 = new Opmerking(OpmerkingType.Vervoer, "vervoer test", DateTime.Today);
                Opmerking opmerking8 = new Opmerking(OpmerkingType.Vrijwilligers, "vrijwilligers", DateTime.Today);
                Opmerking opmerking9 = new Opmerking(OpmerkingType.Logistiek, "logistiek test", DateTime.Today);
                

                var opmerkingen = new List<Opmerking> { opmerking1, opmerking2, opmerking3, opmerking4, opmerking5, opmerking6,
                opmerking7, opmerking8, opmerking9};
                _dbContext.Opmerkingen.AddRange(opmerkingen);
                _dbContext.SaveChanges();



            }
        }

        private async Task MaakGebruiker(Gebruiker gebruiker, string password)
        {
            await _userManager.CreateAsync(gebruiker, password);
            await _userManager.AddClaimAsync(gebruiker, new Claim(ClaimTypes.Role, gebruiker.Type.ToString()));

        }
    }
}

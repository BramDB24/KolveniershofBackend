using kolveniershofBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Data
{
    public class ApplicationDataInitialiser
    {

        private readonly ApplicationDbContext _dbContext;

        public ApplicationDataInitialiser(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                //gebruikers   
                //admin
                Gebruiker dina = new Gebruiker("dina", "dobbelaar", "dinadobbelaar@hotmail.com", Enums.Sfeergroep.Undefined,
                    "SomeURL", "kerkstraat", "55", "0", "Leuven", "4000", Enums.GebruikerType.Admin);
                
                //begeleiders
                Gebruiker dieter = new Gebruiker("dieter", "dobbeleer", "dieterdobbeleer@hotmail.com", Enums.Sfeergroep.Undefined,
                    "SomeURL", "kerkstraat", "55", "0", "Leuven", "4000", Enums.GebruikerType.Begeleider);
                Gebruiker lucas = new Gebruiker("lucas", "vermeulen", "lucas@gmail.com", Enums.Sfeergroep.Undefined,
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", Enums.GebruikerType.Begeleider);
                //sfeergroep 1
                Gebruiker karo = new Gebruiker("karo", "dewez", "karo@hotmail.com", Enums.Sfeergroep.Sfeergroep1,
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", Enums.GebruikerType.Cliënt);
                Gebruiker thomas = new Gebruiker("thomas", "deweert", "thomas@hotmail.com", Enums.Sfeergroep.Sfeergroep1,
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", Enums.GebruikerType.Cliënt);
                Gebruiker frans = new Gebruiker("frans", "vermalen", "frans@gmail.com", Enums.Sfeergroep.Sfeergroep1,
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", Enums.GebruikerType.Cliënt);
                //sfeergroep 2
                Gebruiker jos = new Gebruiker("jos", "faas", "jos@hotmail.com", Enums.Sfeergroep.Sfeergroep2,
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", Enums.GebruikerType.Cliënt);
                Gebruiker laura = new Gebruiker("laura", "cramers", "laure@hotmail.com", Enums.Sfeergroep.Sfeergroep2,
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", Enums.GebruikerType.Cliënt);
                Gebruiker veerle = new Gebruiker("veerle", "denoode", "veerle@gmail.com", Enums.Sfeergroep.Sfeergroep2,
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", Enums.GebruikerType.Cliënt);
                //sfeergroep 3
                Gebruiker ken = new Gebruiker("ken", "deblezer", "ken@outlook.com", Enums.Sfeergroep.Sfeergroep3,
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", Enums.GebruikerType.Cliënt);
                Gebruiker nicolas = new Gebruiker("nicolas", "planckaer", "nicolas@gmail.com", Enums.Sfeergroep.Sfeergroep3,
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", Enums.GebruikerType.Cliënt);
                Gebruiker lisa = new Gebruiker("lisa", "janssens", "lisa@gmail.be", Enums.Sfeergroep.Sfeergroep3,
                    "SomeURL", "Arendstraat", "5", "1", "Gent", "9000", Enums.GebruikerType.Cliënt);

                var gebruikers = new List<Gebruiker> { dina, dieter, lucas, karo, thomas, frans, jos, laura, veerle, ken, nicolas, lisa };
                foreach (Gebruiker g in gebruikers)
                {
                    _dbContext.Gebruikers.Add(g);
                }
                _dbContext.SaveChanges();

                //alle ateliers
                //gewone ateliers
                Atelier bakken = new Atelier(Enums.AtelierType.Gewoon, "bakken", "foto");
                Atelier feest = new Atelier(Enums.AtelierType.Gewoon, "feest", "foto"); ;
                Atelier koken = new Atelier(Enums.AtelierType.Gewoon, "koken", "foto");
                Atelier markt = new Atelier(Enums.AtelierType.Gewoon, "markt", "foto");
                Atelier praatcafe = new Atelier(Enums.AtelierType.Gewoon, "praatcafe", "foto");
                Atelier textiel = new Atelier(Enums.AtelierType.Gewoon, "textiel", "foto");
                Atelier tuin = new Atelier(Enums.AtelierType.Gewoon, "tuin", "foto");
                Atelier wandelen = new Atelier(Enums.AtelierType.Gewoon, "wandelen", "foto");
                Atelier yoga = new Atelier(Enums.AtelierType.Gewoon, "yoga", "foto");
                Atelier balanske = new Atelier(Enums.AtelierType.Gewoon, "balanske", "foto");
                Atelier crea = new Atelier(Enums.AtelierType.Gewoon, "crea", "foto");
                Atelier hout = new Atelier(Enums.AtelierType.Gewoon, "hout", "foto");
                Atelier kringgesprek = new Atelier(Enums.AtelierType.Gewoon, "kringgesprek", "foto");
                Atelier muziek = new Atelier(Enums.AtelierType.Gewoon, "muziek", "foto");
                Atelier provinciaalDomein = new Atelier(Enums.AtelierType.Gewoon, "provinciaal domein", "foto");
                Atelier snoezelen = new Atelier(Enums.AtelierType.Gewoon, "snoezelen", "foto");
               
                Atelier uitstap = new Atelier(Enums.AtelierType.Gewoon, "uitstap", "foto");
                Atelier zwemmen = new Atelier(Enums.AtelierType.Gewoon, "zwemmen", "foto");
                Atelier beleving = new Atelier(Enums.AtelierType.Gewoon, "beleving", "foto");
                Atelier digitaal = new Atelier(Enums.AtelierType.Gewoon, "digitaal", "foto");
                Atelier kaarsen = new Atelier(Enums.AtelierType.Gewoon, "kaarsen", "foto");
                Atelier kunst = new Atelier(Enums.AtelierType.Gewoon, "kunst", "foto");
                Atelier paardrijden = new Atelier(Enums.AtelierType.Gewoon, "paard rijden", "foto");
                Atelier spikEnSpan = new Atelier(Enums.AtelierType.Gewoon, "spik en span", "foto");
                Atelier tievo = new Atelier(Enums.AtelierType.Gewoon, "tievo", "foto");
                Atelier verhalen = new Atelier(Enums.AtelierType.Gewoon, "verhalen", "foto");
                Atelier werkplaats = new Atelier(Enums.AtelierType.Gewoon, "werkplaats", "foto");
                Atelier bib = new Atelier(Enums.AtelierType.Gewoon, "bib", "foto");
                Atelier expressie = new Atelier(Enums.AtelierType.Gewoon, "expressie", "foto");
                Atelier keukenEnAfwas = new Atelier(Enums.AtelierType.Gewoon, "keuken en afwas", "foto");
                Atelier levensboeken = new Atelier(Enums.AtelierType.Gewoon, "levensboeken", "foto");
                Atelier petanque = new Atelier(Enums.AtelierType.Gewoon, "petanque", "foto");
                Atelier sporten = new Atelier(Enums.AtelierType.Gewoon, "sporten", "foto");
                Atelier toneel = new Atelier(Enums.AtelierType.Gewoon, "toneel", "foto");
                Atelier vorming = new Atelier(Enums.AtelierType.Gewoon, "vorming", "foto");
                Atelier winkelen = new Atelier(Enums.AtelierType.Gewoon, "winkelen", "foto");

                //speciale ateliers
                Atelier afwezig = new Atelier(Enums.AtelierType.Afwezig, "afwezig", "foto");
                Atelier vervoer = new Atelier(Enums.AtelierType.VervoerAtelier, "vervoer", "foto");
                Atelier ziek = new Atelier(Enums.AtelierType.Ziek, "ziek", "foto");
                Atelier thuisVerlof = new Atelier(Enums.AtelierType.Thuis, "thuis verlof", "foto");

                var ateliers = new List<Atelier> {bakken, feest, koken, markt, praatcafe, textiel, tuin, wandelen, yoga, balanske, crea,
                hout, kringgesprek, muziek, provinciaalDomein, snoezelen, uitstap, zwemmen, beleving, digitaal, kaarsen, kunst,
                paardrijden, spikEnSpan, tievo, verhalen, werkplaats, bib, expressie, keukenEnAfwas, levensboeken, petanque,
                    sporten, toneel, vorming, winkelen};
                foreach (Atelier a in ateliers)
                {
                    _dbContext.Ateliers.Add(a);
                }
                _dbContext.SaveChanges();

                //dagAteliers
                DagAtelier kokenOpMaandagVoormiddag = new DagAtelier(Enums.DagMoment.Voormiddag, koken);
                DagAtelier zwemmenOpMaandagNamiddag = new DagAtelier(Enums.DagMoment.Namiddag, zwemmen);
                DagAtelier sportenOpDinsdagVolledigeDag = new DagAtelier(Enums.DagMoment.VolledigeDag, sporten);
                DagAtelier expressieOpWoensdagVoormiddag = new DagAtelier(Enums.DagMoment.Voormiddag, expressie);
                DagAtelier toneelOpWoensdagNamiddag = new DagAtelier(Enums.DagMoment.Namiddag, toneel);
                DagAtelier winkelenOpDonderdagVolledigeDag = new DagAtelier(Enums.DagMoment.VolledigeDag, winkelen);
                DagAtelier paardrijdenOpVrijdagVoormiddag = new DagAtelier(Enums.DagMoment.Voormiddag, paardrijden);
                DagAtelier verhalenOpVrijdagNamiddag = new DagAtelier(Enums.DagMoment.Namiddag, verhalen);
                var dagAteliers = new List<DagAtelier> {kokenOpMaandagVoormiddag, zwemmenOpMaandagNamiddag, sportenOpDinsdagVolledigeDag,
                    expressieOpWoensdagVoormiddag,toneelOpWoensdagNamiddag, winkelenOpDonderdagVolledigeDag, paardrijdenOpVrijdagVoormiddag,
                    verhalenOpVrijdagNamiddag };
                foreach (DagAtelier da in dagAteliers)
                {
                    _dbContext.DagAteliers.Add(da);
                }
                _dbContext.SaveChanges();

                //dagplanningTemplate
                //week1
                DagPlanningTemplate maandagWeek1 = new DagPlanningTemplate(1, Enums.Weekdag.Maandag, true);
                DagPlanningTemplate dinsdagWeek1 = new DagPlanningTemplate(1, Enums.Weekdag.Dinsdag, true);
                DagPlanningTemplate woensdagWeek1 = new DagPlanningTemplate(1, Enums.Weekdag.Woensdag, true);
                DagPlanningTemplate donderdagWeek1 = new DagPlanningTemplate(1, Enums.Weekdag.Donderdag, true);
                DagPlanningTemplate vrijdagWeek1 = new DagPlanningTemplate(1, Enums.Weekdag.Vrijdag, true);
                //week2
                DagPlanningTemplate maandagWeek2 = new DagPlanningTemplate(2, Enums.Weekdag.Maandag, true);
                DagPlanningTemplate dinsdagWeek2 = new DagPlanningTemplate(2, Enums.Weekdag.Dinsdag, true);
                DagPlanningTemplate woensdagWeek2 = new DagPlanningTemplate(2, Enums.Weekdag.Woensdag, true);
                DagPlanningTemplate donderdagWeek2 = new DagPlanningTemplate(2, Enums.Weekdag.Donderdag, true);
                DagPlanningTemplate vrijdagWeek2 = new DagPlanningTemplate(2, Enums.Weekdag.Vrijdag, true);
                //week3
                DagPlanningTemplate maandagWeek3 = new DagPlanningTemplate(3, Enums.Weekdag.Maandag, true);
                DagPlanningTemplate dinsdagWeek3 = new DagPlanningTemplate(3, Enums.Weekdag.Dinsdag, true);
                DagPlanningTemplate woensdagWeek3 = new DagPlanningTemplate(3, Enums.Weekdag.Woensdag, true);
                DagPlanningTemplate donderdagWeek3 = new DagPlanningTemplate(3, Enums.Weekdag.Donderdag, true);
                DagPlanningTemplate vrijdagWeek3 = new DagPlanningTemplate(3, Enums.Weekdag.Vrijdag, true);
                //week4
                DagPlanningTemplate maandagWeek4 = new DagPlanningTemplate(4, Enums.Weekdag.Maandag, true);
                DagPlanningTemplate dinsdagWeek4 = new DagPlanningTemplate(4, Enums.Weekdag.Dinsdag, true);
                DagPlanningTemplate woensdagWeek4 = new DagPlanningTemplate(4, Enums.Weekdag.Woensdag, true);
                DagPlanningTemplate donderdagWeek4 = new DagPlanningTemplate(4, Enums.Weekdag.Donderdag, true);
                DagPlanningTemplate vrijdagWeek4 = new DagPlanningTemplate(4, Enums.Weekdag.Vrijdag, true);

                var DagPlanningTemplates = new List<DagPlanningTemplate> {maandagWeek1, dinsdagWeek1, woensdagWeek1, donderdagWeek1, vrijdagWeek1,
                maandagWeek2, dinsdagWeek2, woensdagWeek2, donderdagWeek2, vrijdagWeek2,
                maandagWeek3, dinsdagWeek3, woensdagWeek3, donderdagWeek3, vrijdagWeek3,
                maandagWeek4, dinsdagWeek4, woensdagWeek4, donderdagWeek4, vrijdagWeek4};
                foreach (DagPlanningTemplate dpt in DagPlanningTemplates)
                {
                    _dbContext.DagPlanningen.Add(dpt);
                }
                _dbContext.SaveChanges();

                //dagplanningen
                DateTime dt = DateTime.Today;
                var dagplanning = new DagPlanning(1, false, new DateTime(2020, 5, 11), "balletjes in tomatensaus en friet");
                _dbContext.DagPlanningenConcreet.Add(dagplanning);
                for(int i= 1; i<20; i++)
                {
                    var date = dt.AddDays(i);
                    var dp = new DagPlanning(2, false, date, "groenten, vlees en pasta");
                    _dbContext.DagPlanningenConcreet.Add(dp);
                }
                Console.WriteLine(dt);


                _dbContext.SaveChanges();
            }
        }
    }
}

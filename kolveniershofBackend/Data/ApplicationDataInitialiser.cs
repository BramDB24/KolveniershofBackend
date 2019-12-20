using kolveniershofBackend.Enums;
using kolveniershofBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            if (_dbContext.Database.EnsureCreated()) //niet gebruiken bij azure (migrations)
                {

                #region Gebruikers
                //admin
                Gebruiker dina = new Gebruiker("dina", "dobbelaar", "dinadobbelaar@hotmail.com", Sfeergroep.Undefined,
                     "johanna.jpg", GebruikerType.Admin);

                Gebruiker jonah = new Gebruiker("jonah", "desmet", "jonahdesmet@hotmail.com", Sfeergroep.Sfeergroep1,
                    "jonah.jpg", GebruikerType.Admin);
                //begeleiders
                Gebruiker dieter = new Gebruiker("dieter", "dobbeleer", "dieterdobbeleer@hotmail.com", Sfeergroep.Undefined,
                    "bram.jpg", GebruikerType.Begeleider);
                Gebruiker lucas = new Gebruiker("lucas", "vermeulen", "lucas@gmail.com", Sfeergroep.Undefined,
                    "lucas.jpg", GebruikerType.Begeleider);
                //sfeergroep 1
                Gebruiker karo = new Gebruiker("karo", "dewez", "karo@hotmail.com", Sfeergroep.Sfeergroep1,
                    "kato.jpg", GebruikerType.Cliënt);
                Gebruiker thomas = new Gebruiker("thomas", "deweert", "thomas@hotmail.com", Sfeergroep.Sfeergroep1,
                    "robin.jpg", GebruikerType.Cliënt);
                Gebruiker frans = new Gebruiker("frans", "vermalen", "frans@gmail.com", Sfeergroep.Sfeergroep1,
                    "jonah.jpg", GebruikerType.Cliënt);
                //sfeergroep 2
                Gebruiker jos = new Gebruiker("jos", "faas", "jos@hotmail.com", Sfeergroep.Sfeergroep2,
                    "bram.jpg", GebruikerType.Cliënt);
                Gebruiker laura = new Gebruiker("laura", "cramers", "laure@hotmail.com", Sfeergroep.Sfeergroep2,
                    "johanna.jpg", GebruikerType.Cliënt);
                Gebruiker veerle = new Gebruiker("veerle", "denoode", "veerle@gmail.com", Sfeergroep.Sfeergroep2,
                    "kato.jpg", GebruikerType.Cliënt);
                //sfeergroep 3
                Gebruiker ken = new Gebruiker("ken", "deblezer", "ken@outlook.com", Sfeergroep.Sfeergroep3,
                    "lucas.jpg", GebruikerType.Cliënt);
                Gebruiker nicolas = new Gebruiker("nicolas", "planckaer", "nicolas@gmail.com", Sfeergroep.Sfeergroep3,
                    "robin.jpg", GebruikerType.Cliënt);
                Gebruiker lisa = new Gebruiker("lisa", "janssens", "lisa@gmail.be", Sfeergroep.Sfeergroep3,
                    "johanna.jpg", GebruikerType.Cliënt);

                var gebruikers = new List<Gebruiker> { dina, jonah, dieter, lucas, karo, thomas, frans,
                    jos, laura, veerle, ken, nicolas, lisa };
                foreach (Gebruiker g in gebruikers)
                {
                    await MaakGebruiker(g, "password1010");
                }
                #endregion

                #region Ateliers

                //gewone ateliers
                Atelier bakken = new Atelier(AtelierType.Gewoon, "bakken", "bakken.jpg");
                Atelier balanske = new Atelier(AtelierType.Gewoon, "balanske", "balanske.jpg");
                Atelier beleving = new Atelier(AtelierType.Gewoon, "beleving", "belevingsatelier.jpg");
                Atelier bib = new Atelier(AtelierType.Gewoon, "bib", "bib.jpg");
                //blanco ateliers?
                Atelier cafetariaRozenberg = new Atelier(AtelierType.Gewoon, "Cafetaria Rozenberg", "cafetaria_rozenberg.jpg");
                Atelier crea = new Atelier(AtelierType.Gewoon, "crea", "crea.jpg");
                Atelier digitaal = new Atelier(AtelierType.Gewoon, "digitaal", "digitaal_atelier.jpg");
                Atelier expressie = new Atelier(AtelierType.Gewoon, "expressie", "expressie.jpg");
                Atelier feest = new Atelier(AtelierType.Gewoon, "feest", "feest.jpg"); ;
                Atelier hout = new Atelier(AtelierType.Gewoon, "hout", "houtatelier.jpg");
                Atelier kaarsen = new Atelier(AtelierType.Gewoon, "kaarsen", "kaarsenatelier.jpg");
                Atelier keukenEnAfwas = new Atelier(AtelierType.Gewoon, "keuken en afwas", "keuken_afwas.jpg");
                Atelier koken = new Atelier(AtelierType.Gewoon, "koken", "koken.jpg");
                Atelier kringgesprek = new Atelier(AtelierType.Gewoon, "kringgesprek", "kringgesprek.jpg");
                Atelier kunst = new Atelier(AtelierType.Gewoon, "kunst", "kunstatelier.jpg");
                Atelier levensboeken = new Atelier(AtelierType.Gewoon, "levensboeken", "levensboeken.jpg");
                Atelier markt = new Atelier(AtelierType.Gewoon, "markt", "markt.jpg");
                Atelier muziek = new Atelier(AtelierType.Gewoon, "muziek", "muziek.jpg");
                Atelier paardrijden = new Atelier(AtelierType.Gewoon, "paard rijden", "paardrijden.jpg");
                Atelier petanque = new Atelier(AtelierType.Gewoon, "petanque", "petanque.jpg");
                Atelier praatcafe = new Atelier(AtelierType.Gewoon, "praatcafe", "praatcafe.jpg");
                Atelier provinciaalDomein = new Atelier(AtelierType.Gewoon, "provinciaal domein", "provinciaal_domein.jpg");
                Atelier snoezelen = new Atelier(AtelierType.Gewoon, "snoezelen", "snoezelen.jpg");
                Atelier spikEnSpan = new Atelier(AtelierType.Gewoon, "spik en span", "spik_en_span.jpg");
                Atelier sporten = new Atelier(AtelierType.Gewoon, "sporten", "sporten2.jpg");
                Atelier textiel = new Atelier(AtelierType.Gewoon, "textiel", "textiel.jpg");
                Atelier tievo = new Atelier(AtelierType.Gewoon, "tievo", "tievo.jpg");
                Atelier toneel = new Atelier(AtelierType.Gewoon, "toneel", "toneel.jpg");
                Atelier tuin = new Atelier(AtelierType.Gewoon, "tuin", "tuin.jpg");
                Atelier uitstap = new Atelier(AtelierType.Gewoon, "uitstap", "uitstap.jpg");
                Atelier verhalen = new Atelier(AtelierType.Gewoon, "verhalen", "verhalen.jpg");
                Atelier vorming = new Atelier(AtelierType.Gewoon, "vorming", "vorming.jpg");
                Atelier wandelen = new Atelier(AtelierType.Gewoon, "wandelen", "wandelen.jpg");
                Atelier weekschema = new Atelier(AtelierType.Gewoon, "weekschema", "weekschema.jpg");
                Atelier werkplaats = new Atelier(AtelierType.Gewoon, "werkplaats", "werkplaats.jpg");
                Atelier winkelen = new Atelier(AtelierType.Gewoon, "winkelen", "winkelen.jpg");
                Atelier yoga = new Atelier(AtelierType.Gewoon, "yoga", "yoga.jpg");
                Atelier zwemmen = new Atelier(AtelierType.Gewoon, "zwemmen", "zwemmen.jpg");

                //speciale ateliers
                Atelier afwezig = new Atelier(AtelierType.Afwezig, "afwezig", "blanco.jpg");
                Atelier beigebus = new Atelier(AtelierType.VervoerAtelier, "beige bus", "bus.jpg");
                Atelier blauwebus = new Atelier(AtelierType.VervoerAtelier, "blauwe bus", "bus.jpg");
                Atelier gelebus = new Atelier(AtelierType.VervoerAtelier, "gele bus", "bus.jpg");
                Atelier ziek = new Atelier(AtelierType.Ziek, "ziek", "ziek.png");
                Atelier thuisVerlof = new Atelier(AtelierType.Thuis, "thuis verlof", "thuis_verlof.jpg");

                var ateliers = new List<Atelier> {bakken, feest, koken, markt, praatcafe, textiel, tuin, wandelen, yoga, balanske, crea,
                hout, kringgesprek, muziek, provinciaalDomein, snoezelen, uitstap, zwemmen, beleving, digitaal, kaarsen, kunst,
                paardrijden, spikEnSpan, tievo, verhalen, werkplaats, bib, expressie, keukenEnAfwas, levensboeken, petanque,
                    sporten, toneel, vorming, winkelen, afwezig, beigebus, blauwebus, gelebus, ziek, thuisVerlof};

                _dbContext.Ateliers.AddRange(ateliers);

                #endregion

                #region DagAteliers
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
                DagAtelier afwezigVolledigeDag = new DagAtelier(DagMoment.Undefined, afwezig);
                DagAtelier gelebusDag = new DagAtelier(DagMoment.Undefined, gelebus);
                DagAtelier blauwebusDag = new DagAtelier(DagMoment.Undefined, blauwebus);
                DagAtelier beigebusDag = new DagAtelier(DagMoment.Undefined, beigebus);
                DagAtelier ziekVolledigDag = new DagAtelier(DagMoment.Undefined, ziek);
                DagAtelier thuisvervofVolledigeDag = new DagAtelier(DagMoment.Undefined, thuisVerlof);

                var dagAteliers = new List<DagAtelier> {kokenVoormiddag, zwemmenNamiddag, sportenVolledigeDag,
                    expressieVoormiddag,toneelNamiddag, winkelenVolledigeDag, paardrijdenVoormiddag,
                    verhalenNamiddag, petanqueVoormiddag, afwezigVolledigeDag, gelebusDag, blauwebusDag, beigebusDag, ziekVolledigDag, thuisvervofVolledigeDag };
                #endregion


                #region Gebruikerstoevoegen
                gelebusDag.VoegGebruikerAanDagAtelierToe(karo);
                gelebusDag.VoegGebruikerAanDagAtelierToe(jos);
                gelebusDag.VoegGebruikerAanDagAtelierToe(laura);
                gelebusDag.VoegGebruikerAanDagAtelierToe(veerle);
                gelebusDag.VoegGebruikerAanDagAtelierToe(dieter);
                blauwebusDag.VoegGebruikerAanDagAtelierToe(frans);
                blauwebusDag.VoegGebruikerAanDagAtelierToe(thomas);
                blauwebusDag.VoegGebruikerAanDagAtelierToe(lucas);
                beigebusDag.VoegGebruikerAanDagAtelierToe(ken);
                beigebusDag.VoegGebruikerAanDagAtelierToe(nicolas);

                afwezigVolledigeDag.VoegGebruikerAanDagAtelierToe(ken);
                afwezigVolledigeDag.VoegGebruikerAanDagAtelierToe(nicolas);
                afwezigVolledigeDag.VoegGebruikerAanDagAtelierToe(laura);

                thuisvervofVolledigeDag.VoegGebruikerAanDagAtelierToe(dieter);
                thuisvervofVolledigeDag.VoegGebruikerAanDagAtelierToe(lucas);

                kokenVoormiddag.VoegGebruikerAanDagAtelierToe(karo);
                kokenVoormiddag.VoegGebruikerAanDagAtelierToe(jos);
                kokenVoormiddag.VoegGebruikerAanDagAtelierToe(laura);
                kokenVoormiddag.VoegGebruikerAanDagAtelierToe(veerle);
                kokenVoormiddag.VoegGebruikerAanDagAtelierToe(ken);
                kokenVoormiddag.VoegGebruikerAanDagAtelierToe(nicolas);
                kokenVoormiddag.VoegGebruikerAanDagAtelierToe(lucas);

                zwemmenNamiddag.VoegGebruikerAanDagAtelierToe(frans);
                zwemmenNamiddag.VoegGebruikerAanDagAtelierToe(karo);
                zwemmenNamiddag.VoegGebruikerAanDagAtelierToe(dina);
                zwemmenNamiddag.VoegGebruikerAanDagAtelierToe(dieter);

                sportenVolledigeDag.VoegGebruikerAanDagAtelierToe(karo);
                sportenVolledigeDag.VoegGebruikerAanDagAtelierToe(jos);
                sportenVolledigeDag.VoegGebruikerAanDagAtelierToe(laura);
                sportenVolledigeDag.VoegGebruikerAanDagAtelierToe(veerle);
                sportenVolledigeDag.VoegGebruikerAanDagAtelierToe(ken);
                sportenVolledigeDag.VoegGebruikerAanDagAtelierToe(nicolas);
                sportenVolledigeDag.VoegGebruikerAanDagAtelierToe(lucas);

                expressieVoormiddag.VoegGebruikerAanDagAtelierToe(frans);
                expressieVoormiddag.VoegGebruikerAanDagAtelierToe(karo);
                expressieVoormiddag.VoegGebruikerAanDagAtelierToe(dina);
                expressieVoormiddag.VoegGebruikerAanDagAtelierToe(thomas);

                toneelNamiddag.VoegGebruikerAanDagAtelierToe(frans);
                toneelNamiddag.VoegGebruikerAanDagAtelierToe(veerle);
                toneelNamiddag.VoegGebruikerAanDagAtelierToe(dina);
                toneelNamiddag.VoegGebruikerAanDagAtelierToe(jos);

                winkelenVolledigeDag.VoegGebruikerAanDagAtelierToe(frans);
                winkelenVolledigeDag.VoegGebruikerAanDagAtelierToe(karo);
                winkelenVolledigeDag.VoegGebruikerAanDagAtelierToe(dina);
                winkelenVolledigeDag.VoegGebruikerAanDagAtelierToe(dieter);

                paardrijdenVoormiddag.VoegGebruikerAanDagAtelierToe(dieter);
                paardrijdenVoormiddag.VoegGebruikerAanDagAtelierToe(nicolas);
                paardrijdenVoormiddag.VoegGebruikerAanDagAtelierToe(dina);
                paardrijdenVoormiddag.VoegGebruikerAanDagAtelierToe(karo);

                verhalenNamiddag.VoegGebruikerAanDagAtelierToe(karo);
                verhalenNamiddag.VoegGebruikerAanDagAtelierToe(jos);
                verhalenNamiddag.VoegGebruikerAanDagAtelierToe(laura);
                verhalenNamiddag.VoegGebruikerAanDagAtelierToe(dieter);
                verhalenNamiddag.VoegGebruikerAanDagAtelierToe(ken);
                verhalenNamiddag.VoegGebruikerAanDagAtelierToe(dina);
                verhalenNamiddag.VoegGebruikerAanDagAtelierToe(lucas);

                petanqueVoormiddag.VoegGebruikerAanDagAtelierToe(frans);
                petanqueVoormiddag.VoegGebruikerAanDagAtelierToe(karo);
                petanqueVoormiddag.VoegGebruikerAanDagAtelierToe(dina);
                petanqueVoormiddag.VoegGebruikerAanDagAtelierToe(dieter);

                ziekVolledigDag.VoegGebruikerAanDagAtelierToe(frans);
                ziekVolledigDag.VoegGebruikerAanDagAtelierToe(veerle);
                ziekVolledigDag.VoegGebruikerAanDagAtelierToe(thomas);

                #endregion

                #region DagPlanningTemplates
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
                #endregion

                #region Template seeding

                maandagWeek1.VoegDagateliersToe(ziek).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek1.VoegDagateliersToe(koken).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek1.VoegDagateliersToe(zwemmen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek1.VoegDagateliersToe(winkelen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek1.VoegDagateliersToe(paardrijden).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek1.VoegDagateliersToe(expressie).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek1.VoegDagateliersToe(petanque).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek1.VoegDagateliersToe(toneel).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek1.VoegDagateliersToe(verhalen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));

                dinsdagWeek1.VoegDagateliersToe(ziek).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                dinsdagWeek1.VoegDagateliersToe(zwemmen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                dinsdagWeek1.VoegDagateliersToe(paardrijden).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                dinsdagWeek1.VoegDagateliersToe(expressie).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                dinsdagWeek1.VoegDagateliersToe(petanque).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                dinsdagWeek1.VoegDagateliersToe(verhalen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));

                woensdagWeek1.VoegDagateliersToe(ziek).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek1.VoegDagateliersToe(koken).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek1.VoegDagateliersToe(zwemmen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek1.VoegDagateliersToe(winkelen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek1.VoegDagateliersToe(paardrijden).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek1.VoegDagateliersToe(expressie).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek1.VoegDagateliersToe(petanque).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek1.VoegDagateliersToe(toneel).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek1.VoegDagateliersToe(verhalen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));

                donderdagWeek1.VoegDagateliersToe(ziek).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek1.VoegDagateliersToe(zwemmen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek1.VoegDagateliersToe(winkelen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek1.VoegDagateliersToe(paardrijden).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek1.VoegDagateliersToe(petanque).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek1.VoegDagateliersToe(toneel).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek1.VoegDagateliersToe(verhalen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));

                vrijdagWeek1.VoegDagateliersToe(ziek).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek1.VoegDagateliersToe(koken).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek1.VoegDagateliersToe(zwemmen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek1.VoegDagateliersToe(paardrijden).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek1.VoegDagateliersToe(expressie).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek1.VoegDagateliersToe(petanque).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek1.VoegDagateliersToe(toneel).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));

                maandagWeek2.VoegDagateliersToe(ziek).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek2.VoegDagateliersToe(koken).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek2.VoegDagateliersToe(zwemmen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek2.VoegDagateliersToe(winkelen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek2.VoegDagateliersToe(paardrijden).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek2.VoegDagateliersToe(expressie).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek2.VoegDagateliersToe(petanque).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek2.VoegDagateliersToe(toneel).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek2.VoegDagateliersToe(verhalen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));

                dinsdagWeek2.VoegDagateliersToe(ziek).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                dinsdagWeek2.VoegDagateliersToe(zwemmen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                dinsdagWeek2.VoegDagateliersToe(paardrijden).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                dinsdagWeek2.VoegDagateliersToe(expressie).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                dinsdagWeek2.VoegDagateliersToe(petanque).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                dinsdagWeek2.VoegDagateliersToe(verhalen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));

                woensdagWeek2.VoegDagateliersToe(ziek).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek2.VoegDagateliersToe(koken).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek2.VoegDagateliersToe(zwemmen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek2.VoegDagateliersToe(winkelen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek2.VoegDagateliersToe(paardrijden).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek2.VoegDagateliersToe(expressie).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek2.VoegDagateliersToe(petanque).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek2.VoegDagateliersToe(toneel).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek2.VoegDagateliersToe(verhalen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));

                donderdagWeek2.VoegDagateliersToe(ziek).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek2.VoegDagateliersToe(zwemmen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek2.VoegDagateliersToe(winkelen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek2.VoegDagateliersToe(paardrijden).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek2.VoegDagateliersToe(petanque).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek2.VoegDagateliersToe(toneel).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek2.VoegDagateliersToe(verhalen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));

                vrijdagWeek2.VoegDagateliersToe(ziek).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek2.VoegDagateliersToe(koken).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek2.VoegDagateliersToe(zwemmen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek2.VoegDagateliersToe(paardrijden).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek2.VoegDagateliersToe(expressie).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek2.VoegDagateliersToe(petanque).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek2.VoegDagateliersToe(toneel).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));

                maandagWeek3.VoegDagateliersToe(ziek).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek3.VoegDagateliersToe(koken).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek3.VoegDagateliersToe(zwemmen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek3.VoegDagateliersToe(winkelen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek3.VoegDagateliersToe(paardrijden).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek3.VoegDagateliersToe(expressie).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek3.VoegDagateliersToe(petanque).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek3.VoegDagateliersToe(toneel).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek3.VoegDagateliersToe(verhalen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));

                dinsdagWeek3.VoegDagateliersToe(ziek).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                dinsdagWeek3.VoegDagateliersToe(zwemmen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                dinsdagWeek3.VoegDagateliersToe(paardrijden).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                dinsdagWeek3.VoegDagateliersToe(expressie).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                dinsdagWeek3.VoegDagateliersToe(petanque).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                dinsdagWeek3.VoegDagateliersToe(verhalen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));

                woensdagWeek3.VoegDagateliersToe(ziek).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek3.VoegDagateliersToe(koken).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek3.VoegDagateliersToe(zwemmen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek3.VoegDagateliersToe(winkelen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek3.VoegDagateliersToe(paardrijden).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek3.VoegDagateliersToe(expressie).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek3.VoegDagateliersToe(petanque).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek3.VoegDagateliersToe(toneel).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek3.VoegDagateliersToe(verhalen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));

                donderdagWeek3.VoegDagateliersToe(ziek).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek3.VoegDagateliersToe(zwemmen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek3.VoegDagateliersToe(winkelen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek3.VoegDagateliersToe(paardrijden).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek3.VoegDagateliersToe(petanque).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek3.VoegDagateliersToe(toneel).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek3.VoegDagateliersToe(verhalen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));

                vrijdagWeek3.VoegDagateliersToe(ziek).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek3.VoegDagateliersToe(koken).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek3.VoegDagateliersToe(zwemmen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek3.VoegDagateliersToe(paardrijden).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek3.VoegDagateliersToe(expressie).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek3.VoegDagateliersToe(petanque).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek3.VoegDagateliersToe(toneel).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));

                maandagWeek4.VoegDagateliersToe(ziek).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek4.VoegDagateliersToe(koken).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek4.VoegDagateliersToe(zwemmen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek4.VoegDagateliersToe(winkelen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek4.VoegDagateliersToe(paardrijden).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek4.VoegDagateliersToe(expressie).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek4.VoegDagateliersToe(petanque).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek4.VoegDagateliersToe(toneel).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                maandagWeek4.VoegDagateliersToe(verhalen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));

                dinsdagWeek4.VoegDagateliersToe(ziek).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                dinsdagWeek4.VoegDagateliersToe(zwemmen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                dinsdagWeek4.VoegDagateliersToe(paardrijden).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                dinsdagWeek4.VoegDagateliersToe(expressie).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                dinsdagWeek4.VoegDagateliersToe(petanque).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                dinsdagWeek4.VoegDagateliersToe(verhalen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));

                woensdagWeek4.VoegDagateliersToe(ziek).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek4.VoegDagateliersToe(koken).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek4.VoegDagateliersToe(zwemmen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek4.VoegDagateliersToe(winkelen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek4.VoegDagateliersToe(paardrijden).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek4.VoegDagateliersToe(expressie).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek4.VoegDagateliersToe(petanque).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek4.VoegDagateliersToe(toneel).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                woensdagWeek4.VoegDagateliersToe(verhalen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));

                donderdagWeek4.VoegDagateliersToe(ziek).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek4.VoegDagateliersToe(zwemmen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek4.VoegDagateliersToe(winkelen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek4.VoegDagateliersToe(paardrijden).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek4.VoegDagateliersToe(petanque).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek4.VoegDagateliersToe(toneel).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                donderdagWeek4.VoegDagateliersToe(verhalen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));

                vrijdagWeek4.VoegDagateliersToe(ziek).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek4.VoegDagateliersToe(koken).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek4.VoegDagateliersToe(zwemmen).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek4.VoegDagateliersToe(paardrijden).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek4.VoegDagateliersToe(expressie).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek4.VoegDagateliersToe(petanque).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));
                vrijdagWeek4.VoegDagateliersToe(toneel).VoegGebruikersToe(selecteerRandomGebruikers(gebruikers));

                Template template1 = new Template("zomer");
                template1.DagPlanningTemplates = dagPlanningTemplates;
                template1.IsActief = true;

                Template template2 = new Template("winter");
                template1.DagPlanningTemplates = dagPlanningTemplates;
                var list = new List<DagPlanningTemplate>();
                for (int i = 1; i < 5; i++)
                {
                    for (int j = 1; j < 8; j++)
                    {
                        list.Add(new DagPlanningTemplate(i, (Weekdag)j));
                    }
                }
                template2.DagPlanningTemplates = list.AsEnumerable();
                _dbContext.Templates.AddRange(template1, template2);

                #endregion

                #region Dagplanningen
                //dagplanningen concreet
                DateTime dt = DateTime.Today;
                var vandaag = new DagPlanning(1, dt, "balletjes in tomatensaus en friet");
                _dbContext.DagPlanningen.Add(vandaag);

                vandaag.VoegDagAtelierToeAanDagPlanningTemplate(ziekVolledigDag);
                vandaag.VoegDagAtelierToeAanDagPlanningTemplate(gelebusDag);
                vandaag.VoegDagAtelierToeAanDagPlanningTemplate(blauwebusDag);
                vandaag.VoegDagAtelierToeAanDagPlanningTemplate(beigebusDag);
                vandaag.VoegDagAtelierToeAanDagPlanningTemplate(thuisvervofVolledigeDag);
                vandaag.VoegDagAtelierToeAanDagPlanningTemplate(afwezigVolledigeDag);

                vandaag.VoegDagAtelierToeAanDagPlanningTemplate(kokenVoormiddag);
                vandaag.VoegDagAtelierToeAanDagPlanningTemplate(zwemmenNamiddag);
                vandaag.VoegDagAtelierToeAanDagPlanningTemplate(paardrijdenVoormiddag);
                vandaag.VoegDagAtelierToeAanDagPlanningTemplate(expressieVoormiddag);
                vandaag.VoegDagAtelierToeAanDagPlanningTemplate(petanqueVoormiddag);
                vandaag.VoegDagAtelierToeAanDagPlanningTemplate(toneelNamiddag);

                _dbContext.SaveChanges();

                #endregion

                #region Commentaar
                //commentaar
                Commentaar commentaarBijGebruikerLaura1 = new Commentaar("tekst", CommentaarType.AlgemeenCommentaar, new DateTime(2019, 12, 12), laura.Id);
                laura.addCommentaar(commentaarBijGebruikerLaura1);

                Commentaar commentaarBijGebruikerLaura2 = new Commentaar("tekst", CommentaarType.AlgemeenCommentaar, new DateTime(2019, 12, 12), laura.Id);
                laura.addCommentaar(commentaarBijGebruikerLaura2);

                Commentaar commentaarBijGebruikerLucas1 = new Commentaar("tekst", CommentaarType.AlgemeenCommentaar, new DateTime(2019, 12, 12), laura.Id);
                lucas.addCommentaar(commentaarBijGebruikerLucas1);

                Commentaar commentaarBijJonahZaterdag14 = new Commentaar("zaterdag14", CommentaarType.ZaterdagCommentaar, new DateTime(2019, 12, 14), jonah.Id);
                jonah.addCommentaar(commentaarBijJonahZaterdag14);

                Commentaar commentaarBijJonahZaterdag07 = new Commentaar("zaterdag07", CommentaarType.ZaterdagCommentaar, new DateTime(2019, 12, 7), jonah.Id);
                jonah.addCommentaar(commentaarBijJonahZaterdag07);

                Commentaar commentaarBijJonahZondag15 = new Commentaar("zondag15", CommentaarType.ZondagCommentaar, new DateTime(2019, 12, 15), jonah.Id);
                jonah.addCommentaar(commentaarBijJonahZondag15);

                Commentaar commentaarBijJonahZondag08 = new Commentaar("zondag08", CommentaarType.ZondagCommentaar, new DateTime(2019, 12, 8), jonah.Id);
                jonah.addCommentaar(commentaarBijJonahZondag08);

                Commentaar commentaarBijLisaZondag15 = new Commentaar("Lisa zondag 15", CommentaarType.ZondagCommentaar, new DateTime(2019, 12, 15), lisa.Id);
                lisa.addCommentaar(commentaarBijLisaZondag15);

                var commentaar = new List<Commentaar> { commentaarBijGebruikerLaura1, commentaarBijGebruikerLaura2, commentaarBijGebruikerLucas1, commentaarBijJonahZaterdag14, commentaarBijJonahZaterdag07, commentaarBijJonahZondag15, commentaarBijJonahZondag08, commentaarBijLisaZondag15 };
                _dbContext.Commentaar.AddRange(commentaar);
                _dbContext.SaveChanges();

                #endregion

                #region Opmerkingen

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

                #endregion

                _dbContext.SaveChanges();
            }
        }

        private async Task MaakGebruiker(Gebruiker gebruiker, string password)
        {
            await _userManager.CreateAsync(gebruiker, password);
            await _userManager.AddClaimAsync(gebruiker, new Claim(ClaimTypes.Role, gebruiker.Type.ToString()));

        }

        private List<Gebruiker> selecteerRandomGebruikers(List<Gebruiker> gebruikers)
        {
            Random r = new Random();
            List<Gebruiker> dummy = new List<Gebruiker>(gebruikers);
            List<Gebruiker> custom = new List<Gebruiker>();
            for (int i = 1; i <= 4; i++)
            {
                var pickedIndex = r.Next(dummy.Count);
                Gebruiker g = dummy[pickedIndex];
                custom.Add(g);
                dummy.Remove(g);
            }
            return custom;

        }
    }
}

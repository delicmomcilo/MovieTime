using System;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using Model;

namespace DAL
{
    [ExcludeFromCodeCoverage]
    public class DBInit : DropCreateDatabaseAlways<DB>
    {
        protected override void Seed(DB context)
        {
            
            var postSted = new DBPoststed
            {
                Poststed = "Larvik",
                Postnr = "3274"
            };

            DateTime fodselsdag = new DateTime(1997, 12, 25);

            var salt1 = DBKunde.LagSalt();
            var kundeAdmin = new DBKunde
            {
                Fornavn = "Admin",
                Etternavn = "Adminsen",
                Fodselsdag = fodselsdag,
                Adresse = "Brageveien 9",
                Poststed = postSted,
                Passord = DBKunde.LagHash("Admin1", salt1),
                Salt = salt1,
                Epost = "admin@movietime.no",
                Mobilnummer = 41379773,
                ErAdmin = true

            };

            var salt2 = DBKunde.LagSalt();

            var kunde = new DBKunde
            {
                Fornavn = "Bruker",
                Etternavn = "Brukersen",
                Fodselsdag = fodselsdag,
                Adresse = "Vestlisvingen 26",
                Poststed = postSted,
                Passord = DBKunde.LagHash("Bruker1", salt2),
                Salt = salt2,
                Epost = "bruker@movietime.no",
                Mobilnummer = 47442316,
                ErAdmin = false

            };

            var saltAdmin = DBKunde.LagSalt();
      


            //      ACTION

            var film1 = new Film();
            film1.Filmnavn = "DeathWish";
            film1.Filmbilde = "/Content/Images/DeathWish.jpg";
            film1.Beskrivelse = "Dr. Paul Kersey (Bruce Willis) is a surgeon who only sees the aftermath of his city's violence as it's rushed into his ER -until his wife (Elisabeth Shue) and college-age daughter (Camila Morrone) are viciously attacked in their suburban home. With the police overloaded with crimes, Paul, burning for revenge, hunts for his family's assailants to deliver justice. As the anonymous slayings of criminals grabs the media's attention, the city wonders if this deadly avenger is a guardian angel...or a grim reaper. Fury and fate collide in the intense action-thriller Death Wish.";
            
            film1.Sjanger = "Action";
            film1.Pris = 100;
            film1.Fremhevet = false;

            var dbFilm = new DBFilm();
            dbFilm.OpprettFilm(film1);

         
            var film5 = new Film();
            film5.Filmnavn = "Inquistor";
            film5.Filmbilde = "/Content/Images/Inquistor.jpg";
            film5.Sjanger = "Action";
            film5.Beskrivelse = "Den engstelige mediemagnaten Jonathan Davenport blir med kjæresten sin inn i den peruanske delen av Amazonas for å finne en tilbaketrukket kunstner som skal leve der inne. Til tross for deres filantropiske hensikter så viser oppdraget seg å sette i gang noe mørkt og ondskapsfullt i hjerte til Jonathan.";
            film5.Pris = 200;
            film5.Fremhevet = false;
            dbFilm.OpprettFilm(film5);

            var film6 = new Film();
            film6.Filmnavn = "Rampage Big Meets Bigger";
            film6.Filmbilde = "/Content/Images/Rampage.jpg";
            film6.Sjanger = "Action";
            film6.Beskrivelse = "Davis Okoye er sterkt knyttet til en intelligent gorilla som han har tatt hånd om siden fødselen. Plutselig går et vitenskapelig eksperiment galt, og det forvandler gorillaen til et gigantisk monster. Okoye må nå prøve å finne en motgift. Ikke bare for å forhindre en katastrofe, men også for å redde skapningen som en gang var vennen hans.";
            film6.Pris = 200;
            film5.Fremhevet = false;
            dbFilm.OpprettFilm(film6);

            // SCI FI

            var film222 = new Film();
            film222.Filmnavn = "Ready Player One";
            film222.Filmbilde = "/Content/Images/ReadyPlayerOne.jpg";
            film222.Beskrivelse = "When the creator of a virtual reality world called the OASIS dies, he releases a video in which he challenges all OASIS users to find his Easter Egg, which will give the finder his fortune.";
            film222.Sjanger = "Action, SciFi";
            film222.Pris = 100;
            film222.Fremhevet = false;
            dbFilm.OpprettFilm(film222);


            var film8 = new Film();
            film8.Filmnavn = "The Endless";
            film8.Filmbilde = "/Content/Images/endless.jpg";
            film8.Sjanger = "SciFi";
            film8.Beskrivelse = "Etter ti år i den vanlige verden, bestemmer brødrene Justin og Aaron seg for å besøke den domedagssekten de vokste opp i. Når de kommer tilbake til Arcadia, viser det seg at den sekten som de en gang rømte fra kanskje tross alt ikke har så vanvittig syn allikevel.";
            film8.Pris = 200;
            film8.Fremhevet = false;
            dbFilm.OpprettFilm(film8);

            var film9 = new Film();
            film9.Filmnavn = "Pacific Rim";
            film9.Filmbilde = "/Content/Images/pacific.jpg";
            film9.Sjanger = "SciFi";
            film9.Beskrivelse = "Jake Pentecost var en gang en lovende Jaegerpilot, hvis far ofret livet sitt for å sikre menneskehetens seier mot de monsterlignende 'Kaiju'.Han har hoppet av utdannelsen sin og havnet i kriminelle kretser.Men når jorden nok en gang er truet av undergang får Jake en ny sjanse til å leve opp til sin fars rykte ved å slå seg sammen med en modig ny generasjon med piloter som har vokst opp i skyggen av krigen.";
            film9.Pris = 200;
            film9.Fremhevet = false;
            dbFilm.OpprettFilm(film9);



            var film10 = new Film();
            film10.Filmnavn = "Astro";
            film10.Filmbilde = "/Content/Images/astro.jpg";
            film10.Sjanger = "SciFi";
            film10.Beskrivelse = "Alenefaren og veteranen Jack Adams lever et stille og rolig liv. Men når han får besøk av en gammel venn blir alt forandret. Jack blir kastet inn i et eksperiment som dreier seg om en fremmed som har blitt fraktet til jorden fra en nyoppdaget planet i verdensrommet.";
            film10.Pris = 200;
            film10.Fremhevet = false;
            dbFilm.OpprettFilm(film10);


            // DRAMA

            var film11 = new Film();
            film11.Filmnavn = "Gotti";
            film11.Filmbilde = "/Content/Images/Gotti.jpg";
            film11.Sjanger = "Drama";
            film11.Beskrivelse = "Filmen er basert på de virkelige hendelsene i 1976, da et Air France-fly ble kapret. Flyet var på vei fra Tel Aviv til Paris, men ble tvunget til å nødlande i Entebbe, Uganda, med passasjerene som gisler. Da begynte en av tidenes mest dristige redningsaksjoner.";
            film11.Pris = 200;
            film11.Fremhevet = false;
            dbFilm.OpprettFilm(film11);

            var film12 = new Film();
            film12.Filmnavn = "Bullet Head";
            film12.Filmbilde = "/Content/Images/bhead.jpg";
            film12.Sjanger = "Drama";
            film12.Beskrivelse = "Etter et feilslått kupp sitter tre profesjonelle kriminelle innelåst på et lager. Politiet nærmer seg, men en enda verre trussel lurer inne i lagerlokalet, og snart må de tre rømlingene gå inn i en kamp på liv og død.";
            film12.Pris = 200;
            film12.Fremhevet = false;
            dbFilm.OpprettFilm(film12);


            var film13 = new Film();
            film13.Filmnavn = "Juggernaut";
            film13.Filmbilde = "/Content/Images/juggernaut.jpg";
            film13.Sjanger = "Drama";
            film13.Beskrivelse = "Etter å ha vært lenge borte fra hjembyen sin kommer den kriminelle Saxon tilbake. Han er fullstendig overbevist om at morens død ikke var et selvmord. Og det har han til hensikt å finne ut av.";
            film13.Pris = 200;
            film13.Fremhevet = false;
            dbFilm.OpprettFilm(film13);


            var film14 = new Film();
            film14.Filmnavn = "7 Days in Entebbe";
            film14.Filmbilde = "/Content/Images/seven.jpg";
            film14.Sjanger = "Drama";
            film14.Beskrivelse = "Filmen er basert på de virkelige hendelsene i 1976, da et Air France-fly ble kapret. Flyet var på vei fra Tel Aviv til Paris, men ble tvunget til å nødlande i Entebbe, Uganda, med passasjerene som gisler. Da begynte en av tidenes mest dristige redningsaksjoner.";
            film14.Pris = 200;
            film14.Fremhevet = false;
            dbFilm.OpprettFilm(film14);


            var film15 = new Film();
            film15.Filmnavn = "What still remains";
            film15.Filmbilde = "/Content/Images/wsr.jpg";
            film15.Sjanger = "Drama";
            film15.Beskrivelse = "I en verden som for lenge siden ble ødelagt av sykdom må en ung kvinne kjempe for å overleve etter å ha mistet hele familien sin. Men når en ensom vandrer tilbyr henne et sted i samfunnet hans må hun ta en vanskelig beslutning. Er løftet om et bedre liv verdt risikoen med å stole på ham og gruppen hans?";
            film15.Pris = 200;
            film15.Fremhevet = false;
            dbFilm.OpprettFilm(film15);

            var film16 = new Film();
            film16.Filmnavn = "First reformed";
            film16.Filmbilde = "/Content/Images/reformed.jpg";
            film16.Sjanger = "Drama";
            film16.Beskrivelse = "Presten Ernst Toller er skilt, og knust av sorg etter sønnens død. En dag dukker en ny kvinne opp i forsamlingen hans, den gravide Mary. Hun ber Ernst om å snakke med mannen hennes, en radikal miljøverner som ikke vil la et barn vokse opp i en verden ødelagt av klimaendringer. Nå må Ernst hjelpe Mary samtidig som han må håndtere spøkelsene i sitt eget hode.";
            film16.Pris = 200;
            film16.Fremhevet = false;
            dbFilm.OpprettFilm(film16);

            // Klassikere

            var film17 = new Film();
            film17.Filmnavn = "Fifty Shades Freed";
            film17.Filmbilde = "/Content/Images/fsf.jpg";
            film17.Sjanger = "Klassikere";
            film17.Beskrivelse = "Presten Ernst Toller er skilt, og knust av sorg etter sønnens død. En dag dukker en ny kvinne opp i forsamlingen hans, den gravide Mary. Hun ber Ernst om å snakke med mannen hennes, en radikal miljøverner som ikke vil la et barn vokse opp i en verden ødelagt av klimaendringer. Nå må Ernst hjelpe Mary samtidig som han må håndtere spøkelsene i sitt eget hode.";
            film17.Pris = 200;
            film17.Fremhevet = false;
            dbFilm.OpprettFilm(film17);


            var film18 = new Film();
            film18.Filmnavn = "Hva vil folk si";
            film18.Filmbilde = "/Content/Images/hsfs.jpg";
            film18.Sjanger = "Klassikere";
            film18.Beskrivelse = "Presten Ernst Toller er skilt, og knust av sorg etter sønnens død. En dag dukker en ny kvinne opp i forsamlingen hans, den gravide Mary. Hun ber Ernst om å snakke med mannen hennes, en radikal miljøverner som ikke vil la et barn vokse opp i en verden ødelagt av klimaendringer. Nå må Ernst hjelpe Mary samtidig som han må håndtere spøkelsene i sitt eget hode.";
            film18.Pris = 200;
            film18.Fremhevet = false;
            dbFilm.OpprettFilm(film18);


            var film19 = new Film();
            film19.Filmnavn = "Pulp Fiction";
            film19.Filmbilde = "/Content/Images/pulp.jpg";
            film19.Sjanger = "Klassikere";
            film19.Beskrivelse = "Presten Ernst Toller er skilt, og knust av sorg etter sønnens død. En dag dukker en ny kvinne opp i forsamlingen hans, den gravide Mary. Hun ber Ernst om å snakke med mannen hennes, en radikal miljøverner som ikke vil la et barn vokse opp i en verden ødelagt av klimaendringer. Nå må Ernst hjelpe Mary samtidig som han må håndtere spøkelsene i sitt eget hode.";
            film19.Pris = 200;
            film19.Fremhevet = false;
            dbFilm.OpprettFilm(film19);


            var film20 = new Film();
            film20.Filmnavn = "Goodfellas";
            film20.Filmbilde = "/Content/Images/goodfellas.jpg";
            film20.Sjanger = "Klassikere";
            film20.Beskrivelse = "Presten Ernst Toller er skilt, og knust av sorg etter sønnens død. En dag dukker en ny kvinne opp i forsamlingen hans, den gravide Mary. Hun ber Ernst om å snakke med mannen hennes, en radikal miljøverner som ikke vil la et barn vokse opp i en verden ødelagt av klimaendringer. Nå må Ernst hjelpe Mary samtidig som han må håndtere spøkelsene i sitt eget hode.";
            film20.Pris = 200;
            film20.Fremhevet = false;
            dbFilm.OpprettFilm(film20);


            var film21 = new Film();
            film21.Filmnavn = "The Godfather";
            film21.Filmbilde = "/Content/Images/thegodfather.jpg";
            film21.Sjanger = "Klassikere";
            film21.Beskrivelse = "Presten Ernst Toller er skilt, og knust av sorg etter sønnens død. En dag dukker en ny kvinne opp i forsamlingen hans, den gravide Mary. Hun ber Ernst om å snakke med mannen hennes, en radikal miljøverner som ikke vil la et barn vokse opp i en verden ødelagt av klimaendringer. Nå må Ernst hjelpe Mary samtidig som han må håndtere spøkelsene i sitt eget hode.";
            film21.Pris = 200;
            film21.Fremhevet = false;
            dbFilm.OpprettFilm(film21);


            var film22 = new Film();
            film22.Filmnavn = "Schindlers List";
            film22.Filmbilde = "/Content/Images/schindlerslist.jpg";
            film22.Sjanger = "Klassikere";
            film22.Beskrivelse = "Presten Ernst Toller er skilt, og knust av sorg etter sønnens død. En dag dukker en ny kvinne opp i forsamlingen hans, den gravide Mary. Hun ber Ernst om å snakke med mannen hennes, en radikal miljøverner som ikke vil la et barn vokse opp i en verden ødelagt av klimaendringer. Nå må Ernst hjelpe Mary samtidig som han må håndtere spøkelsene i sitt eget hode.";
            film22.Pris = 200;
            film22.Fremhevet = false;
            dbFilm.OpprettFilm(film22);

            var film23 = new Film();
            film23.Filmnavn = "Fight Club";
            film23.Filmbilde = "/Content/Images/fightclub.jpg";
            film23.Sjanger = "Klassikere";
            film23.Beskrivelse = "Presten Ernst Toller er skilt, og knust av sorg etter sønnens død. En dag dukker en ny kvinne opp i forsamlingen hans, den gravide Mary. Hun ber Ernst om å snakke med mannen hennes, en radikal miljøverner som ikke vil la et barn vokse opp i en verden ødelagt av klimaendringer. Nå må Ernst hjelpe Mary samtidig som han må håndtere spøkelsene i sitt eget hode.";
            film23.Pris = 200;
            film23.Fremhevet = false;
            dbFilm.OpprettFilm(film23);


            var film24 = new Film();
            film24.Filmnavn = "Die Hard";
            film24.Filmbilde = "/Content/Images/diehard.jpg";
            film24.Sjanger = "Klassikere";
            film24.Beskrivelse = "Presten Ernst Toller er skilt, og knust av sorg etter sønnens død. En dag dukker en ny kvinne opp i forsamlingen hans, den gravide Mary. Hun ber Ernst om å snakke med mannen hennes, en radikal miljøverner som ikke vil la et barn vokse opp i en verden ødelagt av klimaendringer. Nå må Ernst hjelpe Mary samtidig som han må håndtere spøkelsene i sitt eget hode.";
            film24.Pris = 200;
            film24.Fremhevet = false;
            dbFilm.OpprettFilm(film24);
            

            var film25 = new Film();
            film25.Filmnavn = "Deadpool 2";
            film25.FremhevetBilde = "/Content/Images/dada.jpg";
            film25.Filmbilde = "/Content/Images/deadpool.jpg";
            film25.Beskrivelse = "Presten Ernst Toller er skilt, og knust av sorg etter sønnens død. En dag dukker en ny kvinne opp i forsamlingen hans, den gravide Mary. Hun ber Ernst om å snakke med mannen hennes, en radikal miljøverner som ikke vil la et barn vokse opp i en verden ødelagt av klimaendringer. Nå må Ernst hjelpe Mary samtidig som han må håndtere spøkelsene i sitt eget hode.";
            film25.Sjanger = "Action, Komedie";
            film25.Pris = 200;
            film25.Fremhevet = true;
            dbFilm.OpprettFilm(film25);

            var film26 = new Film();
            film26.Filmnavn = "Life of the party";
            film26.FremhevetBilde = "/Content/Images/lifepart.jpg";
            film26.Filmbilde = "/Content/Images/lifeparty.jpg";
            film26.Beskrivelse = "Når hjemmeværende Deanna plutselig blir forlatt av mannen sin, bestemmer hun seg for å begynne på college igjen. Hun havner i samme klasse som datteren sin, som ikke er ensidig entusiastisk for ideen. Deanna - som nå kaller seg Dee Rock - hengir seg til livet på campus med alt det innebærer. Frihet, moro, gutter og å finne seg selv.";
            film26.Sjanger = "Action, Komedie";
            film26.Pris = 200;
            film26.Fremhevet = true;
            dbFilm.OpprettFilm(film26);

            var film27 = new Film();
            film27.Filmnavn = "Ocean 8";
            film27.FremhevetBilde = "/Content/Images/8fr.jpg";
            film27.Filmbilde = "/Content/Images/8.jpg";
            film27.Beskrivelse = "Debbie Ocean, søsteren til Danny, slipper ut av fengsel, men det tar ikke lang tid før hun planlegger neste kupp. Hun samler en gruppe med de beste av de beste innen deres område. Målet er diamanter til 150 millioner dollar, som kommer til å henge rundt halsen på den verdensberømte skuespillerinnen Daphne Kluger, som vil være midtpunktet på årets arrangement, the Met Gala. Planen er idiotsikker, men alt må gå feilfritt...";
            film27.Sjanger = "Action, Komedie";
            film27.Pris = 200;
            film27.Fremhevet = true;
            dbFilm.OpprettFilm(film27);

            context.Kunder.Add(kunde);
            context.Kunder.Add(kundeAdmin);
            base.Seed(context);

        }
    }
}
using AutoMapper;
using Model;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;


namespace DAL
{
    [ExcludeFromCodeCoverage]
    public class DBKunde : IDBKunde
    {
        public int ID { get; set; }
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public DateTime Fodselsdag { get; set; }
        public string Adresse { get; set; }
        public int Mobilnummer { get; set; }
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Epost { get; set; }
        public bool ErAdmin { get; set; }
        public byte[] Passord { get; set; }
        public byte[] Salt { get; set; }
        public virtual DBPoststed Poststed { get; set; }
        public virtual List<DBOrdre> Ordrer { get; set; }


        public bool Registrer(KundeRegistreringViewModel innKunde)
        {
            try
            {
                using (var db = new DB())
                {
                        byte[] salt = LagSalt();
                        byte[] hash = LagHash(innKunde.Passord, salt);

                        var SjekkEpost = Finn(innKunde.Epost);

                        if (SjekkEpost == null)
                        {
                            var nyKunde = new DBKunde()
                            {
                                Fornavn = innKunde.Fornavn,
                                Etternavn = innKunde.Etternavn,
                                Passord = LagHash(innKunde.Passord, salt),
                                Adresse = innKunde.Adresse,
                                Fodselsdag = innKunde.Fodselsdag,
                                Epost = innKunde.Epost,
                                Mobilnummer = innKunde.Mobilnummer,
                                ErAdmin = false,
                                Salt = salt

                            };

                            var postSted = db.Poststeder.Find(innKunde.Postnummer);

                            if (postSted == null)
                            {
                                var poststed = new DBPoststed();
                                poststed.Postnr = innKunde.Postnummer;
                                poststed.Poststed = innKunde.Poststed;

                                nyKunde.Poststed = poststed;

                            }
                            else
                            {
                                nyKunde.Poststed = postSted;

                            }
                            var endring = new DBEndring()
                            {
                                Tidspunkt = DateTime.Now,
                                EndringOperasjon = "Ny kunde: ",
                                endring = $"{nyKunde.Fornavn} {nyKunde.Etternavn}, {nyKunde.Adresse}, {nyKunde.Epost}, {nyKunde.Poststed.Poststed}, {nyKunde.Poststed.Postnr}"

                            };
                            db.Endringer.Add(endring);
                            db.Kunder.Add(nyKunde);
                            db.SaveChanges();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                }
            }
            catch (Exception e)
            {
                DBLog.ErrorToFile("Feil oppstått når kunden skulle registreres", "DBKunde:Registrer", e);
                return false;
            }
        }

        public List<KundeEndreViewModel> HentAlle(int MedRettighet)
        {
            try {
                using (var db = new DB())
                {
                    var kunder = db.Kunder.ToList();
                    var domeneKunder = new List<KundeEndreViewModel>();
                    if(kunder != null)
                    {
                        foreach (var kunde in kunder)
                        {
                            if(MedRettighet == 0 && kunde.ErAdmin == false)
                            {
                                domeneKunder.Add(Mapper.Map<KundeEndreViewModel>(kunde));
                            }
                            else if (MedRettighet == 1 && kunde.ErAdmin == true)
                            {
                                domeneKunder.Add(Mapper.Map<KundeEndreViewModel>(kunde));
                            }
                        }
                    }
                    return domeneKunder;
                }
            }
            catch (Exception e)
            {
                DBLog.ErrorToFile("Feil oppstått når HentAlle-metoden prøver å hente alle brukere med rettighet", "DBKunde:HentAlle", e);
                return null;
            }
        }


        public Kunde Finn(string epost)
        {
            using (var db = new DB())
            {
                try {
                    var kunde = (from k in db.Kunder
                                 where k.Epost == epost
                                 select k).FirstOrDefault();

                    if (kunde != null)
                    {
                        return new Kunde
                        {
                            ID = kunde.ID,
                            Fornavn = kunde.Fornavn,
                            Etternavn = kunde.Etternavn,
                            Epost = kunde.Epost,
                            ErAdmin = kunde.ErAdmin
                        };
                    }
                    return null;
                }
                catch (Exception e)
                {
                    DBLog.ErrorToFile("Feil oppstått når Finn-metoden skulle finne kunde", "DBKunde:Finn", e);
                    return null;
                }
            }
        }

        public Kunde FinnKundeID(int id)
        {
            try
            {
                using (var db = new DB())
                {              
                    var kunde = db.Kunder.FirstOrDefault(k => k.ID == id);
                    if (kunde != null)
                    {
                        var domeneKunde = new Kunde();
                        domeneKunde.ID = kunde.ID;
                        domeneKunde.Fornavn = kunde.Fornavn;
                        domeneKunde.Etternavn = kunde.Etternavn;
                        domeneKunde.Fodselsdag = kunde.Fodselsdag;
                        domeneKunde.Mobilnummer = kunde.Mobilnummer;
                        domeneKunde.Epost = kunde.Epost;
                        domeneKunde.Poststed = kunde.Poststed.Poststed;
                        domeneKunde.Adresse = kunde.Adresse;
                        domeneKunde.Postnummer = kunde.Poststed.Postnr;

                        return domeneKunde;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                DBLog.ErrorToFile("Feil oppstått når FinnKundeID-metoden prøve å finne ID-en", "DBKunde:FinnKundeId", ex);
                return null;

            }
        }

        public bool Endre(KundeEndreViewModel endretKunde, int id)
        {
            try
            {
                using (var db = new DB())
                {

                    var kunde = db.Kunder.FirstOrDefault(k => k.ID == id);

                    if (kunde != null)
                    {
                        var postNr = db.Poststeder.Find(endretKunde.Postnummer);
                        kunde.Fornavn = endretKunde.Fornavn;
                        kunde.Etternavn = endretKunde.Etternavn;
                        kunde.Mobilnummer = endretKunde.Mobilnummer;
                        kunde.Fodselsdag = endretKunde.Fodselsdag;
                        kunde.Adresse = endretKunde.Adresse;

                        if (endretKunde.Epost != kunde.Epost)
                        {
                            var sjekkEpost = Finn(endretKunde.Epost);
                            if (sjekkEpost == null)
                            {
                                kunde.Epost = endretKunde.Epost;
                            }
                            else
                            {
                                return false;
                            }
                        }

                        if (postNr == null)
                        {
                            var poststed = new DBPoststed();
                            poststed.Postnr = endretKunde.Postnummer;
                            poststed.Poststed = endretKunde.Poststed;
                            kunde.Poststed = poststed;

                        }
                        else
                        {
                            kunde.Poststed = postNr;
                        }
                        var endring = new DBEndring()
                        {
                            Tidspunkt = DateTime.Now,
                            EndringOperasjon = "Endret Kunde:",
                            endring = $"{kunde.Fornavn} {kunde.Etternavn}, {kunde.Adresse}, {kunde.Epost}, {kunde.Poststed.Poststed}, {kunde.Poststed.Postnr}"

                        };
                        db.Endringer.Add(endring);
                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }}catch (Exception ex)
                {
                    DBLog.ErrorToFile("Feil oppstått når kunde skulle endres", "DBKunde:Endre", ex);
                    return false;

                }
        }

        public bool EndrePassord(PassordViewModel endretPassord, int kID)
        {
            try
            {
                using(var db = new DB())
                {
                    var kunde = db.Kunder.FirstOrDefault(k => k.ID == kID);
                    if(kunde != null)
                    {
                        byte[] passordForTest = LagHash(endretPassord.GammeltPassord, kunde.Salt);
                        bool riktigBruker = kunde.Passord.SequenceEqual(passordForTest);

                        if(riktigBruker)
                        {
                            if(endretPassord.NyttPassord == endretPassord.NyttPassordBekreft)
                            {
                                var salt = LagSalt();
                                var nyttPassord = LagHash(endretPassord.NyttPassord, salt);
                                kunde.Passord = nyttPassord;
                                kunde.Salt = salt;
                                var endring = new DBEndring()
                                {
                                    Tidspunkt = DateTime.Now,
                                    EndringOperasjon = "Endret Kunde Passord",
                                    endring = $"{kunde.Fornavn} {kunde.Etternavn}, {kunde.Adresse}, {kunde.Epost}, {kunde.Poststed.Poststed}, {kunde.Poststed.Postnr}"
                                };                              
                                db.Endringer.Add(endring);
                                db.SaveChanges();
                                return true;

                            }

                        }

                    }
                    return false;       
                }
            }catch(Exception ex)
            {
                DBLog.ErrorToFile("Feil oppstått når passordet skulle endres på en bruker", "DBKunde:EndrePassord", ex);
                return false;

            }
        }

        public static byte[] LagHash(string innPassord, byte[] innSalt)
        {
            const int keyLength = 24;
            var pbkdf2 = new Rfc2898DeriveBytes(innPassord, innSalt, 1000);
            return pbkdf2.GetBytes(keyLength);
        }

        public static byte[] LagSalt()
        {
            var csprng = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csprng.GetBytes(salt);
            return salt;
        }

        public bool Login(KundeLoginViewModel innKunde)
        {
            using (var db = new DB())
            {
                DBKunde funnetKunde = db.Kunder.FirstOrDefault(b => b.Epost == innKunde.Epost);
                if (funnetKunde != null)
                {
                    byte[] passordForTest = LagHash(innKunde.Passord, funnetKunde.Salt);
                    bool riktigBruker = funnetKunde.Passord.SequenceEqual(passordForTest);
                    return riktigBruker;
                }
                else
                {
                    return false;
                }
            }
        }

        public KundeEndreViewModel HentKunde(int kID)
        {
            try
            {
                using (var db = new DB())
                {
                    var kunde = db.Kunder.Find(kID);
                    if(kunde != null)
                    {
                        return Mapper.Map<KundeEndreViewModel>(kunde);
                    }
                    return null;
                }

            }catch(Exception ex)
            {
                DBLog.ErrorToFile("Feil oppstått når en kunde skulle hentes", "DBKunde:HentKunde", ex);
                return null;

            }
        }

        public bool SlettKunde(int kID)
        {
            try
            {
                using (var db = new DB())
                {
                    var kunde = db.Kunder.Find(kID);
                    if(kunde != null)
                    {
                        db.Kunder.Remove(kunde);
                        db.SaveChanges();
                        return true;
                    }
                }
                return false;

            }catch (Exception ex)
            {
                DBLog.ErrorToFile("Feil oppstått når en kunde skulle slettes", "DBKunde:SlettKunde", ex);
                return false;
            }
        }

        public bool TildelRettigheter(int kID, int rettighet)
        {
            try
            {
                using (var db = new DB())
                {
                    var kunde = db.Kunder.Find(kID);

                    if(kunde != null)
                    {
                        if(rettighet == 1)
                        {
                            kunde.ErAdmin = true;
                            db.SaveChanges();
                            return true;
                        }
                        else
                        {
                            kunde.ErAdmin = false;
                            db.SaveChanges();
                            return true;
                        }
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                DBLog.ErrorToFile("Feil oppstått når en bruker ble tildelt rettigheter", "DBKunde:TildelRettigheter", ex);
                return false;
            }
        }
    }
  
}
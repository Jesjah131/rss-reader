using System.Collections.Generic;
using System.Linq;
using Data;

namespace Logic
{
    public class XmlHelper
    {
        public List<string> LaddaFeeden(string namn)
        {
            var data = new Datahantering();
            return data.load(namn).ToList();
        }

        public IEnumerable<string> laddaSpecific(string namn, string titel)
        {
            var data = new Datahantering();
            return data.loadSpecific(namn, titel);
        }

        public IEnumerable<string> laddaSpelaUpp(string namn, string titel)
        {
            var data = new Datahantering();
            return data.spelaUppValdPodcast(namn, titel);
        }

        public void laddaUppspelad(string namn, string titel)
        {
            var data = new Datahantering();
            data.uppdateraSpelad(namn, titel);
        }

        public List<string> listaAllaFeeds()
        {
            var data = new Datahantering();
            return data.GetAllFeeds();
        } 

        public void skapaKategoriFilen()
        {
            var data = new Datahantering();
            data.skapaFilenKategori();
        }

        public void InmatningKategoriFilen(string input)
        {
            var data = new Datahantering();
            data.InmatningKategoriFilen(input);
        }

        //public List<char> PopuleraComboboxKategori()
        //{
        //    var data = new LoadFeed();
        //    return data.PopuleraComboboxKategori();
        //}

        public void SkapaNamnKategoriFilen()
        {
            var data = new Datahantering();
            data.skapaNamnKategoriFilen();
        }

        public void InmatningNamnKategoriFilen(string valdKategori, string namn)
        {
            var data = new Datahantering();
            data.InmatningNamnKategoriFilen(valdKategori, namn);
        }

        public IEnumerable<string> ListaUppspeladEllerEj(string namn, string titel)
        {
            var data = new Datahantering();
            return data.ListaUppspeladEllerEj(namn, titel);
        }

        public void TaBortKategoriItem(string namn)
        {
            var data = new Datahantering();
            data.DeleteNameCategory(namn);
        }

        public void TaBortKategori(string kategori)
        {
            var data = new Datahantering();
            data.taBortKategori(kategori);
        }

        public void BytNamnPaKategori(string kategori, string namn)
        {
            var data = new Datahantering();
            data.BytNamnPaKategori(kategori, namn);
        }
    }


}

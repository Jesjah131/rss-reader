using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Logic;
using Logic.Entities;
using Logic.Service.Validation;

namespace CSharpProject.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var ladda = new XmlHelper();
            ladda.skapaKategoriFilen();
            ladda.SkapaNamnKategoriFilen();
            PopuleraComboboxKategori();        
            populeraAllaFeeds();
        }

        private void btnAddFeed_Click(object sender, RoutedEventArgs e)
        {
            if (Validator.CheckIfNameIsValid(tbNameFeed.Text) && (Validator.CheckUrlContainsRssOrXml(tbUrlFeed.Text)))
            {
                var feed = new Feed();
                var helper = new FeedHelper();
                feed.url = tbUrlFeed.Text;
                LvListaTitlar.Items.Clear();

                helper.addFeed(feed, tbNameFeed.Text);

                lbFeed.Items.Add(tbNameFeed.Text);
                MataInNamnKategori();
            }
        }

        private void lbFeed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbFeed.SelectedItem != null)
            {
                LvListaTitlar.Items.Clear();
                LvListaSpecific.Items.Clear();
                var text = lbFeed.SelectedItem;

                var ladda = new XmlHelper();
                var laddaFeeden = ladda.LaddaFeeden(text.ToString());

                foreach (var l in laddaFeeden)
                {
                    LvListaTitlar.Items.Add(l);
                }
            }
        }

        private void LvListaTitlar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {         
            LvUppspelad.Items.Clear();
            LvListaSpecific.Items.Clear();
            if (lbFeed.SelectedItem == null || LvListaTitlar.SelectedItem == null) return;
            var text1 = lbFeed.SelectedItem.ToString();
            var text = LvListaTitlar.SelectedItem.ToString();

            var ladda = new XmlHelper();
            var hej = ladda.laddaSpecific(text1, text);
            var hej2 = ladda.ListaUppspeladEllerEj(text1, text);

            foreach (var h in hej)
            {
                if (h == "")
                {
                    LvListaSpecific.Items.Add("Finns ingen info om denna podcast");
                }
                LvListaSpecific.Items.Add(h);
            }

            foreach (var h2 in hej2)
            {
                LvUppspelad.Items.Add(h2);
            }
        }

        private void btnPlaySelected_Click(object sender, RoutedEventArgs e)
        {
            if (LvListaTitlar.SelectedItem != null)
            {
                var text1 = lbFeed.SelectedItem.ToString();
                var text = LvListaTitlar.SelectedItem.ToString();

                var ladda = new XmlHelper();
                var hej = ladda.laddaSpelaUpp(text1, text);
                foreach (var h in hej)
                {
                    Process.Start("wmplayer.exe", h);
                }
                ladda.laddaUppspelad(text1, text);
            }
        }

        private void labelNyKategori_Clicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            tbNyKategori.Visibility = Visibility.Visible;
            btnAddCategory.Visibility = Visibility.Visible;
        }

        private void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            if (Validator.CheckIfCategoryAlreadyExists(tbNyKategori.Text, cbCategoryFeed.Items.ToString()) && Validator.CheckIfNameIsValid(tbNyKategori.Text))
            {
                if (tbNyKategori.Text != null)
                {
                    cbCategoryFeed.Items.Add(tbNyKategori.Text);
                }

                var ladda = new XmlHelper();
                ladda.InmatningKategoriFilen(tbNyKategori.Text);
                PopuleraComboboxKategori();
            }
        }

        private void PopuleraComboboxKategori()
        {
            cbCategoryFeed.Items.Clear();
            cbNamnKategori.Items.Clear();
            cbNyKategori.Items.Clear();
            cbBytNamnKategori.Items.Clear();
            XDocument xDoc = XDocument.Load(@"\Kategorimap\kategorier.xml");

            var kategori = xDoc.Descendants("kategori");

            foreach (var k in kategori)
            {
                cbCategoryFeed.Items.Add(k.Value);
                cbNamnKategori.Items.Add(k.Value);
                cbNyKategori.Items.Add(k.Value);
                cbBytNamnKategori.Items.Add(k.Value);
            }
            cbNyKategori.SelectedIndex = 0;
            cbCategoryFeed.SelectedIndex = 0;
            cbBytNamnKategori.SelectedIndex = 0;
        }

        private void MataInNamnKategori()
        {
            var valdKategori = cbCategoryFeed.SelectedItem.ToString();
            var namn = tbNameFeed.Text;

            var ladda = new XmlHelper();
            ladda.InmatningNamnKategoriFilen(valdKategori, namn);
        }

        private void cbNamnKategori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbNamnKategori.SelectedItem != null)
            {
                LvListaTitlar.Items.Clear();
                LvListaSpecific.Items.Clear();
                LvUppspelad.Items.Clear();
                var valdKategori = cbNamnKategori.SelectedItem;

                XDocument xdoccet =
                    XDocument.Load(
                        @"\Kategorimap\namnKategori.xml");

                var k = xdoccet.Descendants("Item");

                lbFeed.Items.Clear();

                foreach (var ke in k)
                {
                    if (ke.Element("Kategori").Value == valdKategori.ToString())
                    {
                        lbFeed.Items.Add(ke.Element("Namn").Value);
                    }
                }
            }
        }

        private void btnUpdatePodcast_Click(object sender, RoutedEventArgs e)
        {
            if (Validator.CheckUrlContainsRssOrXml(tbNyUrl.Text))
            {
                LvListaTitlar.Items.Clear();
                var titel = lbFeed.SelectedItem.ToString();
                var kategori = cbNyKategori.SelectedItem.ToString();
                var feed = new Feed();
                var load = new XmlHelper();
                var fhelper = new FeedHelper();
                fhelper.deleteFeed(titel);
                feed.url = tbNyUrl.Text;
                load.TaBortKategoriItem(titel);
                load.InmatningNamnKategoriFilen(kategori, titel);

                fhelper.addFeed(feed, titel);
                foreach (var items in feed.Items)
                {
                    LvListaTitlar.Items.Add(items.Title);
                }
            }
        }

        private void btnDeleteFeed_Click(object sender, RoutedEventArgs e)
        {
            if (lbFeed.SelectedItem != null)
            {
                var valdfeed = lbFeed.SelectedItem.ToString();
                int selected = lbFeed.SelectedIndex;
                lbFeed.Items.RemoveAt(selected);
                var ladda = new XmlHelper();
                ladda.TaBortKategoriItem(valdfeed);
                var fhelper = new FeedHelper();
                fhelper.deleteFeed(valdfeed);
            }
        }

        private void btnTaBortKategori_Click(object sender, RoutedEventArgs e)
        {
            var selectedindex = cbCategoryFeed.SelectedIndex;
            var kategori = cbCategoryFeed.SelectedItem.ToString();
            Console.WriteLine(kategori);
            var ladda = new XmlHelper();
            ladda.TaBortKategori(kategori);
            cbCategoryFeed.Items.RemoveAt(selectedindex);
            PopuleraComboboxKategori();
        }

        private void btnBytNamnKategori_Click(object sender, RoutedEventArgs e)
        {
            var kategori = cbBytNamnKategori.SelectedItem.ToString();
            var namn = tbNyttKategoriNamn.Text;
            var ladda = new XmlHelper();
            ladda.BytNamnPaKategori(kategori, namn);
        }

        private void populeraAllaFeeds()
        {
            var xHelper = new XmlHelper();
            var lista = xHelper.listaAllaFeeds();
            foreach (var k in lista)
            {
                lbFeed.Items.Add(k);
            }
        }
    }
}

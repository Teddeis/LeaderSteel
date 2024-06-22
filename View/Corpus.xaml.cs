using LeaderSteel.ViewModel;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeaderSteel.View
{
    /// <summary>
    /// Логика взаимодействия для Corpus.xaml
    /// </summary>
    /// 
    public partial class Corpus : Page
    {
        DB db = new DB();
        ViewModelCombo v = new ViewModelCombo();
        DBCorpus d = new DBCorpus();
        Search s = new Search();
        public Corpus()
        {
            InitializeComponent();
            v.STransport(Transport);
            v.SMaterial(Material);
            v.SWarehouse2(Warehouse);
            d.LoadFullRowsFromDatabaseCorpus(corpuslist);
        }

        public void S1()
        {
            db.con.Open();
            string query = $"SELECT id FROM Transport where Transport = '{Transplabel.Content}'";

            using (var command = new SqlCommand(query, db.con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idTransport.Text = reader["id"].ToString();
                    }
                }
            }
            db.con.Close();
        }

        public void S2()
        {
            db.con.Open();
            string query = $"SELECT id FROM Material where Type = '{Materlabel.Content}'";

            using (var command = new SqlCommand(query, db.con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idMaterial.Text = reader["id"].ToString();
                    }
                }
            }
            db.con.Close();
        }

        public void S3()
        {
            db.con.Open();
            string query = $"SELECT id FROM Warehouse where NumberWarehouse = '{Warlabel.Content}'";

            using (var command = new SqlCommand(query, db.con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idWarehouse.Text = reader["id"].ToString();
                    }
                }
            }
            db.con.Close();
        }

        private void PP()
        {
            string query = $"insert into Corpus(idWarehouse,idTransport,idMaterial,BoxNumber,Quantity,LengthCorpus,WidthCorpus,HeightCorpus,Weight) values ('{idWarehouse.Text}','{idTransport.Text}','{idMaterial.Text}','{BoxNumber.Text}','{Quantity.Text}','{LengthCorpus.Text}','{WidthCorpus.Text}','{HeightCorpus.Text}','{Weight.Text}')";
            SqlCommand command = new SqlCommand(query, db.con);
            try
            {
                db.con.Open();
                int rowsAffected = command.ExecuteNonQuery();
                System.Windows.MessageBox.Show("Ящик добавлен!", "Успешно", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
                db.con.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(idMaterial.Text) || 
                string.IsNullOrWhiteSpace(idTransport.Text) ||
                string.IsNullOrWhiteSpace(idWarehouse.Text) || 
                string.IsNullOrWhiteSpace(Quantity.Text) || 
                string.IsNullOrWhiteSpace(BoxNumber.Text) ||
                string.IsNullOrWhiteSpace(Weight.Text) || 
                string.IsNullOrWhiteSpace(LengthCorpus.Text) ||
                string.IsNullOrWhiteSpace(WidthCorpus.Text) || 
                string.IsNullOrWhiteSpace(HeightCorpus.Text))
            {
                System.Windows.MessageBox.Show("Не заполнены обязательные поля!", "Ошибка", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Exclamation);
            }
            else
            {
                corpuslist.Items.Clear();
                PP();
                d.LoadFullRowsFromDatabaseCorpus(corpuslist);
            }
        }

        private void Transport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Transplabel.Content = Transport.SelectedItem.ToString();
            S1();
        }

        private void Warehouse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Warlabel.Content = Warehouse.SelectedItem.ToString();
            S3();
        }

        private void Material_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Materlabel.Content = Material.SelectedItem.ToString();
            S2();
        }

        private void QWLWH_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789 ".IndexOf(e.Text) < 0;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BoxNumber.Text = string.Empty;
            Quantity.Text = string.Empty;
            LengthCorpus.Text = string.Empty;
            WidthCorpus.Text= string.Empty;
            HeightCorpus.Text = string.Empty;
            Weight.Text = string.Empty;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(deletes.Text))
            {
                System.Windows.MessageBox.Show("Не заполнены обязательные поля!", "Ошибка", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Exclamation);
            }
            else
            {
                corpuslist.Items.Clear();
                d.delete(deletes.Text);
                d.LoadFullRowsFromDatabaseCorpus(corpuslist);
            }
        }

        private void corpuslist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = corpuslist.SelectedItem;

            if (selectedItem != null)
            {
                var selectedItemText = selectedItem.ToString();

                var lines = selectedItemText.Split();

                deletes.Text = lines[1];
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            corpuslist.Items.Clear();
            s.SearchAll(corpuslist, searchs);
        }

        public void DD()
        {
            int lengthCorpus = int.Parse(LengthCorpus.Text);
            int widthCorpus = int.Parse(WidthCorpus.Text);
            int heightCorpus = int.Parse(HeightCorpus.Text);


            SqlCommand command = new SqlCommand("SELECT * FROM Transport WHERE MaxLengthCorpus >= @maxLength AND MaxWidthCorpus >= @maxWidth AND MaxHeightCorpus >= @maxHeight", db.con);
            command.Parameters.AddWithValue("@maxLength", lengthCorpus);
            command.Parameters.AddWithValue("@maxWidth", widthCorpus);
            command.Parameters.AddWithValue("@maxHeight", heightCorpus);

            db.con.Open();
            SqlDataReader reader = command.ExecuteReader();

                Compatibility.Items.Clear();

            while (reader.Read())
            {
                Compatibility.Items.Add(reader["Transport"]);
            }

            reader.Close();
            db.con.Close();
        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LengthCorpus.Text) || string.IsNullOrWhiteSpace(WidthCorpus.Text) || string.IsNullOrWhiteSpace(HeightCorpus.Text))
            {
                System.Windows.MessageBox.Show("Заполните Длину/Ширину/Высоту", "Ошибка", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Exclamation);
            }
            else
            {
                DD();
            }
        }

        private void Compatibility_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = Compatibility.SelectedItem;

            if (selectedItem != null)
            {
                var selectedItemText = selectedItem.ToString();

                var lines = selectedItemText;

                Transport.Text = lines;
            }
        }
    }
}

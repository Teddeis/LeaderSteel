using LeaderSteel.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace LeaderSteel.View
{
    /// <summary>
    /// Логика взаимодействия для Material.xaml
    /// </summary>
    public partial class Material : Page
    {
        DB db = new DB();
        ViewModelCombo v = new ViewModelCombo();
        public Material()
        {
            InitializeComponent();
            v.SMaterial(Combo);
            Combo.SelectedIndex = 0;
        }

        public void S2()
        {
            db.con.Open();
            string query = $"SELECT Type, Characteristic, Quantity, Icons FROM Material WHERE Type = '{OneT1.Text}'";

            using (var command = new SqlCommand(query, db.con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        OneT2.Text = reader["Characteristic"].ToString();
                        OneT3.Text = reader["Quantity"].ToString() + " шт.";
                        string imagePath = reader["Icons"].ToString();
                        One.Source = new BitmapImage(new Uri($@"\View\Res\Icons\{imagePath}", UriKind.Relative));
                    }
                }
            }
            db.con.Close();
        }

            private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OneT1.Text = Combo.SelectedItem.ToString();
            S2();
        }
    }
}

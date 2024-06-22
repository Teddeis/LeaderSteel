using LeaderSteel.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
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
using System.Windows.Shapes;

namespace LeaderSteel.View
{
    /// <summary>
    /// Логика взаимодействия для Transport.xaml
    /// </summary>
    public partial class Transport : Page
    {
        DB db = new DB();
        ViewModelCombo v = new ViewModelCombo();
        public Transport()
        {
            InitializeComponent();
            v.STransport(Combo);

            Combo.SelectedIndex = 0;
        }

        public void S2()
        {
            db.con.Open();
            string query = $"SELECT Transport, LengthCorpus,MaxLengthCorpus,WidthCorpus,MaxWidthCorpus,HeightCorpus,MaxHeightCorpus, Images FROM Transport WHERE Transport = '{OneT1.Text}'";

            using (var command = new SqlCommand(query, db.con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        OneT1.Text = reader["Transport"].ToString();
                        OneT2.Text = $"Длина: {reader["LengthCorpus"].ToString()}-{reader["MaxLengthCorpus"].ToString()} мм.\nШирина: {reader["WidthCorpus"].ToString()}-{reader["MaxWidthCorpus"].ToString()} мм.\nВысота: {reader["HeightCorpus"].ToString()}-{reader["MaxHeightCorpus"].ToString()} мм.";
                        string imagePath = reader["Images"].ToString();
                        One.Source = new BitmapImage(new Uri($@"\View\Res\Transport\{imagePath}", UriKind.Relative));
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

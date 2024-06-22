using LeaderSteel.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace LeaderSteel.View
{
    /// <summary>
    /// Логика взаимодействия для Warehouse.xaml
    /// </summary>
    public partial class Warehouse : Page
    {
        DB db = new DB();
        ViewModelCombo v = new ViewModelCombo();
        public Warehouse()
        {
            InitializeComponent();
            v.SWarehouse(comboBox);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string address = comboBox.SelectedValue.ToString();
            string url = $"https://yandex.ru/maps/?display-text=&ll=46.098134%2C51.415901&mode=search&sll=46.100005%2C51.411073&sspn=0.123253%2C0.043853&text={address}";
            webView.Source = new Uri(url);
        }
    }
}

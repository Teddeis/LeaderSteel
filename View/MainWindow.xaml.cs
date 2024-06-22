using LeaderSteel.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CorpusVid.IsEnabled = false;
            AccountVid.IsEnabled = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();


        private void L_Click(object sender, RoutedEventArgs e)
        {
            AuthRegWindow authRegWindow = new AuthRegWindow();
            L.Content = Account.Logins;

            if (L.Content.ToString() == "Войти")
            {
                CorpusVid.IsEnabled = false;
                AccountVid.IsEnabled = false;
            }
            else
            {
                CorpusVid.IsEnabled = true;
            }
            if(L.Content.ToString() == "admin")
            {
                AccountVid.IsEnabled = true;
            }
            else
            {
                AccountVid.IsEnabled = false;
            }
        }
    }
}

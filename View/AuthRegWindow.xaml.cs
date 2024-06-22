using LeaderSteel.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
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
using System.Windows.Shapes;


namespace LeaderSteel.View
{
    /// <summary>
    /// Логика взаимодействия для AuthRegWindow.xaml
    /// </summary>
    ///
    public partial class AuthRegWindow : Window
    {
        Account a = new Account();
        DB db = new DB();
        public AuthRegWindow()
        {
            InitializeComponent();
            this.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();


        public void logins(string login, string pass)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable Table = new DataTable();

            string query = $"select * from Account where Login='{login}' and Password='{pass}'";

            SqlCommand command = new SqlCommand(query, db.con);

            adapter.SelectCommand = command;

            adapter.Fill(Table);

            if (Table.Rows.Count == 1)
            {
                Account.Logins = Login.Text;
                System.Windows.MessageBox.Show("Вы успешно вошли!", "Успешно", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
                this.Close();

                db.con.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Close();
                db.con.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("Неверный логин/пароль", "Ошибка", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Login.Text) || string.IsNullOrWhiteSpace(Pass.Password))
            {
                System.Windows.MessageBox.Show("Пожалуйста, введите логин/пароль", "Ошибка", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                logins(Login.Text, Pass.Password);
            }
        }
    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LeaderSteel.View
{
    /// <summary>
    /// Логика взаимодействия для Accounts.xaml
    /// </summary>
    public partial class Accounts : System.Windows.Controls.Page
    {
        DB db = new DB();
        ViewModelListBox v = new ViewModelListBox();
        public Accounts()
        {
            InitializeComponent();
            v.LoadFullRowsFromDatabase(AccountsList);
        }

        public void registration()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable Table = new DataTable();

            string query = $"select Login,Password from Account where Login='{log.Text}'";

            SqlCommand cmd = new SqlCommand(query, db.con);

            adapter.SelectCommand = cmd;

            adapter.Fill(Table);

            db.con.Open();

            if (Table.Rows.Count == 0)
            {
                SqlCommand insertCommand = new SqlCommand($"insert into Account(Login,Password) values ('{log.Text}','{pass.Text}')", db.con);

                if (insertCommand.ExecuteNonQuery() == 1)
                {
                    System.Windows.MessageBox.Show("Аккаунт добавлен успешно!", "Успешно", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Такой логин уже существует, создайте другой", "Ошибка", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Exclamation);
            }

            db.con.Close();
        }

        public void delete()
        {
            string query = $"delete from Account where Login = '{log.Text}' and Password = '{pass.Text}';";

            SqlCommand command = new SqlCommand(query, db.con);

            try
            {
                db.con.Open();

                int rowsAffected = command.ExecuteNonQuery();

                db.con.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(log.Text) || string.IsNullOrWhiteSpace(pass.Text))
            {
                System.Windows.MessageBox.Show("Не заполнены обязательные поля!", "Ошибка", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Exclamation);
            }
            else
            {
                AccountsList.Items.Clear();
                registration();
                v.LoadFullRowsFromDatabase(AccountsList);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(log.Text) || string.IsNullOrWhiteSpace(pass.Text)) 
            {
                System.Windows.MessageBox.Show("Не заполнены обязательные поля!", "Ошибка", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Exclamation);
            }
            else
            {
                AccountsList.Items.Clear();
                delete();
                v.LoadFullRowsFromDatabase(AccountsList);
            }
        }

        private void AccountsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = AccountsList.SelectedItem;

            if (selectedItem != null)
            {
                var selectedItemText = selectedItem.ToString();

                var lines = selectedItemText.Split();

                log.Text = lines[0];
                pass.Text = lines[2];
            }
        }
    }
}

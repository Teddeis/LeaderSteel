using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LeaderSteel.ViewModel
{
    public class ViewModelListBox
    {
        DB db = new DB();
        public void LoadFullRowsFromDatabase(ListBox listBox)
        {
            db.con.Open();

            using (var command = db.con.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Account";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string row = "";

                        for (int i = 1; i < reader.FieldCount; i++)
                        {
                            string value = reader[i].ToString();

                            row += $"{value} | ";
                        }
                        listBox.Items.Add(row);
                    }
                }
            }
            db.con.Close();
        }
    }
}

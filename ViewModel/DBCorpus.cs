using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LeaderSteel.ViewModel
{
    public class DBCorpus
    {
        DB db = new DB();

        public void LoadFullRowsFromDatabaseCorpus(ListBox listBox)
        {
            db.con.Open();

            using (var command = db.con.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Corpus";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string row = "";

                        for (int i = 0; i < reader.FieldCount; i++)
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
        public void delete(string id)
        {
            string query = $"delete from Corpus where id = '{id}'";

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

    }
}

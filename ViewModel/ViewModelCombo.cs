using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LeaderSteel.ViewModel
{
    public class ViewModelCombo
    {
        DB db = new DB();
        public void SMaterial(ComboBox comboBox)
        {
            db.con.Open();

            string query = $"SELECT Type,Characteristic,Icons FROM Material";

            using (var command = new SqlCommand(query, db.con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox.Items.Add(reader["Type"]);
                    }
                }
            }
            db.con.Close();
        }


        public void SWarehouse(ComboBox combo)
        {
            db.con.Open();

            string query = $"SELECT Location FROM WareHouse";

            using (var command = new SqlCommand(query, db.con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        combo.Items.Add(reader["Location"]);
                    }
                }
            }
            db.con.Close();
        }

        public void SWarehouse2(ComboBox combo)
        {
            db.con.Open();

            string query = $"SELECT NumberWarehouse FROM WareHouse";

            using (var command = new SqlCommand(query, db.con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        combo.Items.Add(reader["NumberWarehouse"]);
                    }
                }
            }
            db.con.Close();
        }

        public void STransport(ComboBox comboBox)
        {
            db.con.Open();

            string query = $"SELECT Transport FROM Transport";

            using (var command = new SqlCommand(query, db.con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox.Items.Add(reader["Transport"]);
                    }
                }
            }
            db.con.Close();
        }
    }
}

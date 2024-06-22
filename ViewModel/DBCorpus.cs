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
                command.CommandText = "SELECT Corpus.id,Warehouse.NumberWarehouse,Transport.Transport ,Material.Type,Corpus.BoxNumber,Corpus.Quantity,Corpus.LengthCorpus,Corpus.WidthCorpus,Corpus.HeightCorpus,Corpus.Weight FROM Corpus, Warehouse, Material,Transport where Corpus.idWarehouse = Warehouse.id and Corpus.idTransport = Transport.id and Corpus.idMaterial = Material.id";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listBox.Items.Add($"id: {reader["id"].ToString()} | Склад: {reader["NumberWarehouse"].ToString()} | Транспорт: { reader["Transport"].ToString()} | Материал: {reader["Type"].ToString()} | Ящик: {reader["BoxNumber"].ToString()} | Количество: {reader["Quantity"].ToString()} | Габариты: {reader["LengthCorpus"].ToString()}x{reader["WidthCorpus"].ToString()}x{reader["HeightCorpus"].ToString()} | Вес: {reader["Weight"].ToString()}");
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

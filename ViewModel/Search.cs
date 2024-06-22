using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LeaderSteel.ViewModel
{
    internal class Search
    {
        DB db = new DB();
        public void SearchAll(ListBox listBox, TextBox textBox)
        {
            db.con.Open();

            using (var command = db.con.CreateCommand())
            {
                command.CommandText = $"SELECT Corpus.id,Warehouse.NumberWarehouse ,Transport.Transport ,Material.Type,Corpus.BoxNumber,Corpus.Quantity,Corpus.LengthCorpus,Corpus.WidthCorpus,Corpus.HeightCorpus,Corpus.Weight FROM Corpus, Warehouse, Material,Transport where Corpus.idWarehouse = Warehouse.id and Corpus.idTransport = Transport.id and Corpus.idMaterial = Material.id and (Warehouse.NumberWarehouse LIKE '%{textBox.Text}%' or Transport.Transport LIKE '%{textBox.Text}%' or Warehouse.NumberWarehouse LIKE '%{textBox.Text}%' or Corpus.BoxNumber LIKE '%{textBox.Text}%' or Corpus.Quantity LIKE '%{textBox.Text}%' or Corpus.LengthCorpus LIKE '%{textBox.Text}%' or Corpus.WidthCorpus LIKE '%{textBox.Text}%' or Corpus.HeightCorpus LIKE '%{textBox.Text}%' or Corpus.Weight LIKE '%{textBox.Text}%')";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listBox.Items.Add($"id: {reader["id"].ToString()} | Склад: {reader["NumberWarehouse"].ToString()} | Транспорт: {reader["Transport"].ToString()} | Материал: {reader["Type"].ToString()} | Ящик: {reader["BoxNumber"].ToString()} | Количество: {reader["Quantity"].ToString()} | Габариты: {reader["LengthCorpus"].ToString()}x{reader["WidthCorpus"].ToString()}x{reader["HeightCorpus"].ToString()} | Вес: {reader["Weight"].ToString()}");
                    }
                }
            }
            db.con.Close();
        }
    }
}

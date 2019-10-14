using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Biz2
{
    class KLIENTAT
    {
        //funksioni per shtuar klientat
        DataBaza db = new DataBaza();
        
        public bool shtoKlientet(string emri, string mbiemri, DateTime dataSkadimit, string nrKlientit,
            string nrKompanis, string emriKompanis, string qyteti, string rruga, string email, string reputacioni,
            string koment, string shuma, MemoryStream dosja)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `klientat`(`emri`,`mbiemri`,`emri_kompanis`,`nr_Kompanis`,`qyteti`,`rruga`,`nr_klientit`,`email`,`reputacioni`,`koment_shtese`,`shuma`,`data_skadimit`,`dosje`) VALUES (@em,@mbi,@emKmp,@nrKmp,@qt,@rr,@nrKl,@mail,@rep,@kom,@shm,@dt,@dok)", db.GetConnection);
            //@em,@mbi,@emKmp,@nrKmp,@qt,@rr,@nrKl,@mail,@rep,@kom,@shm,@dt,@dok
            command.Parameters.Add("@em", MySqlDbType.VarChar).Value = emri;
            command.Parameters.Add("@mbi", MySqlDbType.VarChar).Value = mbiemri;
            command.Parameters.Add("@emKmp", MySqlDbType.VarChar).Value = emriKompanis;
            command.Parameters.Add("@nrKmp", MySqlDbType.VarChar).Value = nrKompanis;
            command.Parameters.Add("@qt", MySqlDbType.Text).Value = qyteti;
            command.Parameters.Add("@rr", MySqlDbType.Text).Value = rruga;
            command.Parameters.Add("@nrKl", MySqlDbType.VarChar).Value = nrKlientit;
            command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = email;
            command.Parameters.Add("@rep", MySqlDbType.VarChar).Value = reputacioni;
            command.Parameters.Add("@kom", MySqlDbType.Text).Value = koment;
            command.Parameters.Add("@shm", MySqlDbType.Text).Value =shuma;
            command.Parameters.Add("@dt", MySqlDbType.Date).Value = dataSkadimit;
            command.Parameters.Add("@dok", MySqlDbType.Blob).Value = dosja.ToArray();

            
            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }


            
        }

        public DataTable shfaqKlientat(MySqlCommand command)
        {
            command.Connection = db.GetConnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }

        //funksion per editimin e klientave

        public bool editoKlientat(int id, string emri, string mbiemri, DateTime dataSkadimit, string nrKlientit,
            string nrKompanis, string emriKompanis, string qyteti, string rruga, string email, string reputacioni,
            string koment, string shuma, MemoryStream dosja)
        {

            MySqlCommand command = new MySqlCommand("UPDATE `klientat` SET `emri`=@em,`mbiemri`=@mbi,`data_skadimit`=@dt,`nr_klientit`=@nrKl,`nr_kompanis`=@nrKmp,`qyteti`=@qt,`rruga`=@rr,`email`=@mail,`reputacioni`=@rep,`koment_shtese`=@kom,`shuma`=@shm,`dosje`=@dok,`emri_kompanis`=@emKmp WHERE `id`=@ID", db.GetConnection);
           
            //@ID,@em,@mbi,@emKmp,@nrKmp,@qt,@rr,@nrKl,@mail,@rep,@kom,@shm,@dt,@dok

            command.Parameters.Add("@ID", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@em", MySqlDbType.VarChar).Value = emri;
            command.Parameters.Add("@mbi", MySqlDbType.VarChar).Value = mbiemri;
            command.Parameters.Add("@emKmp", MySqlDbType.VarChar).Value = emriKompanis;
            command.Parameters.Add("@nrKmp", MySqlDbType.VarChar).Value = nrKompanis;
            command.Parameters.Add("@qt", MySqlDbType.Text).Value = qyteti;
            command.Parameters.Add("@rr", MySqlDbType.Text).Value = rruga;
            command.Parameters.Add("@nrKl", MySqlDbType.VarChar).Value = nrKlientit;
            command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = email;
            command.Parameters.Add("@rep", MySqlDbType.VarChar).Value = reputacioni;
            command.Parameters.Add("@kom", MySqlDbType.Text).Value = koment;
            command.Parameters.Add("@shm", MySqlDbType.Text).Value = shuma;
            command.Parameters.Add("@dt", MySqlDbType.Date).Value = dataSkadimit;
            command.Parameters.Add("@dok", MySqlDbType.Blob).Value = dosja.ToArray();


            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        }
        //create a function to dlete the selected student
        public bool fshijKlientin(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `klientat` WHERE `id`=@klientatID", db.GetConnection);

            //@klientatID

            command.Parameters.Add("@klientatID", MySqlDbType.Int32).Value = id;
          


            db.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        }
    }
}

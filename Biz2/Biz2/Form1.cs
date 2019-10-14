using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;


namespace Biz2
{
    public partial class Form1 : Form
    {
    
        public Form1()
        {
            InitializeComponent();
            LoadChart();
        }

             readonly KLIENTAT klientat = new KLIENTAT();
        
        private void BtnUpload_Click(object sender, EventArgs e)
        {
            //browse image from HDD/Computer
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image (*.jpg;*.png;*.gif) |*.jpg;*.png;*.gif";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                picBoxDosje.Image = Image.FromFile(opf.FileName);
            }
        }

        //funksion per verifikimin e tdhenav
        bool verifiko()
        {
            
            if ((txtEmri.Text.Trim() == "") ||
            (txtMbiemri.Text.Trim() == "") ||
            (txtNrTel.Text.Trim() == "") ||
            (txtRruga.Text.Trim() == "") ||
            (dateTimePicker.Value.Date == DateTime.Now.Date) ||
            (cmbQyteti.Text.Trim() == "") ||
            (txtShuma.Text.Trim() == ""))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            //shto klientin e ri
          
            string emri = txtEmri.Text;
            string mbiemri = txtMbiemri.Text;
            string emriKompanis = txtEmriKomp.Text;
            string nrKompanis = txtNrKomp.Text;
            string qyteti = cmbQyteti.Text;
            string rruga = txtRruga.Text;
            string nrKlientit = txtNrTel.Text;
            string email = txtEmail.Text;
            string reputacioni;
            if (rdbPoz.Checked)
            {
                reputacioni = "Pozitiv";
            }
            else if (rdbNeg.Checked)
            {
                reputacioni = "Negativ";
            }
            else
            {
                reputacioni = "Neotral";
            }

            string koment = txtkoment.Text;
            string shuma = txtShuma.Text;
            DateTime dataSkadimit = dateTimePicker.Value;
            MemoryStream dosja = new MemoryStream();
          
            if (verifiko())
            {
               

                picBoxDosje.Image.Save(dosja,picBoxDosje.Image.RawFormat);
               //picBoxDosje.Image.Save(dosja,picBoxDosje.Image.RawFormat);

           
                if(klientat.shtoKlientet(emri,mbiemri,dataSkadimit,nrKlientit,nrKompanis,emriKompanis,qyteti,rruga,email,reputacioni,koment,shuma,dosja))
                {
                    MessageBox.Show("Klienti i ri eshte shtuar", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Nuk mund te procesohet.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                    MessageBox.Show("Fushat prioritare duet plotesuar", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                // Me kry funksionin qe alarmon nese sepaku fushat e rendesishme(prioritare) jan t'zbrazta.
                //ato duhet mbushur patjeter

            }
            //
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `klientat`");
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn dosjeCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 50;
            dataGridView1.DataSource = klientat.shfaqKlientat(command);
            dosjeCol = (DataGridViewImageColumn) dataGridView1.Columns[12];
            dosjeCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
            
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            // edito klientat e selectun
            //shto klientin e ri
           
            int id = Convert.ToInt32(txtID.Text);
            string emri = txtEmri.Text;
            string mbiemri = txtMbiemri.Text;
            string emriKompanis = txtEmriKomp.Text;
            string nrKompanis = txtNrKomp.Text;
            string qyteti = cmbQyteti.Text;
            string rruga = txtRruga.Text;
            string nrKlientit = txtNrTel.Text;
            string email = txtEmail.Text;
            string reputacioni;
            if (rdbPoz.Checked)
            {
                reputacioni = "Pozitiv";
            }
            else if (rdbNeg.Checked)
            {
                reputacioni = "Negativ";
            }
            else
            {
                reputacioni = "Neotral";
            }

            string koment = txtkoment.Text;
            string shuma = txtShuma.Text;
            DateTime dataSkadimit = dateTimePicker.Value;
            MemoryStream dosja = new MemoryStream();



            if (verifiko())
            {


                picBoxDosje.Image.Save(dosja, picBoxDosje.Image.RawFormat);
                //picBoxDosje.Image.Save(dosja,picBoxDosje.Image.RawFormat);


                if (klientat.editoKlientat(id,emri, mbiemri, dataSkadimit, nrKlientit, nrKompanis, emriKompanis, qyteti, rruga, email, reputacioni, koment, shuma, dosja))
                {
                    MessageBox.Show("Klienti eshte Perditesuar", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Nuk mund te procesohet.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Fushat prioritare duet plotesuar", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                // Me kry funksionin qe alarmon nese sepaku fushat e rendesishme(prioritare) jan t'zbrazta.
                //ato duhet mbushur patjeter

            }
        }

        private void DataGridView1_DoubleClick(object sender, EventArgs e)
        {
            //id emri mbiemri data nrKlientit nr kompanis qyteti rruga email rep kommenti shuma dosja emriKomp
            Form1 editoFshijKlientat = new Form1();
            editoFshijKlientat.txtID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            editoFshijKlientat.txtEmri.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            editoFshijKlientat.txtMbiemri.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            editoFshijKlientat.txtEmriKomp.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            editoFshijKlientat.txtNrKomp.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            editoFshijKlientat.txtRruga.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            editoFshijKlientat.cmbQyteti.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            editoFshijKlientat.txtNrTel.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            editoFshijKlientat.txtEmail.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            editoFshijKlientat.txtkoment.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            editoFshijKlientat.txtShuma.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            editoFshijKlientat.dateTimePicker.Value = (DateTime)dataGridView1.CurrentRow.Cells[3].Value;
            editoFshijKlientat.txtEmriKomp.Text = dataGridView1.CurrentRow.Cells[13].Value.ToString();
            byte[] dosje;
            dosje =(byte[]) dataGridView1.CurrentRow.Cells[12].Value;
            MemoryStream dosja = new MemoryStream(dosje);
            editoFshijKlientat.picBoxDosje.Image = Image.FromStream(dosja);
            editoFshijKlientat.Show();


            // Reputacioni

            

            if (dataGridView1.CurrentRow.Cells[9].Value.ToString() == "Pozitiv")
            {
                editoFshijKlientat.rdbPoz.Checked = true;
            }
            else if (dataGridView1.CurrentRow.Cells[9].Value.ToString() == "Negativ")
            {
                editoFshijKlientat.rdbNeg.Checked = true;
            }


        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            // kerko studentet ne baz te IDs
            
           
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Ju lutem shkruani ID'n te cilen e kerkoni");
            }
            else
            {
                var id = Convert.ToInt32(txtID.Text);
                var cmd = (
                    "SELECT `id`, `emri`, `mbiemri`, `data_skadimit`, `nr_klientit`, `nr_kompanis`, `qyteti`, `rruga`, `email`, `reputacioni`, `koment_shtese`, `shuma`, `dosje`, `emri_kompanis` FROM `klientat` WHERE `id`=" +
                    id);



                MySqlCommand command = new MySqlCommand(cmd);

                DataTable table = klientat.shfaqKlientat(command);


                if (table.Rows.Count > 0)
                {

                    txtEmri.Text = table.Rows[0]["emri"].ToString();
                    txtMbiemri.Text = table.Rows[0]["mbiemri"].ToString();
                    txtEmriKomp.Text = table.Rows[0]["emri_kompanis"].ToString();
                    txtNrKomp.Text = table.Rows[0]["nr_kompanis"].ToString();
                    txtRruga.Text = table.Rows[0]["rruga"].ToString();
                    cmbQyteti.Text = table.Rows[0]["qyteti"].ToString();
                    txtNrTel.Text = table.Rows[0]["nr_klientit"].ToString();
                    txtEmail.Text = table.Rows[0]["email"].ToString();
                    txtkoment.Text = table.Rows[0]["koment_shtese"].ToString();
                    txtShuma.Text = table.Rows[0]["shuma"].ToString();
                    dateTimePicker.Value = (DateTime) table.Rows[0]["data_skadimit"];
                    // reputacioni

                    if (table.Rows[0]["reputacioni"].ToString() == "Pozitiv")
                    {
                        rdbPoz.Checked = true;
                    }
                    else if (table.Rows[0]["reputacioni"].ToString() == "Negativ")
                    {
                        rdbNeg.Checked = true;
                    }
                    else
                    {
                        rdbNeo.Checked = true;
                    }

                    // dosjet/ foto / dokumente

                    byte[] pic = (byte[]) table.Rows[0]["dosje"];
                    MemoryStream dosja = new MemoryStream(pic);
                    picBoxDosje.Image = Image.FromStream(dosja);
                }
            }

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {

            // fshij klientet e selektum
           

            if (txtID.Text == "")
            {
                MessageBox.Show("Per te fshijr klientit , zgjidhni nje ID", "Info", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                int id = Convert.ToInt32(txtID.Text);




                if (MessageBox.Show("Jeni sigurt qe doni ta fshini kete Klient?", "Info", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (klientat.fshijKlientin(id))
                    {
                        MessageBox.Show("Klienti eshte Fshir", "Info", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        txtID.Text = "";
                        txtEmri.Text = "";
                        txtMbiemri.Text = "";
                        txtEmriKomp.Text = "";
                        txtNrKomp.Text = "";
                        txtRruga.Text = "";
                        cmbQyteti.Text = "";
                        txtNrTel.Text = "";
                        txtEmail.Text = "";
                        txtkoment.Text = "";
                        txtShuma.Text = "";
                        dateTimePicker.Value = DateTime.Now;
                        picBoxDosje.Image = null;

                    }
                    else
                    {
                        MessageBox.Show("Klienti NUk eshte Fshir", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }


                }
            }

        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            //aktualizo listen ne datagridview

            MySqlCommand command = new MySqlCommand("SELECT * FROM `klientat`");
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn dosjeCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 50;
            dataGridView1.DataSource = klientat.shfaqKlientat(command);
            dosjeCol = (DataGridViewImageColumn)dataGridView1.Columns[12];
            dosjeCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
            chart1.Update();
        }

        private void TxtID_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void LoadChart()
        {
            MySqlConnection cn =
                new MySqlConnection(
                    "Data Source=remotemysql.com;port=3306;username=9FJ1dIGmOK;password=OCGS3US7e2;database=9FJ1dIGmOK");
            MySqlCommand cmdDataBase = new MySqlCommand("select * from 9FJ1dIGmOK.klientat ;", cn);
            MySqlDataReader myReader;
            try
            {
                cn.Open();
                myReader = cmdDataBase.ExecuteReader();

                while (myReader.Read())
                {
                    this.chart1.Series["Skadimi"].Points.AddXY(myReader.GetString("emri"), myReader.GetDateTime("data_skadimit"));
                    //this.chart1.Series["Shuma"].Points.AddY( myReader.GetString("shuma"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        
    
    }

}
}

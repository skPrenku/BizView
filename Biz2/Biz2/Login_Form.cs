using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Biz2
{
    public partial class Login_Form : Form
    {
        public Login_Form()
        {
            InitializeComponent();
        }

        private void Login_Form_Load(object sender, EventArgs e)
        {
            this.textBoxPassword.PasswordChar = '*';

        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            DataBaza db = new DataBaza();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `username` = @usn AND `password` = @pass", db.GetConnection);
            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = textBoxUsername.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = textBoxPassword.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                this.DialogResult = DialogResult.OK;

            }
            else
            {
                MessageBox.Show("Përdoruesi ose Fjalëkalimi janë gabim.","Problem ne Kycje",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Label4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

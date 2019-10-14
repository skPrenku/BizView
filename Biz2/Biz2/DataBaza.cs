using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace Biz2
{
    /*
     * ketu eshte krijuar lidhja mes aplikacionit dhe databazes.MySQL
     */
    class DataBaza
    {
        private MySqlConnection conn = new MySqlConnection("Data Source=remotemysql.com;port=3306;username=9FJ1dIGmOK;password=OCGS3US7e2;database=9FJ1dIGmOK");

        //krijimi i funksionit per tkrijuar lidhjen

        public MySqlConnection GetConnection

        {
            get { return conn; }
        }

        public void openConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public void closeConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

    }

    

    // krijo funksionin per te hapur lidhjen.

  

}

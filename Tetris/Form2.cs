using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Tetris
{
    public partial class Form2 : Form
    {
        SqlConnection conn = null;
        SqlDataAdapter da = null;
        DataSet dt = null;
        SqlCommandBuilder cmd = null;
        string cs = "";
        public Form2(Form1 f)
        {
            InitializeComponent();
            conn = new SqlConnection();
            cs = ConfigurationManager.ConnectionStrings["Records"].ConnectionString;
            conn.ConnectionString = cs;
            dataGridView1.RowHeadersVisible = false;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                da = new SqlDataAdapter("select * from Records;", conn);
                cmd = new SqlCommandBuilder(da);
                dt = new DataSet();
                da.Fill(dt, "records");
                dataGridView1.DataSource = dt.Tables["Records"];
                this.dataGridView1.Columns["id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

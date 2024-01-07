using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLLGD
{

    public partial class frmMonhoc : Form
    {

        SqlConnection conn;
        SqlDataAdapter da_MONHOC;
        DataSet ds_MONHOC;
        public frmMonhoc()
        {
            InitializeComponent();
            string strConnect = "Data Source = DESKTOP-UMUNA1R; Initial Catalog= QLGD; User Id=sa; Password = 123 ";
            conn = new SqlConnection(strConnect);
            DataTable dt_ND = new DataTable();
            ds_MONHOC = new DataSet();
            string sql = "select*from MONHOC";
            da_MONHOC = new SqlDataAdapter(sql, conn);
            //dt_ND=db.getDataTable(str);
            da_MONHOC.Fill(ds_MONHOC, "MONHOC");
        }

        private void frmMonhoc_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds_MONHOC.Tables["MONHOC"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow newrow = ds_MONHOC.Tables[0].NewRow();
            newrow["maMon"] = txtMamon.Text;
            newrow["tenMon"] = txtTenmon.Text;
            ds_MONHOC.Tables[0].Rows.Add(newrow);
            SqlCommandBuilder cmb = new SqlCommandBuilder(da_MONHOC);
            da_MONHOC.Update(ds_MONHOC, "MONHOC");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_MONHOC.Tables["MONHOC"].Columns["maMon"];
            ds_MONHOC.Tables["MONHOC"].PrimaryKey = key;
            DataRow dr = ds_MONHOC.Tables["MONHOC"].Rows.Find(txtMamon.Text);
            if (dr != null)
            {
                dr["maMon"] = txtMamon.Text;
                dr["tenMon"] = txtTenmon.Text;
            }
            SqlCommandBuilder cmb = new SqlCommandBuilder(da_MONHOC);
            da_MONHOC.Update(ds_MONHOC, "MONHOC");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_MONHOC.Tables["MONHOC"].Columns["maMon"];
            ds_MONHOC.Tables["MONHOC"].PrimaryKey = key;
            DataRow dr = ds_MONHOC.Tables["MONHOC"].Rows.Find(txtMamon.Text);
            if (dr != null)
            {
                dr.Delete();
            }
            SqlCommandBuilder cmb = new SqlCommandBuilder(da_MONHOC);
            da_MONHOC.Update(ds_MONHOC, "MONHOC");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

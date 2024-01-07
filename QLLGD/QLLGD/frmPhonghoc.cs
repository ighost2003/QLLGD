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
    public partial class frmPhonghoc : Form
    {
        SqlConnection conn;
        SqlDataAdapter da_PHONGHOC;
        DataSet ds_PHONGHOC;
        public frmPhonghoc()
        {
            InitializeComponent();
            string strConnect = "Data Source = DESKTOP-UMUNA1R; Initial Catalog= QLGD; User Id=sa; Password = 123 ";
            conn = new SqlConnection(strConnect);
            DataTable dt_ND = new DataTable();
            ds_PHONGHOC = new DataSet();
            string sql = "select*from PHONGHOC";
            da_PHONGHOC = new SqlDataAdapter(sql, conn);
            //dt_ND=db.getDataTable(str);
            da_PHONGHOC.Fill(ds_PHONGHOC, "PHONGHOC");
        }

        private void frmPhonghoc_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds_PHONGHOC.Tables["PHONGHOC"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow newrow = ds_PHONGHOC.Tables[0].NewRow();
            newrow["maPhong"] = txtMaphong.Text;
            newrow["tenPhong"] = txtTenphong.Text;
            newrow["soBan"] = txtSoban.Text;
            ds_PHONGHOC.Tables[0].Rows.Add(newrow);
            SqlCommandBuilder cmb = new SqlCommandBuilder(da_PHONGHOC);
            da_PHONGHOC.Update(ds_PHONGHOC, "PHONGHOC");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_PHONGHOC.Tables["PHONGHOC"].Columns["maPhong"];
            ds_PHONGHOC.Tables["PHONGHOC"].PrimaryKey = key;
            DataRow dr = ds_PHONGHOC.Tables["PHONGHOC"].Rows.Find(txtMaphong.Text);
            if (dr != null)
            {
                dr["maPhong"] = txtMaphong.Text;
                dr["tenPhong"] = txtTenphong.Text;
                dr["soBan"] = txtSoban.Text;
            }
            SqlCommandBuilder cmb = new SqlCommandBuilder(da_PHONGHOC);
            da_PHONGHOC.Update(ds_PHONGHOC, "PHONGHOC");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_PHONGHOC.Tables["PHONGHOC"].Columns["maPhong"];
            ds_PHONGHOC.Tables["PHONGHOC"].PrimaryKey = key;
            DataRow dr = ds_PHONGHOC.Tables["PHONGHOC"].Rows.Find(txtMaphong.Text);
            if (dr != null)
            {
                dr.Delete();
            }
            SqlCommandBuilder cmb = new SqlCommandBuilder(da_PHONGHOC);
            da_PHONGHOC.Update(ds_PHONGHOC, "PHONGHOC");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

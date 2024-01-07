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
    public partial class frmGSGD : Form
    {
        SqlConnection conn;
        SqlDataAdapter da_GSGD;
        DataSet ds_GSGD;
        public frmGSGD()
        {
            InitializeComponent();
            string strConnect = "Data Source = DESKTOP-UMUNA1R; Initial Catalog= QLGD; User Id=sa; Password = 123 ";
            conn = new SqlConnection(strConnect);
            DataTable dt_ND = new DataTable();
            ds_GSGD = new DataSet();
            string sql = "select*from GSGD";
            da_GSGD = new SqlDataAdapter(sql, conn);
            //dt_ND=db.getDataTable(str);
            da_GSGD.Fill(ds_GSGD, "GSGD");
        }

        private void frmGSGD_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds_GSGD.Tables["GSGD"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow newrow = ds_GSGD.Tables[0].NewRow();
            newrow["maGV"] = txtMagv.Text;
            newrow["sotietNghi"] = txtSotietnghi.Text;
            newrow["ngayNghi"] = dateTimePicker1.Value;
            newrow["soTietBu"] = txtSotietdaybu.Text;
            newrow["ngayBu"] = dateTimePicker2.Value;
            ds_GSGD.Tables[0].Rows.Add(newrow);
            SqlCommandBuilder cmb = new SqlCommandBuilder(da_GSGD);
            da_GSGD.Update(ds_GSGD, "GSGD");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_GSGD.Tables["GSGD"].Columns["maGV"];
            ds_GSGD.Tables["GSGD"].PrimaryKey = key;
            DataRow dr = ds_GSGD.Tables["GSGD"].Rows.Find(txtMagv.Text);
            if (dr != null)
            {
                dr["maGV"] = txtMagv.Text;
                dr["soTietNghi"] = txtSotietnghi.Text;
                dr["ngayNghi"] = dateTimePicker1.Value;
                dr["soTietBu"] = txtSotietdaybu.Text;
                dr["ngayBu"] = dateTimePicker2.Value;

                SqlCommandBuilder cmb = new SqlCommandBuilder(da_GSGD);
                da_GSGD.Update(ds_GSGD, "GSGD");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_GSGD.Tables["GSGD"].Columns["maGV"];
            ds_GSGD.Tables["GSGD"].PrimaryKey = key;
            DataRow dr = ds_GSGD.Tables["GSGD"].Rows.Find(txtMagv.Text);
            if (dr != null)
            {
                dr.Delete();
            }
            SqlCommandBuilder cmb = new SqlCommandBuilder(da_GSGD);
            da_GSGD.Update(ds_GSGD, "GSGD");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

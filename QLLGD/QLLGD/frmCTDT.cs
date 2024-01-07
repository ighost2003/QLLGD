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
    public partial class frmCTDT : Form
    {
        SqlConnection conn;
        SqlDataAdapter da_CTDT;
        DataSet ds_CTDT;
        public frmCTDT()
        {
            InitializeComponent();
            string strConnect = "Data Source = DESKTOP-UMUNA1R; Initial Catalog= QLGD; User Id=sa; Password = 123 ";
            conn = new SqlConnection(strConnect);
            DataTable dt_ND = new DataTable();
            ds_CTDT = new DataSet();
            string sql = "select*from CTDT";
            da_CTDT = new SqlDataAdapter(sql, conn);

            //dt_ND=db.getDataTable(str);
            da_CTDT.Fill(ds_CTDT, "CTDT");
        }

        private void frmCTDT_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds_CTDT.Tables["CTDT"];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataRow newrow = ds_CTDT.Tables[0].NewRow();
            newrow["maMon"] = txtMamon.Text;
            newrow["soTinchi"] = txtSotinchi.Text;
            newrow["soTCLT"] = txtTclt.Text;
            newrow["soTCTH"] = txtTcth.Text;
            newrow["hocKy"] = txtHocki.Text;
            ds_CTDT.Tables[0].Rows.Add(newrow);
            SqlCommandBuilder cmb = new SqlCommandBuilder(da_CTDT);
            da_CTDT.Update(ds_CTDT, "CTDT");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_CTDT.Tables["CTDT"].Columns["maMon"];
            ds_CTDT.Tables["CTDT"].PrimaryKey = key;
            DataRow dr = ds_CTDT.Tables["CTDT"].Rows.Find(txtMamon.Text);
            if (dr != null)
            {
                dr["maMon"] = txtMamon.Text;
                dr["soTinChi"] = txtSotinchi.Text;
                dr["soTCLT"] = txtTclt.Text;
                dr["soTCTH"] = txtTcth.Text;
                dr["hocKy"] = txtHocki.Text;
            }
            SqlCommandBuilder cmb = new SqlCommandBuilder(da_CTDT);
            da_CTDT.Update(ds_CTDT, "CTDT");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_CTDT.Tables["CTDT"].Columns["maMon"];
            ds_CTDT.Tables["CTDT"].PrimaryKey = key;
            DataRow dr = ds_CTDT.Tables["CTDT"].Rows.Find(txtMamon.Text);
            if (dr != null)
            {
                dr.Delete();
            }
            SqlCommandBuilder cmb = new SqlCommandBuilder(da_CTDT);
            da_CTDT.Update(ds_CTDT, "CTDT");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

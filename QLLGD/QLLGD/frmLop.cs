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
    public partial class frmLop : Form
    {
        SqlConnection conn;
        SqlDataAdapter da_LOP;
        DataSet ds_LOP;
        public frmLop()
        {
            InitializeComponent();
            string strConnect = "Data Source = DESKTOP-UMUNA1R; Initial Catalog= QLGD; User Id=sa; Password = 123 ";
            conn = new SqlConnection(strConnect);
            DataTable dt_ND = new DataTable();
            ds_LOP = new DataSet();
            string sql = "select*from Lop";
            da_LOP = new SqlDataAdapter(sql, conn);
            //dt_ND=db.getDataTable(str);
            da_LOP.Fill(ds_LOP, "LOP");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DataRow newrow = ds_LOP.Tables[0].NewRow();
            newrow["maLop"] = txtMalop.Text;
            newrow["tenLop"] = txtTenlop.Text;
            newrow["soSV"] = txtSosv.Text;
            ds_LOP.Tables[0].Rows.Add(newrow);
            SqlCommandBuilder cmb = new SqlCommandBuilder(da_LOP);
            da_LOP.Update(ds_LOP, "LOP");
        }

        private void frmLop_Load(object sender, EventArgs e)
        {
            dataGridViewLop.DataSource = ds_LOP.Tables["LOP"];
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_LOP.Tables["LOP"].Columns["maLop"];
            ds_LOP.Tables["LOP"].PrimaryKey = key;
            DataRow dr = ds_LOP.Tables["LOP"].Rows.Find(txtMalop.Text);
            if (dr != null)
            {
                dr["maLop"] = txtMalop.Text;
                dr["tenLop"] = txtTenlop.Text;
                dr["soSv"] = txtSosv.Text;
            }
            SqlCommandBuilder cmb = new SqlCommandBuilder(da_LOP);
            da_LOP.Update(ds_LOP, "LOP");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_LOP.Tables["LOP"].Columns["maLop"];
            ds_LOP.Tables["LOP"].PrimaryKey = key;
            DataRow dr = ds_LOP.Tables["LOP"].Rows.Find(txtMalop.Text);
            if (dr != null)
            {
                dr.Delete();
            }
            SqlCommandBuilder cmb = new SqlCommandBuilder(da_LOP);
            da_LOP.Update(ds_LOP, "LOP");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

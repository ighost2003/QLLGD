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
    public partial class frmHSGV : Form
    {
        SqlConnection conn;
        SqlDataAdapter da_HSGV;
        DataSet ds_HSGV;
        public frmHSGV()
        {
            InitializeComponent();
            string strConnect = "Data Source = DESKTOP-UMUNA1R; Initial Catalog= QLGD; User Id=sa; Password = 123 ";
            conn = new SqlConnection(strConnect);
            DataTable dt_ND = new DataTable();
            ds_HSGV = new DataSet();
            string sql = "select*from HSGV";
            da_HSGV = new SqlDataAdapter(sql, conn);

            //dt_ND=db.getDataTable(str);
            da_HSGV.Fill(ds_HSGV, "HSGV");
        }

        private void frmHSGV_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds_HSGV.Tables["HSGV"];
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DataRow newrow = ds_HSGV.Tables[0].NewRow();
            newrow["maGV"] = txtMagiaovien.Text;
            newrow["tenGV"] = txtTengiaovien.Text;
            newrow["ngaysinh"] = dtpNgaysinh.Value;
            newrow["gioiTinh"] = txtGioitinh.Text;
            newrow["diaChi"] = txtDiachi.Text;
            newrow["dienThoai"] = txtDienthoai.Text;
            newrow["chucVu"] = txtChucvu.Text;
            newrow["maTomon"] = txtMatomon.Text;
            ds_HSGV.Tables[0].Rows.Add(newrow);
            SqlCommandBuilder cmb = new SqlCommandBuilder(da_HSGV);
            da_HSGV.Update(ds_HSGV, "HSGV");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_HSGV.Tables["HSGV"].Columns["magv"];
            ds_HSGV.Tables["HSGV"].PrimaryKey = key;
            DataRow dr = ds_HSGV.Tables["HSGV"].Rows.Find(txtMagiaovien.Text);
            if (dr != null)
            {
                dr.Delete();
            }
            SqlCommandBuilder cmb = new SqlCommandBuilder(da_HSGV);
            da_HSGV.Update(ds_HSGV, "HSGV");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_HSGV.Tables["HSGV"].Columns["maGV"];
            ds_HSGV.Tables["HSGV"].PrimaryKey = key;
            DataRow dr = ds_HSGV.Tables["HSGV"].Rows.Find(txtMagiaovien.Text);
            if (dr != null)
            {
                dr["magv"] = txtMagiaovien.Text;
                dr["tenGV"] = txtTengiaovien.Text;
                dr["ngaySinh"] = dtpNgaysinh.Value;
                dr["gioitinh"] = txtGioitinh.Text;
                dr["diachi"] = txtDiachi.Text;
                dr["dienThoai"] = txtDienthoai.Text;
                dr["chucVu"] = txtChucvu.Text;
                dr["maTomon"] = txtMatomon.Text;
            }
            SqlCommandBuilder cmb = new SqlCommandBuilder(da_HSGV);
            da_HSGV.Update(ds_HSGV, "HSGV");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLLGD
{
    public partial class frmPCGD : Form
    {

        SqlConnection conn;
        SqlDataAdapter da_PCGD;
        DataSet ds_PCGD;
        public frmPCGD()
        {
            InitializeComponent();

            string strConnect = "Data Source = DESKTOP-LSFEJMC\\SQLEXPRESS; Initial Catalog= QLGD; User Id=sa; Password = 123 ";
            conn = new SqlConnection(strConnect);
            DataTable dt_ND = new DataTable();
            ds_PCGD = new DataSet();
            string sql = "select*from XemPhanCong";
            da_PCGD = new SqlDataAdapter(sql, conn);

            //dt_ND=db.getDataTable(str);
            da_PCGD.Fill(ds_PCGD, "PCGD");
        }

        private void frm_phancongGD_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds_PCGD.Tables["PCGD"];

        }



        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string tenGiangVienCanTim = textBox2.Text.Trim();

            // Thực hiện tìm kiếm trong DataTable với tên giảng viên chứa trong cột "tengv"
            DataRow[] dongTimThay = ds_PCGD.Tables["PCGD"].Select($"tengv LIKE '%{tenGiangVienCanTim}%'");

            // Kiểm tra xem có bao nhiêu dòng được tìm thấy
            if (dongTimThay.Length > 0)
            {
                // Tạo một DataTable mới để chứa dữ liệu tìm kiếm
                DataTable dtTimKiem = dongTimThay.CopyToDataTable();

                // Hiển thị dữ liệu tìm kiếm trong DataGridView
                dataGridView1.DataSource = dtTimKiem;


            }
            else
            {
                MessageBox.Show("Không tìm thấy giảng viên.", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

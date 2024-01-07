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
    public partial class frmLogin : Form
    {
        DBConnect conn;
        public frmLogin()
        {
            InitializeComponent();  
            conn = new DBConnect();



        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }
        private int userStatus;

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Hãy nhập vào tên đăng nhập");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Hãy nhập vào mật khẩu");
            }
            else
            {
                string user = textBox1.Text;
                string pass = textBox2.Text;
                string check = checkLogin(user, pass);

                if (check == "")
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng");
                }
                else
                {
                    userStatus = GetUserStatus(user);

                    frmMain frm = new frmMain(userStatus);
                    this.Hide();
                    frm.ShowDialog();
                    this.Close();
                }

            }
        }
        private int GetUserStatus(string username)
        {

            using (SqlConnection conn = Getconnection())
            {

                if (conn != null)
                {
                    conn.Open();

                    string query = "SELECT manhom FROM ND WHERE taikhoan = @username";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int status))
                        {
                            return status;
                        }
                    }
                    conn.Close();
                }
            }
            return 0;
        }

        public static string sqlcon = "server =DESKTOP-LSFEJMC\\SQLEXPRESS; user id= sa; password= 123; database = QLGD";
        public static SqlConnection Getconnection()
        {
            SqlConnection con = new SqlConnection(sqlcon);
            return con;
        }

        public static string ExcuteScalar(string strQuery, CommandType cmdtype, string[] para, object[] values)
        {
            SqlConnection conn = new SqlConnection();
            conn = Getconnection();
            conn.Open();
            string efftectRecord = "";
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandText = strQuery;
            sqlcmd.Connection = conn;
            sqlcmd.CommandType = cmdtype;

            SqlParameter sqlpara;
            for (int i = 0; i < para.Length; i++)
            {
                sqlpara = new SqlParameter();
                sqlpara.ParameterName = para[i];
                sqlpara.SqlValue = values[i];
                sqlcmd.Parameters.Add(sqlpara);
            }
            try
            {
                efftectRecord = sqlcmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                efftectRecord = "";
                Console.WriteLine("Error:" + ex.Message);
            }
            return efftectRecord;
            conn.Close();
        }

        public static string check_Userlogin = "spCheckLogin";

        protected string username { get; set; }
        protected string password { get; set; }

        public string checkLogin(string user, string password)
        {
            string[] paras = new string[2] { "@username", "@password" };
            object[] values = new object[2] { user, password };
            return ExcuteScalar(check_Userlogin, CommandType.StoredProcedure, paras, values);
        }


    }
}

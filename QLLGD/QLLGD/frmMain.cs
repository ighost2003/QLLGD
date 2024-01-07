using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLLGD
{
    public partial class frmMain : Form
    {
        DBConnect conn;
        private int userStatus;
        public frmMain(int userStatus)
        {
            InitializeComponent();
            conn = new DBConnect();
            this.userStatus = userStatus;
            UpdateMenuStatus();
        }

        private void UpdateMenuStatus()
        {
            // Tùy thuộc vào giá trị trạng thái, bạn có thể thực hiện các tác vụ tương ứng
            if (userStatus == 1)
            {
                quảnLýNgườiDùngToolStripMenuItem.Enabled = true;
                nHẬPLIỆUToolStripMenuItem.Enabled = true;
                pHÂNCÔNGToolStripMenuItem.Enabled = true;
                // Disable ToolStripMenuItem
            }
            else
            {
                quảnLýNgườiDùngToolStripMenuItem.Enabled = false;
                nHẬPLIỆUToolStripMenuItem.Enabled = false;
                pHÂNCÔNGToolStripMenuItem.Enabled = false;

            }
        }

        private void quảnLíNgườiDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmUser frmnd = new frmUser();
            frmnd.ShowDialog();
        }

        private void chươngTrìnhĐàoTạoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmCTDT frmctdt = new frmCTDT();
            frmctdt.ShowDialog();

        }

        private void phânCôngGiảngDạyToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmPhancong frmpcgd = new frmPhancong();
            frmpcgd.ShowDialog();
        }

        private void hồSơGiảngViênToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmHSGV frmdsgv = new frmHSGV();
            frmdsgv.ShowDialog();
        }

        private void lớpToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmLop frml = new frmLop();
            frml.ShowDialog();
        }

        private void mônHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmMonhoc frmmh = new frmMonhoc();
            frmmh.ShowDialog();
        }

        private void giámSátGiảngDạyToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmGSGD frmgsgd = new frmGSGD();
            frmgsgd.ShowDialog();
        }

        private void phòngHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmPhonghoc frmph = new frmPhonghoc();
            frmph.ShowDialog();
        }

        private void tÌMKIẾMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPCGD frmpc= new frmPCGD();
            frmpc.ShowDialog();
        }

        private void nHẬPLIỆUToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            
            frmLogin frmLogin = new frmLogin();
            
            frmLogin.ShowDialog();
            
        }
    }
}

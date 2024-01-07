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

namespace QLLGD
{
    public partial class frmPhancong : Form
    {
        private SqlConnection connection;
        //private Dictionary<string, int> assignmentCounts = new Dictionary<string, int>();
        private Dictionary<string, string> maGVDictionary;
        private Dictionary<string, string> maMonDictionary;


        public frmPhancong()
        {
            InitializeComponent();
            connection = new SqlConnection("Data Source=DESKTOP-LSFEJMC\\SQLEXPRESS; Initial Catalog=QLGD; User Id=sa; Password=123");
            maGVDictionary = new Dictionary<string, string>();
            maMonDictionary = new Dictionary<string, string>();
        }

        private void frmPhanCong_Load(object sender, EventArgs e)
        {
            LoadComboBox("SELECT MaGV, TenGV FROM HSGV", comboBox1, "MaGV", "TenGV");
            LoadComboBox("SELECT Mamon, Tenmon FROM MonHoc", comboBox2, "Mamon", "Tenmon");
            LoadComboBox("SELECT Maphong, Tenphong FROM Phonghoc", comboBox3, "Maphong", "Maphong");
            LoadComboBox("SELECT Malop, Tenlop FROM Lop", comboBox4, "Malop", "Tenlop");
        }

        private void LoadComboBox(string query, ComboBox comboBox, string valueMember, string displayMember)
        {


            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    comboBox.DisplayMember = displayMember;
                    comboBox.ValueMember = valueMember;
                    comboBox.DataSource = dataTable;

                    // Đổ dữ liệu vào Dictionary
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string tenGV = row[displayMember].ToString();
                        string maGV = row[valueMember].ToString();
                        maGVDictionary[tenGV] = maGV;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private int GetAssignmentCountFromDatabase(string teacherName)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM PCGD WHERE MaGV = (SELECT MaGV FROM HSGV WHERE TenGV = @TenGV)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TenGV", teacherName);

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return Convert.ToInt32(reader[0]);
                        }
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting assignment count from the database: " + ex.Message);
                return 0;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private HashSet<string> selectedItems = new HashSet<string>();

        private void AddItemToListBox(ComboBox comboBox, ListBox listBox, string keyColumnName, string valueColumnName)
        {
            if (comboBox.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)comboBox.SelectedItem;
                string key = selectedRow[keyColumnName].ToString();
                string value = selectedRow[valueColumnName].ToString();

                // Kiểm tra nếu item đã tồn tại trong ListBox
                if (!selectedItems.Contains(key))
                {
                    selectedItems.Add(key);
                    listBox.Items.Add(new KeyValuePair<string, string>(key, value));
                }
                else
                {
                    MessageBox.Show($"Mục đã tồn tại trong {listBox.Name}.");
                }
            }
            else
            {
                MessageBox.Show($"Vui lòng chọn ít nhất một mục từ {comboBox.Name}.");
            }
        }


        private void RemoveItemFromListBox(ListBox listBox, string keyColumnName)
        {
            if (listBox.SelectedIndex != -1)
            {
                KeyValuePair<string, string> selectedPair = (KeyValuePair<string, string>)listBox.SelectedItem;
                selectedItems.Remove(selectedPair.Key);
                listBox.Items.RemoveAt(listBox.SelectedIndex);
            }
            else
            {
                MessageBox.Show($"Vui lòng chọn một mục trong {listBox.Name} để xóa.");
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {


            if (comboBox1.SelectedItem != null)
            {
                string maGV = ((DataRowView)comboBox1.SelectedItem)["MaGV"].ToString();
                string tenGV = ((DataRowView)comboBox1.SelectedItem)["TenGV"].ToString();

                // Kiểm tra số lần phân công của giáo viên từ cơ sở dữ liệu
                int assignmentCount = GetAssignmentCountFromDatabase(tenGV);

                // Kiểm tra nếu giáo viên đã được phân công 2 lần
                if (assignmentCount >= 2)
                {
                    MessageBox.Show($"Giáo viên {tenGV} đã được phân công tối đa 2 lần.");
                }
                else
                {
                    AddItemToListBox(comboBox1, listBox1, "MaGV", "TenGV");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một mục từ ComboBox1.");
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            RemoveItemFromListBox(listBox1, "MaGV");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string maMon = ((DataRowView)comboBox2.SelectedItem)["MaMon"].ToString();
                string tenMon = ((DataRowView)comboBox2.SelectedItem)["TenMon"].ToString();
                if (!listBox2.Items.Contains(maMon))
                {
                    listBox2.Items.Add(new KeyValuePair<string, string>(maMon, tenMon));
                }
                else
                {
                    MessageBox.Show("Mục đã tồn tại trong ListBox2.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một mục từ ComboBox2.");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một mục trong ListBox2 để xóa.");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                string tenph = ((DataRowView)comboBox3.SelectedItem)["maphong"].ToString();
                if (!listBox3.Items.Contains(tenph))
                {
                    listBox3.Items.Add(tenph);
                }
                else
                {
                    MessageBox.Show("Mục đã tồn tại trong ListBox3.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một mục từ ComboBox3.");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex != -1)
            {
                listBox3.Items.RemoveAt(listBox3.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một mục trong ListBox3 để xóa.");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem != null)
            {
                string tenlop = ((DataRowView)comboBox4.SelectedItem)["Tenlop"].ToString();
                if (!listBox4.Items.Contains(tenlop))
                {
                    listBox4.Items.Add(tenlop);
                }
                else
                {
                    MessageBox.Show("Mục đã tồn tại trong ListBox4.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một mục từ ComboBox4.");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedIndex != -1)
            {
                listBox4.Items.RemoveAt(listBox4.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một mục trong ListBox4 để xóa.");
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo bảng dữ liệu PCGD mới
                DataTable dtPCGD = new DataTable();
                dtPCGD.Columns.Add("MaGV", typeof(string));
                dtPCGD.Columns.Add("MaHocPhan", typeof(string));
                dtPCGD.Columns.Add("MaPhong", typeof(string));
                dtPCGD.Columns.Add("MaLop", typeof(string));
                dtPCGD.Columns.Add("NgayBatDau", typeof(DateTime));
                dtPCGD.Columns.Add("NgayKetThuc", typeof(DateTime));

                // Mở kết nối một lần
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                // Lặp qua danh sách ngẫu nhiên để thu thập thông tin và ghi vào bảng dữ liệu PCGD
                for (int pcgdIndex = 0; pcgdIndex < Math.Min(listBox1.Items.Count, listBox2.Items.Count); pcgdIndex++)
                {
                    // Lấy giá trị từ các ListBox tương ứng
                    string maGV = ((KeyValuePair<string, string>)listBox1.Items[pcgdIndex]).Key;
                    string tenMon = ((KeyValuePair<string, string>)listBox2.Items[pcgdIndex]).Key;

                    // Truy cập giá trị từ các ListBox khác
                    string maPhong = listBox3.Items.Count > pcgdIndex ? listBox3.Items[pcgdIndex].ToString() : string.Empty;
                    string maLop = listBox4.Items.Count > pcgdIndex ? listBox4.Items[pcgdIndex].ToString() : string.Empty;
                    DateTime startDate = dateTimePicker1.Value;
                    DateTime endDate = dateTimePicker2.Value;

                    // Tạo một hàng mới trong bảng dữ liệu PCGD và điền các giá trị tương ứng
                    DataRow newRow = dtPCGD.NewRow();
                    newRow["MaGV"] = maGV;
                    newRow["MaHocPhan"] = tenMon;
                    newRow["MaPhong"] = maPhong;
                    newRow["MaLop"] = maLop;
                    newRow["NgayBatDau"] = startDate;
                    newRow["NgayKetThuc"] = endDate;

                    dtPCGD.Rows.Add(newRow);
                }

                // Đóng kết nối một lần
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

                // Gán bảng dữ liệu PCGD làm nguồn dữ liệu cho dataGridView1
                dataGridView1.DataSource = dtPCGD;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo bảng dữ liệu PCGD: " + ex.Message);
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                // Kết nối đến cơ sở dữ liệu SQL Server
                string connectionString = "Data Source=DESKTOP-LSFEJMC\\SQLEXPRESS; Initial Catalog=QLGD; User Id=sa; Password=123";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Lặp qua các hàng trong dataGridView1 để lưu dữ liệu vào bảng PCGD
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        // Kiểm tra xem hàng có chứa dữ liệu hay không (không phải hàng trống)
                        if (row.IsNewRow)
                        {
                            continue; // Bỏ qua hàng trống
                        }

                        // Lấy các giá trị từ các ô trong hàng
                        string maGV = row.Cells["MaGV"].Value?.ToString();
                        string maHocPhan = row.Cells["MaHocPhan"].Value?.ToString();
                        string maPhong = row.Cells["MaPhong"].Value?.ToString();
                        string maLop = row.Cells["MaLop"].Value?.ToString();
                        DateTime? ngayBatDau = row.Cells["NgayBatDau"].Value as DateTime?;
                        DateTime? ngayKetThuc = row.Cells["NgayKetThuc"].Value as DateTime?;
                        //string soTietDay = row.Cells["TietDay"].Value?.ToString();

                        // Thực hiện câu truy vấn SQL để kiểm tra sự trùng lặp của MaPCGD
                        string checkDuplicateQuery = "SELECT COUNT(*) FROM PCGD WHERE MaPCGD = @MaPCGD";
                        using (SqlCommand checkDuplicateCommand = new SqlCommand(checkDuplicateQuery, connection))
                        {
                            // Khởi tạo giá trị cho MaPCGD (mã tự động)
                            string maPCGD = GeneratePCGDCode(connection);

                            // Nếu MaPCGD không trùng, thêm dữ liệu vào bảng PCGD
                            if (!string.IsNullOrEmpty(maPCGD))
                            {
                                checkDuplicateCommand.Parameters.AddWithValue("@MaPCGD", maPCGD);
                                int duplicateCount = (int)checkDuplicateCommand.ExecuteScalar();

                                while (duplicateCount > 0)
                                {
                                    // Nếu MaPCGD trùng, tạo mã mới và kiểm tra lại
                                    maPCGD = GeneratePCGDCode(connection);
                                    checkDuplicateCommand.Parameters["@MaPCGD"].Value = maPCGD;
                                    duplicateCount = (int)checkDuplicateCommand.ExecuteScalar();
                                }

                                // Thực hiện câu truy vấn SQL để chèn dữ liệu vào bảng PCGD
                                string insertQuery = "INSERT INTO PCGD (MaPCGD, MaGV, MaHocPhan, MaPhong, MaLop, NgayBatDau, NgayKetThuc) " +
                                                   "VALUES (@MaPCGD, @MaGV, @MaHocPhan, @MaPhong, @MaLop, @NgayBatDau, @NgayKetThuc)";

                                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@MaPCGD", maPCGD);
                                    insertCommand.Parameters.AddWithValue("@MaGV", maGV);
                                    insertCommand.Parameters.AddWithValue("@MaHocPhan", maHocPhan);
                                    insertCommand.Parameters.AddWithValue("@MaPhong", maPhong);
                                    insertCommand.Parameters.AddWithValue("@MaLop", maLop);
                                    insertCommand.Parameters.AddWithValue("@NgayBatDau", ngayBatDau);
                                    insertCommand.Parameters.AddWithValue("@NgayKetThuc", ngayKetThuc);
                                    //insertCommand.Parameters.AddWithValue("@TietDay", soTietDay);

                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    connection.Close();
                }

                MessageBox.Show("Lưu dữ liệu thành công vào bảng PCGD trong SQL Server.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu vào bảng PCGD: " + ex.Message);
            }
        }
        private string GeneratePCGDCode(SqlConnection connection)
        {
            try
            {
                Random random = new Random();
                // Generate a unique code based on a GUID and a random number
                string newPCGDCode = "PCGD" + random.Next(001, 999).ToString();

                // Check if the generated code already exists in the PCGD table
                string checkDuplicateQuery = "SELECT COUNT(*) FROM PCGD WHERE MaPCGD = @MaPCGD";
                using (SqlCommand checkDuplicateCommand = new SqlCommand(checkDuplicateQuery, connection))
                {
                    checkDuplicateCommand.Parameters.AddWithValue("@MaPCGD", newPCGDCode);
                    int duplicateCount = (int)checkDuplicateCommand.ExecuteScalar();

                    // If the code already exists or exceeds the length, generate a new code
                    while (duplicateCount > 0 || newPCGDCode.Length >= 30)
                    {
                        newPCGDCode = "PCGD" + random.Next(001, 999).ToString();
                        checkDuplicateCommand.Parameters["@MaPCGD"].Value = newPCGDCode;
                        duplicateCount = (int)checkDuplicateCommand.ExecuteScalar();
                    }
                }

                return newPCGDCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating PCGD code: " + ex.Message);
                return string.Empty;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    dataGridView1.Rows.Remove(selectedRow);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một dòng trong dataGridView1 để xóa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dòng: " + ex.Message);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
                listBox4.Items.Clear();
                dataGridView1.DataSource = null;
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;

                selectedItems.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi reset: " + ex.Message);
            }
        }
        private string GetMaToMonFromHSGV(string maGV)
        {
            try
            {
                // Thực hiện câu truy vấn SQL để lấy MaToMon từ bảng HSGV
                string query = "SELECT MaToMon FROM HSGV WHERE MaGV = @MaGV";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaGV", maGV);

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["MaToMon"].ToString();
                        }
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting MaToMon from HSGV: " + ex.Message);
                return string.Empty;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Lưu trữ thứ tự ban đầu của MaGV và MaHocPhan trước khi trộn
                Dictionary<string, string> originalMaGV_MaHocPhanMapping = GetOriginalMaGV_MaHocPhanMapping();

                // Trộn thứ tự các cột trong DataGridView1
                ShuffleDataGridViewColumns();

                // Cập nhật lại nguồn dữ liệu
                dataGridView1.DataSource = dataGridView1.DataSource;

                // Ưu tiên sắp xếp MaGV và MaHocPhan dựa trên thông tin từ bảng HSGV
                PrioritizeAssignments(originalMaGV_MaHocPhanMapping);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi trộn thứ tự cột: " + ex.Message);
            }
        }

        private Dictionary<string, string> GetOriginalMaGV_MaHocPhanMapping()
        {
            Dictionary<string, string> originalMapping = new Dictionary<string, string>();

            // Lặp qua từng dòng trong DataGridView1 để lưu trữ thông tin ban đầu
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string maGV = row.Cells["MaGV"].Value?.ToString();
                    string maHocPhan = row.Cells["MaHocPhan"].Value?.ToString();

                    if (!string.IsNullOrEmpty(maGV) && !string.IsNullOrEmpty(maHocPhan))
                    {
                        originalMapping[maGV] = maHocPhan;
                    }
                }
            }

            return originalMapping;
        }

        private void PrioritizeAssignments(Dictionary<string, string> originalMapping)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string maGV = row.Cells["MaGV"].Value?.ToString();
                    string maHocPhan = row.Cells["MaHocPhan"].Value?.ToString();

                    if (!string.IsNullOrEmpty(maGV) && !string.IsNullOrEmpty(maHocPhan))
                    {
                        // Kiểm tra xem có hàng tương ứng trong bảng HSGV không
                        bool isMatchInHSGV = IsMatchInHSGV(maGV, maHocPhan);

                        if (isMatchInHSGV)
                        {
                            // Nếu có, ưu tiên gán giá trị từ HSGV
                            string maToMonInHSGV = GetMaToMonFromHSGV(maGV);
                            row.Cells["MaHocPhan"].Value = maToMonInHSGV;
                        }
                        // Ngược lại, giữ nguyên giá trị từ DataGridView
                    }
                }
            }
        }

        private bool IsMatchInHSGV(string maGV, string maHocPhan)
        {
            try
            {
                // Kiểm tra xem có hàng tương ứng trong bảng HSGV không
                string query = "SELECT COUNT(*) FROM HSGV WHERE MaGV = @MaGV AND MaToMon = @MaHocPhan";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaGV", maGV);
                    command.Parameters.AddWithValue("@MaHocPhan", maHocPhan);

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    int rowCount = (int)command.ExecuteScalar();
                    return rowCount > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra sự phù hợp trong HSGV: " + ex.Message);
                return false;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void ShuffleDataGridViewColumns()
        {
            Random random = new Random();

            // Lưu các giá trị của MaGV và MaHocPhan tương ứng để đảm bảo chúng không bị trộn lẫn
            Dictionary<string, string> maGV_MaHocPhanMapping = new Dictionary<string, string>();

            // Lặp qua từng cột trong DataGridView1
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                List<object> values = new List<object>();

                // Lấy giá trị của từng ô trong cột và lưu vào danh sách
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        values.Add(row.Cells[column.Index].Value);

                        // Nếu là cột "MaGV" hoặc "MaHocPhan", lưu giá trị tương ứng vào mapping
                        if (column.Name == "MaGV" || column.Name == "MaHocPhan")
                        {
                            string maGV = row.Cells["MaGV"].Value?.ToString();
                            string maHocPhan = row.Cells["MaHocPhan"].Value?.ToString();

                            if (!string.IsNullOrEmpty(maGV) && !string.IsNullOrEmpty(maHocPhan))
                            {
                                maGV_MaHocPhanMapping[maGV] = maHocPhan;
                            }
                        }
                    }
                }

                // Nếu là cột "MaGV" hoặc "MaHocPhan", trộn lẫn các giá trị để đảm bảo thứ tự ngẫu nhiên
                if (column.Name == "MaGV" || column.Name == "MaHocPhan")
                {
                    values = values.OrderBy(x => random.Next()).ToList();
                }

                // Cập nhật lại giá trị cho từng ô trong cột
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (!dataGridView1.Rows[i].IsNewRow)
                    {
                        dataGridView1.Rows[i].Cells[column.Index].Value = values[i];
                    }
                }
            }

            // Đảm bảo rằng MaGV trong bảng HSGV có maToMon trùng với MaHocPhan
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    string maGV = row.Cells["MaGV"].Value?.ToString();
                    string maHocPhan = row.Cells["MaHocPhan"].Value?.ToString();

                    if (!string.IsNullOrEmpty(maGV) && !string.IsNullOrEmpty(maHocPhan))
                    {
                        // Lấy maToMon từ bảng HSGV dựa trên MaGV
                        string maToMonInHSGV = GetMaToMonFromHSGV(maGV);

                        // Nếu MaToMon không trùng với MaHocPhan, tìm mã môn khác và cập nhật
                        if (!string.IsNullOrEmpty(maToMonInHSGV) && maToMonInHSGV != maHocPhan)
                        {
                            // Tìm mã môn khác
                            string newMaHocPhan = maToMonInHSGV;

                            // Cập nhật giá trị MaHocPhan
                            row.Cells["MaHocPhan"].Value = newMaHocPhan;
                        }
                    }
                }
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Data.SqlClient;

namespace QuanLiKho
{
    public partial class Inteface : Form
    {
        string connectionString = @"Data Source=PERSON;Initial Catalog=QLKhoHangCoffee;Integrated Security=True";
        public DataTable LoadDataFromSQL(string sqlQuery)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }

                connection.Close();
            }

            return dataTable;
        }
        public Inteface()
        {
            InitializeComponent();
        }

       
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Inteface_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLKhoHangCoffeeDataSet.NhaCungCap' table. You can move, or remove it, as needed.
            this.nhaCungCapTableAdapter.Fill(this.qLKhoHangCoffeeDataSet.NhaCungCap);
            // TODO: This line of code loads data into the 'qLKhoHangCoffeeLoaiHangDataSet.LoaiHang' table. You can move, or remove it, as needed.
            this.loaiHangTableAdapter.Fill(this.qLKhoHangCoffeeLoaiHangDataSet.LoaiHang);
            // TODO: This line of code loads data into the 'qLKhoHangCoffeeLoaiHangDataSet.LoaiHang' table. You can move, or remove it, as needed.
            this.loaiHangTableAdapter.Fill(this.qLKhoHangCoffeeLoaiHangDataSet.LoaiHang);
            // TODO: This line of code loads data into the 'qLXDDataSet.LoaiHang' table. You can move, or remove it, as needed.

            LoadDataToDataGridView(dtg_HH,"HangHoa");
            LoadDataToDataGridView(dtV_NV, "NhanVien");
            LoadDataToDataGridView(dtV_NCC, "NhaCungCap");
            LoadDataToDataGridView(dtV_LoaiHang, "LoaiHang");
            LoadDataToDataGridView(dGV_Nhap, "HoaDonNhap");
            LoadDataToDataGridView(dGV_LoHang, "LOHANG");
            LoadDataToDataGridView(dGV_Xuat, "PhieuXuat");

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void bnt_Exit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void bnt_Exit_HH_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void bnt_Exit_NCC_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void bnt_Exit_Nhap_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btn_Exit_Nhap_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btn_Exit_LoHang_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btn_Exit_Xuat_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
        private void LoadDataToDataGridView(DataGridView dtg, string table)
        {
            // Assuming you have a connection string named "ConnectionString" in your app.config or web.config fi
            string connectionString = @"Data Source=PERSON;Initial Catalog=QLKhoHangCoffee;Integrated Security=True";

            // Create a new SqlConnection using the connection string
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand($"SELECT * FROM {table}", connection))
                {
                    // Create a SqlDataAdapter to fill the DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {

                        DataTable dataTable = new DataTable();

                        // Fill the DataTable with the data from the database
                        adapter.Fill(dataTable);

                        // Set the DataTable as the DataSource for the dtg_HH DataGridView
                        dtg.DataSource = dataTable;
                    }
                }
            }
        }

        private void RefreshDataGrid(DataGridView dtg)
        {
            LoadDataToDataGridView(dtg, "HangHoa");
            dtg.Update();
            
            //Other updates
        }

        private void cBox_LoaiHang_HH_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void bnt_Add_HH_Click(object sender, EventArgs e)
        {
            
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = $"insert into HangHoa(MaHH,MaLoaiHang,TenHH,DonViTinh) values (@MaHH, @MaLoaiHang, @TenHH, @DonViTinh)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MaHH", txt_MaHH.Text);
                command.Parameters.AddWithValue("@MaLoaiHang", cBox_LoaiHang_HH.Text);
                command.Parameters.AddWithValue("@TenHH", txt_TenHH.Text);
                command.Parameters.AddWithValue("@DonViTinh", txt_DonVi.Text);

                command.ExecuteNonQuery();
           
            }
            connection.Close();
            MessageBox.Show("them thanh cong");
            RefreshDataGrid(dtg_HH);
        }

        private void dtg_HH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Trích xuất thông tin về bảng ghi đã chọn từ DataGridView
                DataGridViewRow selectedRow = dtg_HH.Rows[e.RowIndex];

                // Trích xuất giá trị từ các ô trong bảng ghi
                string field1Value = selectedRow.Cells["MaHH"].Value.ToString();
                string field2Value = selectedRow.Cells["MaLoaiHang"].Value.ToString();
                string field3Value = selectedRow.Cells["TenHH"].Value.ToString();
                string field4Value = selectedRow.Cells["DonViTinh"].Value.ToString(); // Field1 là tên cột trong bảng


                // ...

                // Gán giá trị vào các TextBox
                txt_MaHH.Text = field1Value;//
                txt_TenHH.Text = field3Value;
                txt_DonVi.Text = field4Value;//
                cBox_LoaiHang_HH.Text = field2Value;//



            }

        }

        private void bnt_Del_NCC_Click(object sender, EventArgs e)
        {

        }

        private void bnt_Del_HH_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = $"Delete from HangHoa where MaHH = @MaHH";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MaHH", txt_MaHH.Text);
                

                command.ExecuteNonQuery();

            }
            connection.Close();
            MessageBox.Show("them thanh cong");
            RefreshDataGrid(dtg_HH);

        }

        private void btn_Tra_HH_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = $"select * from HangHoa where MaHH = @MaHH";
            DataTable dataTable = new DataTable();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MaHH", txt_MaHH_search.Text);
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }

                //command.ExecuteNonQuery();

            }
            connection.Close();

            dGV_HangHoa_search.DataSource = dataTable;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dtV_NV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dGV_Xuat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Trích xuất thông tin về bảng ghi đã chọn từ DataGridView
                DataGridViewRow selectedRow = dGV_Xuat.Rows[e.RowIndex];

                // Trích xuất giá trị từ các ô trong bảng ghi
                string field1Value = selectedRow.Cells["IDHDX"].Value.ToString();
                string field2Value = selectedRow.Cells["MaNV"].Value.ToString();
                string field3Value = selectedRow.Cells["NgayTao"].Value.ToString();



                // ...

                // Gán giá trị vào các TextBox
                txt_MaXuat.Text = field1Value;//
                txt_MaNV_Xuat.Text = field2Value;

                dTP_NgayLap_Xuat.Text = field3Value;//

                lV_Xuat.Columns.Clear(); // Clear previously added columns
                lV_Xuat.Items.Clear(); // Clear previously populated items
                lV_Xuat.View = View.Details;
                lV_Xuat.Columns.Add("IDHDX");
                lV_Xuat.Columns.Add("MaHH");
                lV_Xuat.Columns.Add("TenHH");
                lV_Xuat.Columns.Add("SoLuong");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT IDHDX, ct.MaHH, TenHH, SoLuong FROM ChiTietPhieuXuat ct join HangHoa hh on ct.MaHH = hh.MaHH where IDHDX = '{field1Value}' ";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader[0].ToString()); // Column1
                            item.SubItems.Add(reader[1].ToString()); // Column2
                            item.SubItems.Add(reader[2].ToString()); // Column3
                            item.SubItems.Add(reader[3].ToString());
                            lV_Xuat.Items.Add(item);
                        }
                    }
                }

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL_LTTQ_VIP
{
    public partial class SuaNV : Form
    {
        private string maNV;
        public SuaNV()
        {
            InitializeComponent();
        }

        public SuaNV(string ma, string ten, string gioiTinh, DateTime ngaySinh, string dienThoai, string diaChi, string maCV)
        {
            InitializeComponent();
            this.maNV = ma;

            // Hiển thị thông tin vào các ô nhập liệu
            MaNV.Text = maNV;
            tenNV.Text = ten;
            Ngaysinh.Value = ngaySinh;
            Dienthoai.Text = dienThoai;
            Diachi.Text = diaChi;

            // Hiển thị công việc và giới tính trong ComboBox
            LoadCongViec(maCV);
            LoadGioiTinh(gioiTinh);
        }

        private void LoadCongViec(string maCV)
        {
            using (SqlConnection connection = new SqlConnection(databaselink.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT MaCV, TenCV FROM CongViec";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        CongViec.Items.Add(new { MaCV = reader["MaCV"], TenCV = reader["TenCV"].ToString() });
                    }

                    CongViec.DisplayMember = "TenCV";
                    CongViec.ValueMember = "MaCV";

                    // Đặt công việc đã chọn theo MaCV
                    foreach (var item in CongViec.Items)
                    {
                        if (((dynamic)item).MaCV.ToString() == maCV)
                        {
                            CongViec.SelectedItem = item;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy danh sách công việc: " + ex.Message);
                }
            }
        }

        private void LoadGioiTinh(string gioiTinh)
        {
            Sex.Items.Add("Nam");
            Sex.Items.Add("Nữ");

            if (gioiTinh == "Nam")
            {
                Sex.SelectedIndex = 0;
            }
            else if (gioiTinh == "Nữ")
            {
                Sex.SelectedIndex = 1;
            }
        }
        private void Xacnhan_Click(object sender, EventArgs e)
        {
            // Kiểm tra số điện thoại hợp lệ
            if (!Regex.IsMatch(Dienthoai.Text, @"^0\d{9}$"))
            {
                MessageBox.Show("Số điện thoại phải có 10 số và bắt đầu bằng số 0.");
                return;
            }

            DateTime ngaySinh = Ngaysinh.Value;
            int tuoi = DateTime.Now.Year - ngaySinh.Year;
            if (ngaySinh > DateTime.Now.AddYears(-tuoi)) tuoi--; // Kiểm tra nếu ngày sinh chưa qua trong năm nay

            if (tuoi < 18)
            {
                MessageBox.Show("Nhân viên phải đủ 18 tuổi trở lên.");
                return;
            }

            string gioiTinh = Sex.SelectedItem.ToString();
            int maCongViec = (int)((dynamic)CongViec.SelectedItem).MaCV;

            using (SqlConnection connection = new SqlConnection(databaselink.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE NhanVien SET TenNV=@TenNV, GioiTinh=@GioiTinh, NgaySinh=@NgaySinh, DienThoai=@DienThoai, DiaChi=@DiaChi, MaCV=@MaCV WHERE MaNV=@MaNV";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaNV", maNV);
                        command.Parameters.AddWithValue("@TenNV", tenNV.Text);
                        command.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                        command.Parameters.AddWithValue("@NgaySinh", Ngaysinh.Value);
                        command.Parameters.AddWithValue("@DienThoai", Dienthoai.Text);
                        command.Parameters.AddWithValue("@DiaChi", Diachi.Text);
                        command.Parameters.AddWithValue("@MaCV", maCongViec);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Sửa thông tin nhân viên thành công!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa thông tin nhân viên: " + ex.Message);
                }
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CongViec_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void layma_Click(object sender, EventArgs e)
        {

        }
    }
}

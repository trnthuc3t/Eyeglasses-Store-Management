using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BTL_LTTQ_VIP
{
	public partial class ThemChiTietHoaDonNhap : Form
	{
		private bool isEditMode = false;
		private string editSoHDN = "";
		private int editMaHang = 0;

		public ThemChiTietHoaDonNhap()
		{
			InitializeComponent();
			this.Text = "Thêm Chi Tiết Hóa Đơn Nhập";
			txtThanhTien.ReadOnly = true; // Đảm bảo txtThanhTien chỉ được hiển thị, không cho phép chỉnh sửa
			txtSoLuong.TextChanged += (s, e) => CalculateThanhTien();
			txtDonGia.TextChanged += (s, e) => CalculateThanhTien();
		}

		// Constructor cho chế độ chỉnh sửa
		public ThemChiTietHoaDonNhap(bool isEditMode, string soHDN, int maHang, int soLuong, decimal donGia, decimal giamGia, decimal thanhTien, DateTime ngayNhap, int maNV)
		{
			InitializeComponent();

			this.isEditMode = isEditMode;
			this.editSoHDN = soHDN;
			this.editMaHang = maHang;

			if (isEditMode)
			{
				this.Text = "Chỉnh Sửa Chi Tiết Hóa Đơn Nhập";

				// Điền thông tin vào các trường nhập liệu
				txtSoHDN.Text = soHDN;
				txtMaHang.Text = maHang.ToString();
				txtSoLuong.Text = soLuong.ToString();
				txtDonGia.Text = donGia.ToString();
				txtGiamGia.Text = giamGia.ToString();
				txtThanhTien.Text = thanhTien.ToString();
				dateTimePickerNgayNhap.Value = ngayNhap; // Thiết lập giá trị cho DateTimePicker
				txtMaNV.Text = maNV.ToString(); // Hiển thị mã nhân viên

				// Không cho phép thay đổi các trường khóa chính khi sửa
				txtSoHDN.Enabled = false;
				txtMaHang.Enabled = false;
			}
			else
			{
				this.Text = "Thêm Chi Tiết Hóa Đơn Nhập";
			}
			txtThanhTien.ReadOnly = true; // Đảm bảo txtThanhTien chỉ được hiển thị, không cho phép chỉnh sửa
			txtSoLuong.TextChanged += (s, e) => CalculateThanhTien();
			txtDonGia.TextChanged += (s, e) => CalculateThanhTien();
		}

		// Xác nhận dữ liệu đầu vào
		private bool XacNhanDuLieu()
		{
			if (!int.TryParse(txtSoHDN.Text, out _))
			{
				MessageBox.Show("Số Hóa Đơn Nhập phải là số nguyên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			if (!int.TryParse(txtMaHang.Text, out _))
			{
				MessageBox.Show("Mã Hàng phải là số nguyên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			if (!int.TryParse(txtSoLuong.Text, out _))
			{
				MessageBox.Show("Số Lượng phải là số nguyên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			if (!int.TryParse(txtMaNV.Text, out _))
			{
				MessageBox.Show("Mã Nhân Viên phải là số nguyên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			if (!decimal.TryParse(txtDonGia.Text, out _))
			{
				MessageBox.Show("Đơn Giá phải là số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			if (!decimal.TryParse(txtGiamGia.Text, out _))
			{
				MessageBox.Show("Giảm Giá phải là số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			if (!decimal.TryParse(txtThanhTien.Text, out _))
			{
				MessageBox.Show("Thành Tiền phải là số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		// Tính toán Thành Tiền
		private void CalculateThanhTien()
		{
			if (decimal.TryParse(txtSoLuong.Text, out decimal soLuong) &&
				decimal.TryParse(txtDonGia.Text, out decimal donGia))
			{
				decimal thanhTien = soLuong * donGia;
				txtThanhTien.Text = thanhTien.ToString("0.00");
			}
		}

		private void SaveData()
		{
			if (!XacNhanDuLieu())
			{
				return;
			}

			string soHDN = txtSoHDN.Text;
			int maHang = int.Parse(txtMaHang.Text);
			int soLuong = int.Parse(txtSoLuong.Text);
			decimal donGia = decimal.Parse(txtDonGia.Text);
			decimal giamGia = decimal.Parse(txtGiamGia.Text);
			decimal thanhTien = decimal.Parse(txtThanhTien.Text);
			int maNV = int.Parse(txtMaNV.Text);
			DateTime ngayNhap = dateTimePickerNgayNhap.Value;
			int maNCC = int.Parse(txtMaNCC.Text);

			using (SqlConnection connection = new SqlConnection(databaselink.ConnectionString))
			{
				try
				{
					connection.Open();
					SqlTransaction transaction = connection.BeginTransaction(); // Bắt đầu giao dịch

					// Kiểm tra xem MaHang có tồn tại trong bảng DanhMucHangHoa
					string checkMaHangQuery = "SELECT COUNT(*) FROM DanhMucHangHoa WHERE MaHang = @MaHang";
					SqlCommand checkMaHangCommand = new SqlCommand(checkMaHangQuery, connection, transaction);
					checkMaHangCommand.Parameters.AddWithValue("@MaHang", maHang);
					int maHangExists = (int)checkMaHangCommand.ExecuteScalar();

					if (maHangExists == 0)
					{
						// Nếu MaHang không tồn tại, thông báo lỗi và hủy bỏ giao dịch
						MessageBox.Show("Mã Hàng không tồn tại. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
						transaction.Rollback();
						return;
					}

					string queryHoaDon;

					// Kiểm tra xem SoHDN có tồn tại trong bảng HoaDonNhap
					string checkHoaDonQuery = "SELECT COUNT(*) FROM HoaDonNhap WHERE SoHDN = @SoHDN";
					SqlCommand checkHoaDonCommand = new SqlCommand(checkHoaDonQuery, connection, transaction);
					checkHoaDonCommand.Parameters.AddWithValue("@SoHDN", soHDN);
					int hoaDonExists = (int)checkHoaDonCommand.ExecuteScalar();

					if (hoaDonExists == 0)
					{
						// Nếu hóa đơn nhập chưa tồn tại, tạo mới trong HoaDonNhap
						queryHoaDon = "INSERT INTO HoaDonNhap (SoHDN, MaNV, NgayNhap, MaNCC, TongTien) VALUES (@SoHDN, @MaNV, @NgayNhap, @MaNCC, 0)";
					}
					else
					{
						// Nếu đã tồn tại, cập nhật HoaDonNhap
						queryHoaDon = "UPDATE HoaDonNhap SET MaNV = @MaNV, NgayNhap = @NgayNhap, MaNCC = @MaNCC WHERE SoHDN = @SoHDN";
					}

					SqlCommand commandHoaDon = new SqlCommand(queryHoaDon, connection, transaction);
					commandHoaDon.Parameters.AddWithValue("@SoHDN", soHDN);
					commandHoaDon.Parameters.AddWithValue("@MaNV", maNV);
					commandHoaDon.Parameters.AddWithValue("@NgayNhap", ngayNhap);
					commandHoaDon.Parameters.AddWithValue("@MaNCC", maNCC);
					commandHoaDon.ExecuteNonQuery();

					// Cập nhật hoặc thêm mới ChiTietHoaDonNhap
					string queryChiTiet;
					if (isEditMode)
					{
						queryChiTiet = "UPDATE ChiTietHoaDonNhap SET SoLuong = @SoLuong, DonGia = @DonGia, GiamGia = @GiamGia, ThanhTien = @ThanhTien WHERE SoHDN = @SoHDN AND MaHang = @MaHang";
					}
					else
					{
						queryChiTiet = "INSERT INTO ChiTietHoaDonNhap (SoHDN, MaHang, SoLuong, DonGia, GiamGia, ThanhTien) VALUES (@SoHDN, @MaHang, @SoLuong, @DonGia, @GiamGia, @ThanhTien)";
					}

					SqlCommand commandChiTiet = new SqlCommand(queryChiTiet, connection, transaction);
					commandChiTiet.Parameters.AddWithValue("@SoHDN", soHDN);
					commandChiTiet.Parameters.AddWithValue("@MaHang", maHang);
					commandChiTiet.Parameters.AddWithValue("@SoLuong", soLuong);
					commandChiTiet.Parameters.AddWithValue("@DonGia", donGia);
					commandChiTiet.Parameters.AddWithValue("@GiamGia", giamGia);
					commandChiTiet.Parameters.AddWithValue("@ThanhTien", thanhTien);
					commandChiTiet.ExecuteNonQuery();

					// Xác nhận giao dịch
					transaction.Commit();
					MessageBox.Show(isEditMode ? "Chỉnh sửa chi tiết hóa đơn nhập thành công." : "Thêm chi tiết hóa đơn nhập thành công.");
					this.Close();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message);
				}
			}
		}

		// Xóa chi tiết hóa đơn nhập
		private void DeleteData()
		{
			if (!isEditMode)
			{
				MessageBox.Show("Chỉ có thể xóa trong chế độ chỉnh sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			using (SqlConnection connection = new SqlConnection(databaselink.ConnectionString))
			{
				try
				{
					connection.Open();
					string query = "DELETE FROM ChiTietHoaDonNhap WHERE SoHDN = @SoHDN AND MaHang = @MaHang";

					SqlCommand command = new SqlCommand(query, connection);
					command.Parameters.AddWithValue("@SoHDN", editSoHDN);
					command.Parameters.AddWithValue("@MaHang", editMaHang);

					int result = command.ExecuteNonQuery();
					if (result > 0)
					{
						MessageBox.Show("Xóa chi tiết hóa đơn nhập thành công.");
						this.Close();
					}
					else
					{
						MessageBox.Show("Xóa chi tiết hóa đơn nhập thất bại.");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message);
				}
			}
		}

		// Sự kiện nút xác nhận
		private void btnXacNhan_Click(object sender, EventArgs e)
		{
			SaveData();
			QuanLyHoaDonNhap quanLyHoaDonNhap= new QuanLyHoaDonNhap();
			quanLyHoaDonNhap.ShowDialog();

		}

		// Sự kiện nút xóa
		private void btnXoa_Click(object sender, EventArgs e)
		{
			DeleteData();
		}

		// Sự kiện nút hủy
		private void btnBack_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab5
{
    public partial class F1DKKH : Form
    {
        class KhoaHoc
        {
            public string TenKhoaHoc { get; set; }
            public decimal HocPhi { get; set; }
            public KhoaHoc(string tenKhoaHoc, decimal hocPhi)
            {
                TenKhoaHoc = tenKhoaHoc;
                HocPhi = hocPhi;
            }
            public override string ToString()
            {
                return TenKhoaHoc;
            }
        }
        public F1DKKH()
        {
            InitializeComponent();
            this.Load += F1DKKH_Load;
            txtSoDienThoai.KeyPress += txtSoDienThoai_KeyPress;

            cboKhoaHoc.SelectedIndexChanged += cboKhoaHoc_SelectedIndexChanged;
            numSoThang.ValueChanged += numSoThang_ValueChanged;
        }

        private void F1DKKH_Load(object sender, EventArgs e)
        {
            cboKhoaHoc.Items.Clear();

            cboKhoaHoc.Items.Add(new KhoaHoc("C# WinForms cơ bản", 800000));
            cboKhoaHoc.Items.Add(new KhoaHoc("SQL Server cơ bản", 700000));
            cboKhoaHoc.Items.Add(new KhoaHoc("Web Frontend cơ bản", 750000));
            cboKhoaHoc.Items.Add(new KhoaHoc("Lập trình Python cơ bản", 650000));


            cboKhoaHoc.SelectedIndex = 0; 
            
            radOnline.Checked = true;

            numSoThang.Minimum = 1;
            numSoThang.Maximum = 12;
            numSoThang.Value = 1;

            dtpNgaySinh.Value = DateTime.Today;
            chkNhanEmail.Checked = false;

            CapNhatHocPhi();
        }
        private decimal TinhHocPhi()
        {
            if (cboKhoaHoc.SelectedItem is KhoaHoc khoaHoc)
            {
                decimal hocPhi = khoaHoc.HocPhi;
                int soThang = (int)numSoThang.Value;
                decimal tongTien = hocPhi * soThang;
                if (radOffline.Checked)
                {
                    tongTien += 100000; 
                }
                return tongTien;
            }
            return 0;
        }
        private void CapNhatHocPhi()
        {
            decimal tongTien = TinhHocPhi();
            lblHocPhi.Text = tongTien.ToString("N0") + "VNĐ";
            
        }
        private void cboKhoaHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            CapNhatHocPhi();
        }
        private void numSoThang_ValueChanged(object sender, EventArgs e)
        {
            CapNhatHocPhi();
        }


        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            string soDienThoai =
                txtSoDienThoai.Text.Trim();
            string email = txtEmail.Text.Trim();
            
            if (hoTen == "")
            {
                MessageBox.Show(
                    "Vui lòng nhập họ tên!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtHoTen.Focus();
                return;
            }

            
            if (soDienThoai == "")
            {
                MessageBox.Show(
                    "Vui lòng nhập số điện thoại!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtSoDienThoai.Focus();
                return;
            }
            
            if(email == "")
            {
                MessageBox.Show(
                    "Vui lòng nhập email!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }


            if (cboKhoaHoc.SelectedItem == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn khóa học!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cboKhoaHoc.Focus();
                return;
            }

            KhoaHoc kh =
                (KhoaHoc)cboKhoaHoc.SelectedItem;

            string hinhThuc =
                radOnline.Checked ? "Online" : "Offline";

            decimal tongTien = TinhHocPhi();

            string trangThaiEmail =
                chkNhanEmail.Checked
                ? "Có nhận email"
                : "Không nhận email";


            string phieuDangKy =
     "ĐĂNG KÝ THÀNH CÔNG\n\n" +
     "Họ tên: " + hoTen + "\n" +
     "Số điện thoại: " + soDienThoai + "\n" +
     "Ngày sinh: " + dtpNgaySinh.Value.ToString("dd/MM/yyyy") + "\n" +
     "Khóa học: " + cboKhoaHoc.SelectedItem.ToString() + "\n" +
     "Hình thức học: " +
         (radOnline.Checked ? "Online" : "Offline") + "\n" +
     "Số tháng đăng ký: " + numSoThang.Value + "\n" +
     "Tổng học phí: " + TinhHocPhi().ToString("N0") + " VNĐ\n" +
     "Nhận email: " + (chkNhanEmail.Checked ? "Có" : "Không") 
     ;

            MessageBox.Show(
                phieuDangKy,
                "ĐĂNG KÝ THÀNH CÔNG",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        private void txtSoDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();
            txtSoDienThoai.Clear();

            dtpNgaySinh.Value = DateTime.Today;
            chkNhanEmail.Checked = false;

            if (cboKhoaHoc.Items.Count > 0)
                cboKhoaHoc.SelectedIndex = 0;

            radOnline.Checked = true;
            numSoThang.Value = 1;

            CapNhatHocPhi();
            txtHoTen.Focus();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult ketQua = MessageBox.Show(
                 "Bạn có chắc chắn muốn thoát không?",
                 "Xác nhận",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question);

            if (ketQua == DialogResult.Yes)
            {
                this.Close();
            }
        }

    }
}

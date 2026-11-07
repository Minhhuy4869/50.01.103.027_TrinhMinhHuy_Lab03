using Lab03_QuanLySinhVien;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Lab03_QuanLySinhVien
{
    class Program
    {
        static QuanLySinhVien qlsv = new QuanLySinhVien();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool dangChay = true;
            while (dangChay)
            {
                HienThiMenu();
                string luaChon = Console.ReadLine()?.Trim();
                Console.WriteLine();

                switch (luaChon)
                {
                    case "1":
                        ChucNangThemSinhVien();
                        break;
                    case "2":
                        Console.WriteLine("--- DANH SÁCH TOÀN BỘ SINH VIÊN ---");
                        InDanhSach(qlsv.LayDanhSach());
                        break;
                    case "3":
                        ChucNangTimTheoMa();
                        break;
                    case "4":
                        ChucNangTimTheoTen();
                        break;
                    case "5":
                        ChucNangSuaDiem();
                        break;
                    case "6":
                        ChucNangXoaSinhVien();
                        break;
                    case "7":
                        Console.WriteLine("--- DANH SÁCH SẮP XẾP THEO ĐIỂM GIẢM DẦN ---");
                        InDanhSach(qlsv.SapXepTheoDiem());
                        break;
                    case "8":
                        Console.WriteLine("--- DANH SÁCH SINH VIÊN ĐẠT (ĐTB >= 5.0) ---");
                        InDanhSach(qlsv.LocSinhVienDat());
                        break;
                    case "0":
                        dangChay = false;
                        Console.WriteLine("Đã thoát chương trình.");
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ! Vui lòng chọn từ 0 đến 8.");
                        break;
                }

                if (dangChay)
                {
                    Console.WriteLine("\nNhấn phím bất kỳ để tiếp tục...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void HienThiMenu()
        {
            Console.WriteLine("===== QUAN LY SINH VIEN =====");
            Console.WriteLine("1. Them sinh vien");
            Console.WriteLine("2. Xuat danh sach");
            Console.WriteLine("3. Tim sinh vien theo ma");
            Console.WriteLine("4. Tim sinh vien theo ten");
            Console.WriteLine("5. Sua diem trung binh");
            Console.WriteLine("6. Xoa sinh vien");
            Console.WriteLine("7. Sap xep theo diem giam dan");
            Console.WriteLine("8. Loc sinh vien dat");
            Console.WriteLine("0. Thoat");
            Console.Write("Chon chuc nang: ");
        }

        static void ChucNangThemSinhVien()
        {
            Console.WriteLine("--- THÊM SINH VIÊN MỚI ---");
            string maSV = NhapChuoiKhongRong("Nhập mã sinh viên: ");
            if (qlsv.TimTheoMa(maSV) != null)
            {
                Console.WriteLine("Lỗi: Mã sinh viên đã tồn tại trong danh sách!");
                return;
            }

            string hoTen = NhapChuoiKhongRong("Nhập họ tên: ");
            DateTime ngaySinh = NhapNgaySinh("Nhập ngày sinh (dd/MM/yyyy): ");
            string maLop = NhapChuoiKhongRong("Nhập mã lớp: ");
            double diemTB = NhapDiem("Nhập điểm trung bình (0 - 10): ");

            SinhVien sv = new SinhVien(maSV, hoTen, ngaySinh, maLop, diemTB);
            if (qlsv.Them(sv))
            {
                Console.WriteLine("Thêm sinh viên thành công!");
            }
        }

        static void ChucNangTimTheoMa()
        {
            string maSV = NhapChuoiKhongRong("Nhập mã sinh viên cần tìm: ");
            SinhVien sv = qlsv.TimTheoMa(maSV);
            if (sv != null)
            {
                Console.WriteLine("Tìm thấy sinh viên:");
                Console.WriteLine(sv.LayThongTin());
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên có mã này.");
            }
        }

        static void ChucNangTimTheoTen()
        {
            string tuKhoa = NhapChuoiKhongRong("Nhập từ khóa họ tên cần tìm: ");
            List<SinhVien> ketQua = qlsv.TimTheoTen(tuKhoa);
            Console.WriteLine($"--- KẾT QUẢ TÌM KIẾM TỪ KHÓA '{tuKhoa}' ---");
            InDanhSach(ketQua);
        }

        static void ChucNangSuaDiem()
        {
            string maSV = NhapChuoiKhongRong("Nhập mã sinh viên cần sửa điểm: ");
            if (qlsv.TimTheoMa(maSV) == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên có mã này.");
                return;
            }

            double diemMoi = NhapDiem("Nhập điểm trung bình mới (0 - 10): ");
            if (qlsv.Sua(maSV, diemMoi))
            {
                Console.WriteLine("Cập nhật điểm trung bình thành công!");
            }
        }

        static void ChucNangXoaSinhVien()
        {
            string maSV = NhapChuoiKhongRong("Nhập mã sinh viên cần xóa: ");
            if (qlsv.Xoa(maSV))
            {
                Console.WriteLine("Đã xóa sinh viên khỏi danh sách.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên có mã này.");
            }
        }

        static void InDanhSach(List<SinhVien> danhSach)
        {
            if (danhSach == null || danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách trống hoặc không có kết quả phù hợp.");
                return;
            }

            foreach (var sv in danhSach)
            {
                Console.WriteLine(sv.LayThongTin());
            }
        }

        static string NhapChuoiKhongRong(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string input = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(input))
                    return input;
                Console.WriteLine("Dữ liệu không được để trống! Vui lòng nhập lại.");
            }
        }

        static DateTime NhapNgaySinh(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string input = Console.ReadLine()?.Trim();
                if (DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngaySinh))
                {
                    return ngaySinh;
                }
                Console.WriteLine("Ngày sinh không đúng định dạng dd/MM/yyyy (Ví dụ: 25/12/2005). Vui lòng nhập lại!");
            }
        }

        static double NhapDiem(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);
                string input = Console.ReadLine()?.Trim().Replace(',', '.');
                if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double diem) && diem >= 0 && diem <= 10)
                {
                    return diem;
                }
                Console.WriteLine("Điểm không hợp lệ! Vui lòng nhập số từ 0 đến 10.");
            }
        }
    }
}
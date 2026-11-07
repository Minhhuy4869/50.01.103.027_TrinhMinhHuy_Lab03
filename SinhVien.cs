using Lab03_QuanLySinhVien;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_QuanLySinhVien
{
    public class SinhVien : Nguoi
    {
        private double _diemTrungBinh;

        public string MaSinhVien 
        { 
            get; 
            set; 
        }
        public string MaLop 
        { 
            get; 
            set; 
        }

        public double DiemTrungBinh
        {
            get => _diemTrungBinh;
            set
            {
                if (value < 0 || value > 10)
                {
                    throw new ArgumentOutOfRangeException(nameof(DiemTrungBinh), "Điểm trung bình phải nằm trong khoảng từ 0 đến 10.");
                }
                _diemTrungBinh = value;
            }
        }

        public SinhVien() { }
        public SinhVien(string maSinhVien, string hoTen, DateTime ngaySinh, string maLop, double diemTrungBinh)
            : base(hoTen, ngaySinh)
        {
            MaSinhVien = maSinhVien;
            MaLop = maLop;
            DiemTrungBinh = diemTrungBinh;
        }

        public string XepLoai()
        {
            if (DiemTrungBinh >= 8.0) return "Giỏi";
            if (DiemTrungBinh >= 6.5) return "Khá";
            if (DiemTrungBinh >= 5.0) return "Trung bình (Đạt)";
            return "Yếu (Không đạt)";
        }

        public override string LayThongTin()
        {
            return $"Mã SV: {MaSinhVien,-8} | {base.LayThongTin()} | Lớp: {MaLop,-8} | ĐTB: {DiemTrungBinh,4:0.0} | Xếp loại: {XepLoai()}";
        }
    }
}
using Lab03_QuanLySinhVien;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab03_QuanLySinhVien
{
    public class QuanLySinhVien
    {
        private readonly List<SinhVien> _danhSachSinhVien = new List<SinhVien>();

        public bool Them(SinhVien sv)
        {
            if (TimTheoMa(sv.MaSinhVien) != null)
            {
                return false;
            }
            _danhSachSinhVien.Add(sv);
            return true;
        }

        public List<SinhVien> LayDanhSach()
        {
            return _danhSachSinhVien;
        }

        public SinhVien TimTheoMa(string maSinhVien)
        {
            return _danhSachSinhVien.FirstOrDefault(sv =>
                sv.MaSinhVien.Equals(maSinhVien, StringComparison.OrdinalIgnoreCase));
        }

        public List<SinhVien> TimTheoTen(string tuKhoa)
        {
            return _danhSachSinhVien
                .Where(sv => sv.HoTen.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public bool Sua(string maSinhVien, double diemMoi)
        {
            SinhVien sv = TimTheoMa(maSinhVien);
            if (sv == null) 
                return false;
            sv.DiemTrungBinh = diemMoi;
            return true;
        }
        public bool Xoa(string maSinhVien)
        {
            SinhVien sv = TimTheoMa(maSinhVien);
            if (sv == null) return false;

            _danhSachSinhVien.Remove(sv);
            return true;
        }

        public List<SinhVien> SapXepTheoDiem()
        {
            return _danhSachSinhVien
                .OrderByDescending(sv => sv.DiemTrungBinh)
                .ToList();
        }

        public List<SinhVien> LocSinhVienDat()
        {
            return _danhSachSinhVien
                .Where(sv => sv.DiemTrungBinh >= 5.0)
                .ToList();
        }
    }
}

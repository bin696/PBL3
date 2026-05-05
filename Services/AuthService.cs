using System;
using PBL3.Models;
using PBL3.Repositories;

namespace PBL3.Services
{
    public class AuthService
    {
        private readonly NhanVienRepository _nhanVienRepository;

        public AuthService()
        {
            _nhanVienRepository = new NhanVienRepository();
        }

        public AuthService(NhanVienRepository nhanVienRepository)
        {
            _nhanVienRepository = nhanVienRepository;
        }

        public NhanVien Authenticate(string sdt, string matKhau)
        {
            if (string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(matKhau))
                return null;

            return _nhanVienRepository.GetNhanVienBySDTAndMatKhau(sdt, matKhau);
        }

        public bool IsAdmin(NhanVien nv)
        {
            if (nv == null || string.IsNullOrEmpty(nv.MaCV))
                return false;

            return nv.MaCV == "6" || nv.MaCV.Equals("CV06", StringComparison.OrdinalIgnoreCase);
        }
    }
}
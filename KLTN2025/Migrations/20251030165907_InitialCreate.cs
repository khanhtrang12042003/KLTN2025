using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KLTN2025.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    NguoiDungID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false),
                    MaKhauHash = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    SDT = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TaoVaoLuc = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NguoiDun__C4BBA4DDE679DE91", x => x.NguoiDungID);
                });

            migrationBuilder.CreateTable(
                name: "GiaSu",
                columns: table => new
                {
                    GiaSuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiDungID = table.Column<int>(type: "int", nullable: false),
                    BangCap = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    KinhNghiem = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    KyNang = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LichRanh = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KhuVucDay = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AnhDaiDien = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true, defaultValue: ""),
                    NgaySinh = table.Column<DateOnly>(type: "date", nullable: false),
                    TungHocTai = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DaiHoc = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "Chua lên d?i h?c"),
                    NganhHoc = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "Chua lên d?i h?c"),
                    NamBatDau = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    NamKetThuc = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    AnhSinhVien = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true, defaultValue: ""),
                    BangTotNghiep = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GiaSu__BE5E202748DD713A", x => x.GiaSuID);
                    table.ForeignKey(
                        name: "FK_NguoiDungID_GiaSu",
                        column: x => x.NguoiDungID,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungID");
                });

            migrationBuilder.CreateTable(
                name: "NhanSu",
                columns: table => new
                {
                    NhanSuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiDungID = table.Column<int>(type: "int", nullable: false),
                    BoPhan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ChucVu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NhanSu__F2329F1BB51D7490", x => x.NhanSuID);
                    table.ForeignKey(
                        name: "FK_NguoiDungID_NhanSu",
                        column: x => x.NguoiDungID,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungID");
                });

            migrationBuilder.CreateTable(
                name: "PhuHuynh",
                columns: table => new
                {
                    PhuHuynhID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiDungID = table.Column<int>(type: "int", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhuHuynh__D0ADD09079B8831A", x => x.PhuHuynhID);
                    table.ForeignKey(
                        name: "FK_NguoiDungID_PhuHuynh",
                        column: x => x.NguoiDungID,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungID");
                });

            migrationBuilder.CreateTable(
                name: "ThongBao",
                columns: table => new
                {
                    ThongBaoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiDungID = table.Column<int>(type: "int", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NgayGui = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ThongBao__6E51A53B0FEA1A6B", x => x.ThongBaoID);
                    table.ForeignKey(
                        name: "FK_NguoiDungID_ThongBao",
                        column: x => x.NguoiDungID,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungID");
                });

            migrationBuilder.CreateTable(
                name: "HoSoGiaSu",
                columns: table => new
                {
                    HoSoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiaSuID = table.Column<int>(type: "int", nullable: false),
                    NgayGui = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())"),
                    TrangThai = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false, defaultValue: "Đang chờ"),
                    NgayDuyet = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    NhanSuID = table.Column<int>(type: "int", nullable: true),
                    TuChoiVi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HoSoGiaS__A9F1AA18CB5B53F2", x => x.HoSoID);
                    table.ForeignKey(
                        name: "FK_GiaSuID_HoSoGiaSu",
                        column: x => x.GiaSuID,
                        principalTable: "GiaSu",
                        principalColumn: "GiaSuID");
                });

            migrationBuilder.CreateTable(
                name: "LopHoc",
                columns: table => new
                {
                    LopHocID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhuHuynhID = table.Column<int>(type: "int", nullable: false),
                    GiaSuID = table.Column<int>(type: "int", nullable: false),
                    NhanSuID = table.Column<int>(type: "int", nullable: false),
                    MonHoc = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    KhoiLop = table.Column<byte>(type: "tinyint", nullable: true),
                    DiaDiem = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SoBuoiHoc = table.Column<byte>(type: "tinyint", nullable: true),
                    LichHoc = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    HocPhi = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Mới"),
                    NgayTao = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LopHoc__DBC489C09B45C547", x => x.LopHocID);
                    table.ForeignKey(
                        name: "FK_PhuHuynhID_LopHoc",
                        column: x => x.PhuHuynhID,
                        principalTable: "PhuHuynh",
                        principalColumn: "PhuHuynhID");
                });

            migrationBuilder.CreateTable(
                name: "PhanCong",
                columns: table => new
                {
                    PhanCongID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LopHocID = table.Column<int>(type: "int", nullable: false),
                    GiaSuID = table.Column<int>(type: "int", nullable: false),
                    NhanSuID = table.Column<int>(type: "int", nullable: false),
                    NgayPhanCong = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhanCong__7EF840DDA0E8DEE5", x => x.PhanCongID);
                    table.ForeignKey(
                        name: "FK_GiaSuID_PhanCong",
                        column: x => x.GiaSuID,
                        principalTable: "GiaSu",
                        principalColumn: "GiaSuID");
                    table.ForeignKey(
                        name: "FK_LopHocID_PhanCong",
                        column: x => x.LopHocID,
                        principalTable: "LopHoc",
                        principalColumn: "LopHocID");
                    table.ForeignKey(
                        name: "FK_NhanSuID_PhanCong",
                        column: x => x.NhanSuID,
                        principalTable: "NhanSu",
                        principalColumn: "NhanSuID");
                });

            migrationBuilder.CreateTable(
                name: "PhanHoi",
                columns: table => new
                {
                    PhanHoiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LopHocID = table.Column<int>(type: "int", nullable: false),
                    NguoiDungID = table.Column<int>(type: "int", nullable: false),
                    LoaiPhanHoi = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiemDanhGia = table.Column<byte>(type: "tinyint", nullable: false),
                    NgayGui = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())"),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Đang chờ")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhanHoi__7480288BB26D542F", x => x.PhanHoiID);
                    table.ForeignKey(
                        name: "FK_LopHocID_PhanHoi",
                        column: x => x.LopHocID,
                        principalTable: "LopHoc",
                        principalColumn: "LopHocID");
                    table.ForeignKey(
                        name: "FK_NguoiDungID_PhanHoi",
                        column: x => x.NguoiDungID,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungID");
                });

            migrationBuilder.CreateTable(
                name: "ThanhToan",
                columns: table => new
                {
                    ThanhToanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiaSuID = table.Column<int>(type: "int", nullable: false),
                    LopHocID = table.Column<int>(type: "int", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    NgayGui = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())"),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Đang chờ")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ThanhToa__24A8D684973C0587", x => x.ThanhToanID);
                    table.ForeignKey(
                        name: "FK_GiaSuID_ThanhToan",
                        column: x => x.GiaSuID,
                        principalTable: "GiaSu",
                        principalColumn: "GiaSuID");
                    table.ForeignKey(
                        name: "FK_LopHocID_ThanhToan",
                        column: x => x.LopHocID,
                        principalTable: "LopHoc",
                        principalColumn: "LopHocID");
                });

            migrationBuilder.CreateTable(
                name: "UngTuyen",
                columns: table => new
                {
                    UngTuyenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiaSuID = table.Column<int>(type: "int", nullable: false),
                    LopHocID = table.Column<int>(type: "int", nullable: false),
                    NgayUngTuyen = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: false, defaultValueSql: "(sysdatetime())"),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Đang chờ")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UngTuyen__4E510296DAF8E723", x => x.UngTuyenID);
                    table.ForeignKey(
                        name: "FK_GiaSuID_UngTuyen",
                        column: x => x.GiaSuID,
                        principalTable: "GiaSu",
                        principalColumn: "GiaSuID");
                    table.ForeignKey(
                        name: "FK_LopHocID_UngTuyen",
                        column: x => x.LopHocID,
                        principalTable: "LopHoc",
                        principalColumn: "LopHocID");
                });

            migrationBuilder.CreateTable(
                name: "PhieuHoTro",
                columns: table => new
                {
                    PhieuHoTroID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhanHoiID = table.Column<int>(type: "int", nullable: false),
                    NhanSuID = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Đang chờ"),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhieuHoT__6A7F0FDCD2C2ECC5", x => x.PhieuHoTroID);
                    table.ForeignKey(
                        name: "FK_NhanSuIID_PhieuHoTro",
                        column: x => x.NhanSuID,
                        principalTable: "NhanSu",
                        principalColumn: "NhanSuID");
                    table.ForeignKey(
                        name: "FK_PhanHoiID_PhieuHoTro",
                        column: x => x.PhanHoiID,
                        principalTable: "PhanHoi",
                        principalColumn: "PhanHoiID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GiaSu_NguoiDungID",
                table: "GiaSu",
                column: "NguoiDungID");

            migrationBuilder.CreateIndex(
                name: "IX_HoSoGiaSu_GiaSuID",
                table: "HoSoGiaSu",
                column: "GiaSuID");

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_PhuHuynhID",
                table: "LopHoc",
                column: "PhuHuynhID");

            migrationBuilder.CreateIndex(
                name: "UQ__NguoiDun__55F68FC067D430D1",
                table: "NguoiDung",
                column: "TenDangNhap",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__NguoiDun__A9D105346EFE860E",
                table: "NguoiDung",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhanSu_NguoiDungID",
                table: "NhanSu",
                column: "NguoiDungID");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCong_GiaSuID",
                table: "PhanCong",
                column: "GiaSuID");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCong_LopHocID",
                table: "PhanCong",
                column: "LopHocID");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCong_NhanSuID",
                table: "PhanCong",
                column: "NhanSuID");

            migrationBuilder.CreateIndex(
                name: "IX_PhanHoi_LopHocID",
                table: "PhanHoi",
                column: "LopHocID");

            migrationBuilder.CreateIndex(
                name: "IX_PhanHoi_NguoiDungID",
                table: "PhanHoi",
                column: "NguoiDungID");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuHoTro_NhanSuID",
                table: "PhieuHoTro",
                column: "NhanSuID");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuHoTro_PhanHoiID",
                table: "PhieuHoTro",
                column: "PhanHoiID");

            migrationBuilder.CreateIndex(
                name: "IX_PhuHuynh_NguoiDungID",
                table: "PhuHuynh",
                column: "NguoiDungID");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToan_GiaSuID",
                table: "ThanhToan",
                column: "GiaSuID");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToan_LopHocID",
                table: "ThanhToan",
                column: "LopHocID");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBao_NguoiDungID",
                table: "ThongBao",
                column: "NguoiDungID");

            migrationBuilder.CreateIndex(
                name: "IX_UngTuyen_GiaSuID",
                table: "UngTuyen",
                column: "GiaSuID");

            migrationBuilder.CreateIndex(
                name: "IX_UngTuyen_LopHocID",
                table: "UngTuyen",
                column: "LopHocID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoSoGiaSu");

            migrationBuilder.DropTable(
                name: "PhanCong");

            migrationBuilder.DropTable(
                name: "PhieuHoTro");

            migrationBuilder.DropTable(
                name: "ThanhToan");

            migrationBuilder.DropTable(
                name: "ThongBao");

            migrationBuilder.DropTable(
                name: "UngTuyen");

            migrationBuilder.DropTable(
                name: "NhanSu");

            migrationBuilder.DropTable(
                name: "PhanHoi");

            migrationBuilder.DropTable(
                name: "GiaSu");

            migrationBuilder.DropTable(
                name: "LopHoc");

            migrationBuilder.DropTable(
                name: "PhuHuynh");

            migrationBuilder.DropTable(
                name: "NguoiDung");
        }
    }
}

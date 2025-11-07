using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KLTN2025.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
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
                    TenDangNhap = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    MaKhauHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    VaiTro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TaoVaoLuc = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSDATETIME())"),
                    DiaChi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.NguoiDungID);
                });

            migrationBuilder.CreateTable(
                name: "GiaSu",
                columns: table => new
                {
                    NguoiDungID = table.Column<int>(type: "int", nullable: false),
                    BangCap = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    KinhNghiem = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    KyNang = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LichRanh = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KhuVucDay = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TrangThai = table.Column<byte>(type: "tinyint", nullable: false, defaultValue: (byte)0),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    NgaySinh = table.Column<DateTime>(type: "date", nullable: false),
                    TungHocTai = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DaiHoc = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "Chưa lên đại học"),
                    NganhHoc = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "Chưa lên đại học"),
                    NamBatDau = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    NamKetThuc = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    AnhSinhVien = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: ""),
                    BangTotNghiep = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaSu", x => x.NguoiDungID);
                    table.ForeignKey(
                        name: "FK_NguoiDungID_GiaSu",
                        column: x => x.NguoiDungID,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NhanSu",
                columns: table => new
                {
                    NguoiDungID = table.Column<int>(type: "int", nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanSu", x => x.NguoiDungID);
                    table.ForeignKey(
                        name: "FK_NguoiDungID_NhanSu",
                        column: x => x.NguoiDungID,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThongBao",
                columns: table => new
                {
                    ThongBaoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false),
                    TieuDe = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NgayGui = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSDATETIME())"),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBao", x => x.ThongBaoID);
                    table.ForeignKey(
                        name: "FK_NguoiDungID_ThongBao",
                        column: x => x.NguoiDungId,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoSoGiaSu",
                columns: table => new
                {
                    HoSoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false),
                    NgayGui = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSDATETIME())"),
                    NgayDuyet = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    NhanSuId = table.Column<int>(type: "int", nullable: true),
                    LyDoTuChoi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoSoGiaSu", x => x.HoSoID);
                    table.ForeignKey(
                        name: "FK_NguoiDungID_HoSoGiaSu",
                        column: x => x.NguoiDungId,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungID");
                    table.ForeignKey(
                        name: "FK_NhanSuID_HoSoGiaSu",
                        column: x => x.NhanSuId,
                        principalTable: "NhanSu",
                        principalColumn: "NguoiDungID");
                });

            migrationBuilder.CreateTable(
                name: "LopHoc",
                columns: table => new
                {
                    LopHocID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhuHuynhId = table.Column<int>(type: "int", nullable: false),
                    GiaSuId = table.Column<int>(type: "int", nullable: false),
                    NhanSuId = table.Column<int>(type: "int", nullable: false),
                    MonHoc = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    KhoiLop = table.Column<byte>(type: "tinyint", nullable: true),
                    DiaDiem = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LichHoc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    HocPhi = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Mới"),
                    NgayTao = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSDATETIME())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHoc", x => x.LopHocID);
                    table.ForeignKey(
                        name: "FK_GiaSuID_LopHoc",
                        column: x => x.GiaSuId,
                        principalTable: "GiaSu",
                        principalColumn: "NguoiDungID");
                    table.ForeignKey(
                        name: "FK_NhanSuID_LopHoc",
                        column: x => x.NhanSuId,
                        principalTable: "NhanSu",
                        principalColumn: "NguoiDungID");
                    table.ForeignKey(
                        name: "FK_PhuHuynhID_LopHoc",
                        column: x => x.PhuHuynhId,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungID");
                });

            migrationBuilder.CreateTable(
                name: "PhanCong",
                columns: table => new
                {
                    PhanCongID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LopHocId = table.Column<int>(type: "int", nullable: false),
                    GiaSuId = table.Column<int>(type: "int", nullable: false),
                    NhanSuId = table.Column<int>(type: "int", nullable: false),
                    NgayPhanCong = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSDATETIME())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanCong", x => x.PhanCongID);
                    table.ForeignKey(
                        name: "FK_GiaSuID_PhanCong",
                        column: x => x.GiaSuId,
                        principalTable: "GiaSu",
                        principalColumn: "NguoiDungID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LopHocID_PhanCong",
                        column: x => x.LopHocId,
                        principalTable: "LopHoc",
                        principalColumn: "LopHocID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhanSuID_PhanCong",
                        column: x => x.NhanSuId,
                        principalTable: "NhanSu",
                        principalColumn: "NguoiDungID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhanHoi",
                columns: table => new
                {
                    PhanHoiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LopHocId = table.Column<int>(type: "int", nullable: false),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false),
                    LoaiPhanHoi = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiemDanhGia = table.Column<byte>(type: "tinyint", nullable: false),
                    NgayGui = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSDATETIME())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanHoi", x => x.PhanHoiID);
                    table.ForeignKey(
                        name: "FK_LopHocID_PhanHoi",
                        column: x => x.LopHocId,
                        principalTable: "LopHoc",
                        principalColumn: "LopHocID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NguoiDungID_PhanHoi",
                        column: x => x.NguoiDungId,
                        principalTable: "NguoiDung",
                        principalColumn: "NguoiDungID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThanhToan",
                columns: table => new
                {
                    ThanhToanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiaSuId = table.Column<int>(type: "int", nullable: false),
                    LopHocId = table.Column<int>(type: "int", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    NgayGui = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSDATETIME())"),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Đang chờ")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhToan", x => x.ThanhToanID);
                    table.ForeignKey(
                        name: "FK_GiaSuID_ThanhToan",
                        column: x => x.GiaSuId,
                        principalTable: "GiaSu",
                        principalColumn: "NguoiDungID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LopHocID_ThanhToan",
                        column: x => x.LopHocId,
                        principalTable: "LopHoc",
                        principalColumn: "LopHocID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UngTuyen",
                columns: table => new
                {
                    UngTuyenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiaSuId = table.Column<int>(type: "int", nullable: false),
                    LopHocId = table.Column<int>(type: "int", nullable: false),
                    NhanSuId = table.Column<int>(type: "int", nullable: false),
                    NgayUngTuyen = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "(SYSDATETIME())"),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Đang chờ"),
                    NgayDuyet = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    LyDoTuChoi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UngTuyen", x => x.UngTuyenID);
                    table.ForeignKey(
                        name: "FK_GiaSuID_UngTuyen",
                        column: x => x.GiaSuId,
                        principalTable: "GiaSu",
                        principalColumn: "NguoiDungID");
                    table.ForeignKey(
                        name: "FK_LopHocID_UngTuyen",
                        column: x => x.LopHocId,
                        principalTable: "LopHoc",
                        principalColumn: "LopHocID");
                    table.ForeignKey(
                        name: "FK_NhanSuID_UngTuyen",
                        column: x => x.NhanSuId,
                        principalTable: "NhanSu",
                        principalColumn: "NguoiDungID");
                });

            migrationBuilder.CreateTable(
                name: "PhieuHoTro",
                columns: table => new
                {
                    PhieuHoTroID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhanHoiId = table.Column<int>(type: "int", nullable: false),
                    NhanSuId = table.Column<int>(type: "int", nullable: false),
                    NgayGiaiQuyet = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuHoTro", x => x.PhieuHoTroID);
                    table.ForeignKey(
                        name: "FK_NhanSuIID_PhieuHoTro",
                        column: x => x.NhanSuId,
                        principalTable: "NhanSu",
                        principalColumn: "NguoiDungID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhanHoiID_PhieuHoTro",
                        column: x => x.PhanHoiId,
                        principalTable: "PhanHoi",
                        principalColumn: "PhanHoiID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HoSoGiaSu_NguoiDungId",
                table: "HoSoGiaSu",
                column: "NguoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_HoSoGiaSu_NhanSuId",
                table: "HoSoGiaSu",
                column: "NhanSuId");

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_GiaSuId",
                table: "LopHoc",
                column: "GiaSuId");

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_NhanSuId",
                table: "LopHoc",
                column: "NhanSuId");

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_PhuHuynhId",
                table: "LopHoc",
                column: "PhuHuynhId");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCong_GiaSuId",
                table: "PhanCong",
                column: "GiaSuId");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCong_LopHocId",
                table: "PhanCong",
                column: "LopHocId");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCong_NhanSuId",
                table: "PhanCong",
                column: "NhanSuId");

            migrationBuilder.CreateIndex(
                name: "IX_PhanHoi_LopHocId",
                table: "PhanHoi",
                column: "LopHocId");

            migrationBuilder.CreateIndex(
                name: "IX_PhanHoi_NguoiDungId",
                table: "PhanHoi",
                column: "NguoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuHoTro_NhanSuId",
                table: "PhieuHoTro",
                column: "NhanSuId");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuHoTro_PhanHoiId",
                table: "PhieuHoTro",
                column: "PhanHoiId");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToan_GiaSuId",
                table: "ThanhToan",
                column: "GiaSuId");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToan_LopHocId",
                table: "ThanhToan",
                column: "LopHocId");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBao_NguoiDungId",
                table: "ThongBao",
                column: "NguoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_UngTuyen_GiaSuId",
                table: "UngTuyen",
                column: "GiaSuId");

            migrationBuilder.CreateIndex(
                name: "IX_UngTuyen_LopHocId",
                table: "UngTuyen",
                column: "LopHocId");

            migrationBuilder.CreateIndex(
                name: "IX_UngTuyen_NhanSuId",
                table: "UngTuyen",
                column: "NhanSuId");
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
                name: "PhanHoi");

            migrationBuilder.DropTable(
                name: "LopHoc");

            migrationBuilder.DropTable(
                name: "GiaSu");

            migrationBuilder.DropTable(
                name: "NhanSu");

            migrationBuilder.DropTable(
                name: "NguoiDung");
        }
    }
}

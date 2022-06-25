using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Exceptions;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Services;
using MISA.Web04.Core.Services;
using MISA.Web04.Infrastructure.Repository;

namespace MISA.Web04.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]

    public class EmployeesController : MISABaseController<Employee>
    {
        IEmployeeRepository _employeeRepository;
        IEmployeeService _employeeService;

        /// <summary>
        /// Thực hiện khởi tạo đối tượng
        /// </summary>
        /// <param name="baseRepository">khởi tạo đối tượng baseRepository</param>
        /// <param name="baseService">khởi tạo đối tượng baseService</param>
        /// CreatedBy: HVDUNG (20/06/2022)
        public EmployeesController(IEmployeeRepository employeeRepository, IEmployeeService employeeService)
            :base(employeeRepository, employeeService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }

        // danh sách cột tiêu đề cho file excel
        private readonly string[] theads = {"STT", "Mã nhân viên", "Tên nhân viên", "Giới tính", "Ngày sinh", "Số CMND",
            "Chức danh", "Tên đơn vị", "Số tài khoản", "Tên ngân hàng", "Chi nhánh TK ngân hàng"};

        /// <summary>
        /// Thực hiện tạo style border 
        /// </summary>
        /// <param name="titleTable"></param>
        /// CreatedBy: HVDUNG (25/06/2022)
        private static void StyleBorder(IXLRange titleTable)
        {
            titleTable.Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);
            titleTable.Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            titleTable.Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
            titleTable.Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        }

        /// <summary>
        /// Thực hiện tạo style cho title
        /// </summary>
        /// <param name="titleTable"></param>
        /// <param name="fontSize"></param>
        /// <param name="fontName"></param>
        /// CreatedBy: HVDUNG (25/06/2022)
        private static void StyleTitle(IXLRange titleTable, int fontSize, string fontName)
        {
            titleTable.Style.Font.Bold = true;
            titleTable.Style.Font.FontSize = fontSize;
            titleTable.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            titleTable.Style.Font.SetFontName(fontName);
        }

       /// <summary>
       /// Thực hiện set độ rộng cho các cột
       /// </summary>
       /// <param name="worksheet"></param>
       /// CreatedBy: HVDUNG (25/06/2022)
        private static void SetColumnWidth(IXLWorksheet worksheet)
        {
            worksheet.Column("A").Width = 4;
            worksheet.Column("B").Width = 15;
            worksheet.Column("C").Width = 28;
            worksheet.Column("D").Width = 9;
            worksheet.Column("E").Width = 13;
            worksheet.Column("F").Width = 19;
            worksheet.Column("G").Width = 25;
            worksheet.Column("H").Width = 25;
            worksheet.Column("I").Width = 19;
            worksheet.Column("J").Width = 25;
            worksheet.Column("K").Width = 31;
        }

        /// <summary>
        /// Thực hiện format lại ngày tháng theo địng dạng (ngày/tháng/năm)
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Ngày tháng năm dưới dạng chuỗi string</returns>
        /// Author: HVDUNG (25/06/2022)
        private static string FormatDate(DateTime date)
        {
            string dd = date.Day < 10 ? "0" + date.Day.ToString() : date.Day.ToString();
            string mm = date.Month < 10 ? "0" + date.Month.ToString() : date.Month.ToString();
            string yyyy = date.Year.ToString();
            return $"{dd}/{mm}/{yyyy}";
        }

        /// <summary>
        /// Thực hiện xuất file excel
        /// </summary>
        /// <returns>file excel</returns>
        /// Author: HVDUNG (25/06/2022)
        [HttpGet("excel")]
        public IActionResult ExportExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                // tạo tên file
                var worksheet = workbook.Worksheets.Add("DANH SÁCH NHÂN VIÊN");

                // định dạng title
                var title = worksheet.Range("A1:K1");
                title.Value = "DANH SÁCH NHÂN VIÊN";
                title.Merge();
                StyleTitle(title, 16, "Arial");

                // tạo ra row rỗng để tạo khoảng cách
                worksheet.Range("A2:K2").Merge();

                // tạo các cột tiêu đề
                var headersGrid = worksheet.Range("A3:K3");
                headersGrid.Style.Fill.BackgroundColor = XLColor.Green;
                StyleBorder(headersGrid);
                StyleTitle(headersGrid, 10, "Arial");
                foreach (var (header, i) in theads.Select((header, i) => (header, i)))
                {
                    worksheet.Cell(3, i + 1).Value = header;
                }

                // Thực hiện lấy data và đưa vào file

                var employees = _employeeRepository.GetAll();
                int currentRow = 4;
                foreach (var (employee, index) in employees.Select((employee, index) => (employee, index)))
                {
                    // thực hiện format date
                    string dateTemp = "";
                    if (employee.DateOfBirth != null)
                    {
                        dateTemp = FormatDate((DateTime)employee.DateOfBirth);
                    }
                    // thực hiện format giới tính
                    string GenderName = "";
                    if (employee.Gender== Core.Enum.Gender.Male)
                    {
                        GenderName = "Nam";
                    }else if(employee.Gender == Core.Enum.Gender.Female)
                    {
                        GenderName = "Nữ";
                    }
                    else
                        GenderName = "Khác";

                    worksheet.Cell(currentRow, 1).Value = index + 1;
                    worksheet.Cell(currentRow, 2).Value = employee.EmployeeCode;
                    worksheet.Cell(currentRow, 3).Value = employee.EmployeeName;
                    worksheet.Cell(currentRow, 4).Value = GenderName;

                    worksheet.Cell(currentRow, 5).Value = dateTemp;

                    worksheet.Cell(currentRow, 6).Value = $"'{employee.IdentityNumber}";
                    worksheet.Cell(currentRow, 7).Value = employee.PositionName;
                    worksheet.Cell(currentRow, 8).Value = employee.DepartmentName;
                    worksheet.Cell(currentRow, 9).Value = $"'{employee.BankAccountNumber}";
                    worksheet.Cell(currentRow, 10).Value = employee.BankName;
                    worksheet.Cell(currentRow, 11).Value = employee.BankBranchName;

                    currentRow++;
                }

                // Định dạng hiển thị dữ liệu
                var rangeData = worksheet.Range($"A4:K{currentRow - 1}");
                StyleBorder(rangeData);
                rangeData.Style.Font.SetFontName("Times New Roman");

                // Đặt độ rộng cho cột
                SetColumnWidth(worksheet);

                // Đặt căn giữa cho ngày sinh
                worksheet.Range($"E4:E{currentRow - 1}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                using (var stream = new MemoryStream())
                {
                    // Thực hiện lưu file
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    // Trả về file cho cliend
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "danhsachnhanvien.xlsx"
                    );

                }
            }
        }


        /// <summary>
        /// Thực hiện phân trang
        /// </summary>
        /// <param name="pageSize">số bản ghi/trang</param>
        /// <param name="pageNumber">trang hiện tại</param>
        /// <param name="employeeFilter">thông tin tìm kiếm</param>
        /// <returns>danh sách nhân viên</returns>
        /// CreatedBy: HVDUNG (20/06/2022)
        [HttpGet("/api/v1/[controller]/filter")]
        public IActionResult employeeFilter(int pageSize, int pageNumber, string? employeeFilter)
        {
            var employees = _employeeRepository.GetPaging(pageSize, pageNumber, employeeFilter);
            return Ok(employees);
        }

    }
}

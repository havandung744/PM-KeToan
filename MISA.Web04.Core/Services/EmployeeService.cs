using MISA.Web04.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Web04.Core.Resources;
using MISA.Web04.Core.Exceptions;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Services;

namespace MISA.Web04.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        /// <summary>
        /// Thực hiện validate dữ liệu cho Employee
        /// </summary>
        /// <param name="employee">đối tượng employee</param>
        /// <returns>
        /// true - validate thành công
        /// false - validate thất bại
        /// </returns>
        /// CreateBy: HVDUNG (20/06/2022)
        protected override bool Validate(Employee employee)
        {
            // 1.1 thông tin mã nhân viên không đươc phép để trống
            if (string.IsNullOrEmpty(employee.EmployeeCode))
            {string text = "EmployeeCode";
                ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
            }
            // 1.2 thông tin họ tên không đươc phép để trống
            if (string.IsNullOrEmpty(employee.EmployeeName))
            {string text = "EmployeeName";
                ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
            }
            //// 1.3 thông tin email phải đúng định dạng
            if (employee.Email != null && !IsValidEmail(employee.Email))
            {
                ErrorData.Add("Email", Resources.ResourceVN.checkValidateEmail);
            }

            //// 1.4 ngày sinh không được lớn hơn ngày hiện tại
            if (employee.DateOfBirth != null && !CheckDateOfBirth((DateTime)employee.DateOfBirth))
            {
                ErrorData.Add("DateOfBirth", Resources.ResourceVN.checkValidateDateOfBirth);
            }
            // 1.5 mã nhân viên không được phép trùng lặp
            if (employee.EmployeeCode != null)
            {
                var isDuplicate = _employeeRepository.CheckDuplicateCode(employee.EmployeeCode);
                if (isDuplicate == true)
                    ErrorData.Add("EmployeeCode", string.Format(Core.Resources.ResourceVN.NotExistProp, "EmployeeCode"));
            }

            // 1.6 nhân viên phải thuộc một phòng ban xác định (có DepartmentId)
            if (employee.DepartmentId == Guid.Empty)
            {
                string text = "DepartmentId";
                ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
            }
            if (employee.PositionId == Guid.Empty)
            {
                string text = "PositionId";
                ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
            }

            if (ErrorData.Count > 0) return false;
            else return true;
        }

        /// <summary>
        /// Thực hiện validate dữ liệu cho Employee khi cập nhật
        /// </summary>
        /// <param name="employee">đối tượng employee</param>
        /// <returns>
        /// true - validate thành công
        /// false - validate thất bại
        /// </returns>
        /// CreateBy: HVDUNG (20/06/2022)
        protected override bool ValidateForUpdate(Guid employeeId, Employee employee)
        {        // 1.1 thông tin mã nhân viên không đươc phép để trống
            if (string.IsNullOrEmpty(employee.EmployeeCode))
            {
                string text = "EmployeeCode";
                ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
                return false;
            }
            var getEmployeeCode = _employeeRepository.GetEntityCode(employeeId);
            var isDuplicate = _employeeRepository.CheckDuplicateCode(employee.EmployeeCode);
            if (getEmployeeCode != employee.EmployeeCode && isDuplicate == true)
                ErrorData.Add("EmployeeCode", string.Format(Core.Resources.ResourceVN.NotExistProp, "EmployeeCode"));


            // 1.2 thông tin họ tên không đươc phép để trống
            if (string.IsNullOrEmpty(employee.EmployeeName))
            {
                string text = "EmployeeName";
                ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
            }
            //// 1.3 thông tin email phải đúng định dạng
            if (employee.Email != null && !IsValidEmail(employee.Email))
            {
                ErrorData.Add("Email", Resources.ResourceVN.checkValidateEmail);
            }

            //// 1.4 ngày sinh không được lớn hơn ngày hiện tại
            if (employee.DateOfBirth != null && !CheckDateOfBirth((DateTime)employee.DateOfBirth))
            {
                ErrorData.Add("DateOfBirth", Resources.ResourceVN.checkValidateDateOfBirth);
            }

            // 1.6 nhân viên phải thuộc một phòng ban xác định (có DepartmentId)
            if (employee.DepartmentId == Guid.Empty)
            {
                string text = "DepartmentId";
                ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
            }
            if (employee.PositionId == Guid.Empty)
            {   string text = "PositionId";
                ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
            }


            if (ErrorData.Count > 0) return false;
            else return true;
        }


        /// <summary>
        /// Thực hiện kiểm tra Email có đúng định dạng không
        /// </summary>
        /// <param name="email"> email </param>
        /// <returns>
        /// true - email đúng định dạng
        /// false - email không đúng định dạng
        /// </returns>
        /// CreateBy: HVDUNG (09/06/2022)
        /// </summary>
        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Thực hiện kiểm tra ngày sinh có lớn hơn ngày hiện tại không
        /// </summary>
        /// <param name="DateOfBirth"> ngày sinh </param>
        /// <returns>
        /// true - ngày sinh không lớn hơn ngày hiện tại
        /// false - ngày sinh lớn hơn ngày hiện tại
        /// </returns>
        /// CreateBy: HVDUNG (09/06/2022)
        bool CheckDateOfBirth(DateTime? DateOfBirth)
        {
            if (DateOfBirth > DateTime.Now)
                return false;
            return true;
        }
    }
}

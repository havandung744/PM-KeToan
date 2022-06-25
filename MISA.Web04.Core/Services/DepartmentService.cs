using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Exceptions;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Services
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        /// Thực hiện validate dữ liệu cho Department
        /// </summary>
        /// <param name="department">đối tượng department</param>
        /// <returns>
        /// true - validate thành công
        /// false - validate thất bại
        /// </returns>
        /// CreateBy: HVDUNG (20/06/2022)
        protected override bool Validate(Department department)
        {
            if (string.IsNullOrEmpty(department.DepartmentCode))
            {
                string text = "DepartmentCode";
                ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
                return false;
            }
            var isDuplicate = _departmentRepository.CheckDuplicateCode(department.DepartmentCode);
            if (isDuplicate == true)
                ErrorData.Add("EmployeeCode", string.Format(Core.Resources.ResourceVN.NotExistProp, "DepartmentCode"));

            if (ErrorData.Count > 0) return false;
            else return true;

        }


        /// <summary>
        /// Thực hiện validate dữ liệu cho Department khi cập nhật
        /// </summary>
        /// <param name="department">đối tượng department</param>
        /// <returns>
        /// true - validate thành công
        /// false - validate thất bại
        /// </returns>
        /// CreateBy: HVDUNG (20/06/2022)
        protected override bool ValidateForUpdate(Guid departmentId, Department department)
        {
            if (string.IsNullOrEmpty(department.DepartmentCode))
            {string text = "DepartmentCode";
                ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
                return false;
            }
            var getDepartmentCode = _departmentRepository.GetEntityCode(departmentId);
            var isDuplicate = _departmentRepository.CheckDuplicateCode(department.DepartmentCode);
            if (getDepartmentCode != department.DepartmentCode && isDuplicate == true)
                ErrorData.Add("DepartmentCode", string.Format(Core.Resources.ResourceVN.NotExistProp, "DepartmentCode"));

            if (ErrorData.Count > 0) return false;

            else return true;
        }

    }
}

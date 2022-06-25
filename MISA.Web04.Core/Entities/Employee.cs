using MISA.Web04.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Entities
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string? EmployeeCode { get; set; }
        public string? EmployeeName { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? IdentityNumber { get; set; }
        public DateTime? IdentityDate { get; set; }
        public string? IdentityPlace { get; set; }
        public DateTime? JoinDate { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid PositionId { get; set; }
        public int? WorkStatus { get; set; }
        public string? PersonalTaxCode { get; set; }
        public decimal? Salary { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? BankName { get; set; }
        public string? BankBranchName { get; set; }
        public string? BankProvinceName { get; set; }
        public string? EmployeePosition { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    case Enum.Gender.Female:
                        return "Nữ";
                    case Enum.Gender.Male:
                        return "Nam";
                    default:
                        return "Không xác định";
                }
            }
        }
        public string? DepartmentName { get; set; }
        public string? PositionName { get; set; }
    }
}

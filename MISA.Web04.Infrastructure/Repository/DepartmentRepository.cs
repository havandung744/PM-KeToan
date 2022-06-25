using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using Microsoft.Extensions.Configuration;

namespace MISA.Web04.Infrastructure.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<Department> GetPaging(int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Thực hiện cập nhật lại thông tin phòng ban
        /// </summary>
        /// <param name="departmentId">mã phòng ban</param>
        /// <param name="department">đối tượng phòng ban</param>
        /// <returns>
        /// 1 - thành công
        /// 0 - thất bại
        /// </returns>
        /// CreatedBy: HVDUNG (20/06/2022)
        public override int Update(Guid departmentId, Department department)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                department.DepartmentId = departmentId;
                // 3. Thực hiện cập nhật dữ liệu
                var sqlCommandText = $"Proc_UpdateDepartment";
                var res = SqlConnection.Execute(sql: sqlCommandText, param: department, commandType: System.Data.CommandType.StoredProcedure);
                // 4. trả về thông tin cho client
                return res;
            }
        }
    }
}

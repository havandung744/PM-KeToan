using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Infrastructure.Repository
{
    public class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        public PositionRepository(IConfiguration configuration) : base(configuration)
        {

        }

        /// <summary>
        /// Thực hiện cập nhật thông tin chức vụ
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="position"></param>
        /// <returns>
        /// 1 -> cập nhật thành công
        /// 0 -> cập nhật thất bại
        /// </returns>
        /// CreatedBy: HVDUNG (20/06/2022)
        public override int Update(Guid positionId, Position position)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                position.PositionId = positionId;
                // 3. Thực hiện cập nhật dữ liệu
                var sqlCommandText = $"Proc_UpdatePosition";
                var res = SqlConnection.Execute(sql: sqlCommandText, param: position, commandType: System.Data.CommandType.StoredProcedure);
                // 4. trả về thông tin cho client
                return res;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MySqlConnector;

namespace MISA.Web04.Infrastructure.Repository
{
    public class BaseRepository<Entity>: IBaseRepository<Entity>
    {
        protected string ConnectionString;
        protected MySqlConnection SqlConnection;
        protected DynamicParameters Parameters = new DynamicParameters();

        public BaseRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("HVDUNG");
        }

     

        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <typeparam name="Entity">type của object</typeparam>
        /// <returns>Danh sách object</returns>
        /// CreatedBy: HVDUNG(15/06/2022)
        public IEnumerable<Entity> GetAll()
        {
            var className = typeof(Entity).Name;
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var entities = SqlConnection.Query<Entity>(sql: $"Proc_Get{className}s", commandType: System.Data.CommandType.StoredProcedure);
                return entities;
            }
        }

        /// <summary>
        /// Lấy dữ liệu theo Id
        /// </summary>
        /// <param name="EntityId">id của đối tượng</param>
        /// <returns>thông tin đối tượng trả về</returns>
        /// CreateBy: HVDUNG(18/06/2022)
        public Entity GetById(Guid EntityId)
        {
            var className = typeof(Entity).Name;
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                //var sqlCommand = $"SELECT * FROM {className} WHERE {className}Id = @{className}Id";
                var sqlCommand = $"Proc_Get{className}ById";
                Parameters.Add($"@d_{className}Id", EntityId);
                var entity = SqlConnection.QueryFirstOrDefault<Entity>(sql: sqlCommand, param: Parameters, commandType: System.Data.CommandType.StoredProcedure);
                // 4.trả kết quả cho cliend
                return entity;
            }
        }

        /// <summary>
        /// lấy Id tự động
        /// </summary>
        /// <returns>Id lớn nhất + 1 </returns>
        /// CreateBy: HVDUNG(18/06/2022)
        public string GetNewEntityCode()
        {
            var className = typeof(Entity).Name;
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var commandText = $"SELECT MAX({className}Code) from {className}";
                var maxEntityCode = SqlConnection.QueryFirstOrDefault<string>(commandText);
                var entityCode = string.Join(" ", maxEntityCode);
                for (int i = 0; i < maxEntityCode.Length; i++)
                {
                    if (entityCode[i] == '-')
                    {
                        string text = entityCode.Substring(0, i + 1);
                        int code = Int32.Parse(entityCode.Substring(i + 1)) + 1;
                        entityCode = text + code;
                    }
                }
            return entityCode;
            }
           
        }

        /// <summary>
        /// Xóa đối tượng theo Id
        /// </summary>
        /// <param name="EntityId">id của đối tượng</param>
        /// <returns>
        /// 0 -> xóa thất bại
        /// 1 -> xóa hành công
        /// </returns>
        /// CreatedBy: HVDUNG (18/06/2022)
        public int DeleteById(Guid EntityId)
        {
            var className = typeof(Entity).Name;
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlCommand = $"Proc_Delete{className}ById";
                Parameters.Add($"@d_{className}Id", EntityId);
                var res = SqlConnection.Execute(sql: sqlCommand, param: Parameters, commandType: System.Data.CommandType.StoredProcedure);
                // 4.trả kết quả cho cliend
                return res;
            }
        }


        /// <summary>
        /// thêm mới đối tượng
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns>
        /// 0 -> thêm thất bại
        /// 1 -> thêm hành công
        /// </returns>
        /// CreatedBy: HVDUNG (18/06/2022)
        public int Insert(Entity entity)
        {
            var className = typeof(Entity).Name;
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                // 3. Thực hiện thêm mới dữ liệu vào database
                var sqlCommandText = $"Proc_Add{className}";
                var res = SqlConnection.Execute(sql: sqlCommandText, param: entity, commandType: System.Data.CommandType.StoredProcedure);
                // 4. trả về thông tin cho client
                return res;
            }
        }

        /// <summary>
        /// Cập nhật thông tin đối tượng
        /// </summary>
        /// <param name="entityId">id của đối tượng</param>
        /// <param name="entity">đối tượng</param>
        /// <returns>
        /// 0 -> cập nhật thất bại
        /// 1 -> cập nhật thành công
        /// </returns>
        /// CreatedBy: HVDUNG(18/06/2022)
        public virtual int Update(Guid entityId, Entity entity)
        {
            var className = typeof(Entity).Name;
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                // 3. Thực hiện cập nhật dữ liệu
                var sqlCommandText = $"Proc_Update{className}";
                var res = SqlConnection.Execute(sql: sqlCommandText, param: entity, commandType: System.Data.CommandType.StoredProcedure);
                // 4. trả về thông tin cho client
                return res;
            }
        }

        /// <summary>
        /// kiểm tra mã đối tượng có bị trùng lặp hay không
        /// </summary>
        /// <param name="entityCode">mã của đối tượng</param>
        /// <returns>
        /// true -> trùng lặp
        /// false -> không trùng lặp
        /// </returns>
        /// CreatedBy: HVDUNG(18/06/2022)
        public bool CheckDuplicateCode(string entityCode)
        {
            var className = typeof(Entity).Name;
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlCheck = $"select {className}Code from {className} where {className}Code=@{className}Code";
                Parameters.Add($"@{className}Code", entityCode);
                var res = SqlConnection.QueryFirstOrDefault<string>(sql: sqlCheck, param: Parameters);
                if (res != null)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// lấy ra mã của đối tượng
        /// </summary>
        /// <param name="entityId">id của đối tượng</param>
        /// <returns>mã của đối tượng</returns>
        /// CreatedBy: HVDUNG(18/06/2022)
        public string GetEntityCode(Guid entityId)
        {
            var className = typeof(Entity).Name;
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlCommand = $"select {className}Code from {className} where {className}Id = @{className}Id";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add($"@{className}Id",entityId);
                var res = SqlConnection.QueryFirstOrDefault<string>(sql: sqlCommand, param: dynamicParameters);
                return res;
            }
        }
    }
}

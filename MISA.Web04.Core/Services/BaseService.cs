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
    public class BaseService<Entity> : IBaseService<Entity>
    {
        protected IBaseRepository<Entity> Repository;
        protected Dictionary<string, string> ErrorData;

        /// <summary>
        /// Thực hiện khởi tạo đối tượng
        /// </summary>
        /// <param name="repository">đối tượng repository</param>
        /// CreatedBy: HVDUNG (20/06/2022)
        public BaseService(IBaseRepository<Entity> repository)
        {
            Repository = repository;
            ErrorData = new Dictionary<string, string>();
        }

        /// <summary>
        /// thực hiện thêm mới đối tượng
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns>
        /// 1 - thêm thành công
        /// 0 - thêm thất bại
        /// </returns>
        /// <exception cref="ValidateException"></exception>
        /// CreatedBy: HVDUNG (20/06/2022)
        public int InsertService(Entity entity)
        {
            // thực hiện validate dữ liệu
            var isValid = Validate(entity);
            // thực hiện thêm mới vào databse
            if (isValid)
            {
                var res = Repository.Insert(entity);
                return res;
            }
            else
                throw new ValidateException(ErrorData);
        }

        /// <summary>
        /// thực hiện cập nhật đối tượng
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns>
        /// 1 - cập nhật thành công
        /// 0 - cập nhật thất bại
        /// </returns>
        /// <exception cref="ValidateException"></exception>
        /// CreatedBy: HVDUNG (20/06/2022)
        public int UpdateService(Guid entityId, Entity entity)
        {
            // thực hiện validate dữ liệu
            var isValid = ValidateForUpdate(entityId, entity);
            // thực hiện thêm mới vào databse
            if (isValid)
            {
                var res = Repository.Update(entityId, entity);
                return res;
            }
            else
                throw new ValidateException(ErrorData);
        }

        /// <summary>
        /// Thực hiện validate dữ liệu
        /// </summary>
        /// <param name="entity">đối tượng cần validate</param>
        /// <returns>
        /// true - validate thành công
        /// flase = validate thất bại
        /// </returns>
        ///  CreatedBy: HVDUNG (20/06/2022)
        protected virtual bool Validate(Entity entity)
        {
            return true;
        }

        /// <summary>
        /// Thực hiện validate dữ liệu khi cập nhật
        /// </summary>
        /// <param name="entity">đối tượng cần validate</param>
        /// <returns>
        /// true - validate thành công
        /// flase = validate thất bại
        /// </returns>
        ///  CreatedBy: HVDUNG (20/06/2022)
        protected virtual bool ValidateForUpdate(Guid entityId, Entity entity)
        {
            return true;
        }

    }
}

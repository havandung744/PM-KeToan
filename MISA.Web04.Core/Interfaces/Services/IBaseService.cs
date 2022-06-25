using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Services
{
    public interface IBaseService<Entity>
    {
        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity"> đối tượng Base</param>
        /// <returns>
        /// 1 -> thành công
        /// 0 -> thất bại
        /// </returns>
        /// CreatedBy: HVDUNG (13/06/2022)
        int InsertService(Entity entity);
        /// <summary>
        /// Cập nhật phòng ban
        /// </summary>
        /// <param name="entity">đối tượng Base</param>
        /// <returns>
        /// 1 -> thành công
        /// 0 -> thất bại
        /// </returns>
        int UpdateService(Guid entityId, Entity entity);
    }
}

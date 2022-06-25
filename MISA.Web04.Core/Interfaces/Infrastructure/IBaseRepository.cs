using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Interfaces.Infrastructure
{
    public interface IBaseRepository<Entity>
    {
        IEnumerable<Entity> GetAll();
        Entity GetById(Guid EntityId);
        int DeleteById(Guid EntityId);
        int Insert(Entity entity);
        int Update(Guid entityId, Entity entity);

        // thực hiện kiểm tra trùng mã đối tượng
        bool CheckDuplicateCode(string entitycode);
        
        // thực hiện lấy mã của đối tượng
        string GetEntityCode(Guid entytyId);
        // thực hiện tự động lấy mã đối tượng mới
        string GetNewEntityCode();
    }
}

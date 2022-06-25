using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web04.Core.Services
{
    public class PositionService : BaseService<Position>, IPositionService
    {
        IPositionRepository _positionRepository;
        public PositionService(IPositionRepository positionRepository) : base(positionRepository)
        {
            _positionRepository = positionRepository;
        }

        protected override bool Validate(Position position)
        {
            if (string.IsNullOrEmpty(position.PositionCode))
            {
                string text = "PositionCode";
                ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
                return false;
            }

            var isDuplicate = _positionRepository.CheckDuplicateCode(position.PositionCode);
            if (isDuplicate == true)
                ErrorData.Add("PositionCode", string.Format(Core.Resources.ResourceVN.NotExistProp, "PositionCode"));

            if (ErrorData.Count > 0) return false;

            else return true;
        }

        protected override bool ValidateForUpdate(Guid positionId, Position position)
        {
            if (string.IsNullOrEmpty(position.PositionCode))
            {
                string text = "positionCode";
                ErrorData.Add(text, string.Format(Core.Resources.ResourceVN.NotEmptyProp, text));
                return false;
            }

            // lấy ra mã nhân viên
            var getPositionCode = _positionRepository.GetEntityCode(positionId);
            var isDuplicate = _positionRepository.CheckDuplicateCode(position.PositionCode);
            if (getPositionCode != position.PositionCode && isDuplicate == true)
                ErrorData.Add("PositionCode", string.Format(Core.Resources.ResourceVN.NotExistProp, "PositionCode"));

            if (ErrorData.Count > 0) return false;
            else return true;   
        }

    }
}

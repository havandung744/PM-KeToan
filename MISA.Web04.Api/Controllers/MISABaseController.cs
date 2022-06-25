using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web04.Core.Interfaces.Infrastructure;
using MISA.Web04.Core.Interfaces.Services;

namespace MISA.Web04.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MISABaseController<Entity> : ControllerBase
    {
      IBaseRepository<Entity> _baseRepository;
        IBaseService<Entity> _baseService;


        /// <summary>
        /// Thực hiện khởi tạo đối tượng
        /// </summary>
        /// <param name="baseRepository">khởi tạo đối tượng baseRepository</param>
        /// <param name="baseService">khởi tạo đối tượng baseService</param>
        /// CreatedBy: HVDUNG (20/06/2022)
        public MISABaseController(IBaseRepository<Entity> baseRepository, IBaseService<Entity> baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
        }

        /// <summary>
        /// thực hiện lấy ra danh sách đối tượng
        /// </summary>
        /// <returns>danh sách các đối tượng</returns>
        /// CreatedBy: HVDUNG (20/06/2022)
        [HttpGet]
        public IActionResult Get()
        {
            var entitys = _baseRepository.GetAll();
            return Ok(entitys);
        }

        /// <summary>
        /// thực hiện lấy ra đối tượng dựa vào Id
        /// </summary>
        /// <param name="entityId">Id của đối tượng đó</param>
        /// <returns>Thông tin đối tượng</returns>
        /// CreatedBy: HVDUNG (20/06/2022)
        [HttpGet("{entityId}")]
        public IActionResult Get(Guid entityId)
        {
            var entity = _baseRepository.GetById(entityId);
            return Ok(entity);
        }

        /// <summary>
        /// thực hiện lấy Id tự động
        /// </summary>
        /// <returns>
        ///Id lớn nhất + 1
        /// </returns>
        /// CreatedBy: HVDUNG (20/06/2022)
        [HttpGet("NewEmployeeCode")]
        public IActionResult NewEntityCode()
        {
            var res = _baseRepository.GetNewEntityCode();
            return Ok(res);
        }

        /// <summary>
        /// thực hiện xóa đối tượng dựa vào Id
        /// </summary>
        /// <param name="entityId">Id của đối tượng đó</param>
        /// <returns>
        /// 1 - xóa thành công
        /// 0 - xóa thất bại
        /// </returns>
        /// CreatedBy: HVDUNG (20/06/2022)
        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {

            var res = _baseRepository.DeleteById(entityId);
            return Ok(res);
        }

        /// <summary>
        /// thực hiện thêm mới đối tượng
        /// </summary>
        /// <param name="entity">thông tin đối tượng thêm vào</param>
        /// <returns>
        /// 1 - thêm thành công
        /// 0 - thêm thất bại
        /// </returns>
        /// CreatedBy: HVDUNG (20/06/2022)
        [HttpPost]
        public IActionResult Post(Entity entity)
        {
            // thêm mới vào database
            var res = _baseService.InsertService(entity);
            return StatusCode(201, res);
        }

        /// <summary>
        /// thực hiện cập nhật đối tượng dựa vào Id
        /// </summary>
        /// <param name="entityId">Id của đối tượng đó</param>
        /// <returns>
        /// 1 - cập nhật thành công
        /// 0 - cập nhật thất bại
        /// </returns>
        /// CreatedBy: HVDUNG (20/06/2022)
        [HttpPut("{entityId}")]
        public IActionResult Put(Guid entityId, Entity entity)
        {
            var res = _baseService.UpdateService(entityId, entity);
            return Ok(res);
        }
    }
}

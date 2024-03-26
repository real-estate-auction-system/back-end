using Application.Commons;
using Application.Interfaces;
using Application.Services;
using Application.ViewModels.NewsViewModel;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newService;
        private readonly IClaimsService _claimsService;
        private readonly IMapper _mapper;


        public NewsController(INewsService newsService, IClaimsService claimsService, IMapper mapper)
        {
            _newService = newsService;
            _claimsService = claimsService;
            _mapper = mapper;
        }

        [HttpGet("Pagination")]
        public async Task<ActionResult<Pagination<News>>> GetAll(int? pageIndex, int? pageSize)
        {
            // Thiết lập giá trị mặc định cho pageIndex và pageSize nếu chúng là null hoặc rỗng
            int index = pageIndex ?? 0;
            int size = pageSize ?? 10;

            var list = await _newService.GetPaginationAsync(index, size);
            //var list = await _newService.GetPaginationAsync(0, 10);
            if (list.Items.Count == 0)
            {
                return NotFound("Không tìm thấy dữ liệu.");
            }
            return Ok(list);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] NewsModel newsModel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _newService.AddAsync(newsModel, _claimsService.GetCurrentUserId);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet("GetInformationNews/{id}")]
        public async Task<IActionResult> GetInformationNews(int id)
        {
            try
            {
                var news = await _newService.GetNewsByIdAsync(id);
                if (news == null) { return NotFound(); }
                return Ok(news);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNews(int id, [FromBody] NewsModelRequest newsModelRequest)
        {
            try
            {
                var newsUpdate = _mapper.Map<News>(newsModelRequest);
                newsUpdate = await _newService.UpdateNewsModel(id, newsUpdate);
                if (newsUpdate == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<News>(newsUpdate));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRealEstate(int id)
        {
            try
            {
                var realEstate = await _newService.GetByIdAsync(id);
                if (realEstate == null) { return NotFound(); }
                await _newService.DeleteAsync(realEstate);
                return Ok(realEstate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNews()
        {
            try
            {
                var response = await _newService.GetAllNews();
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using Application.Commons;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newService;
        public NewsController(INewsService newsService)
        {
            _newService = newsService;
        }
        [HttpGet("Pagination")]
        public async Task<Pagination<News>> GetAll(int pageIndex, int pageSize)
        {
            var list = await _newService.GetPaginationAsync(pageIndex, pageSize);
            if (list.Items.Count == 0)
            {
                throw new Exception("Khong co gi");
            }
            return list;
        }
    }
}

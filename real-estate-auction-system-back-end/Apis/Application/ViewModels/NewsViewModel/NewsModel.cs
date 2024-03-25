using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.NewsViewModel
{
    public class NewsModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public List<IFormFile>? Image { get; set; } = default!;
    }
}

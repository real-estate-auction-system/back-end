using DataAccessLayer.DAOs;
using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public interface IWatercolorsPaintingRepository
    {
        public IQueryable<WatercolorsPainting> GetAll();
        public IQueryable<WatercolorsPainting> GetById(string id);
        Task AddAsync(WatercolorsPainting watercolorsPainting);
        Task UpdateAsync(WatercolorsPainting watercolorsPainting);
        Task DeleteAsync(string id);
    }
}

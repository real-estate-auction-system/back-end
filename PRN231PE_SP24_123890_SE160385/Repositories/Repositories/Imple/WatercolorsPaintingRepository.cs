using DataAccessLayer.DAOs;
using DataAccessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Imple
{
    public class WatercolorsPaintingRepository : IWatercolorsPaintingRepository
    {
        private readonly WatercolorsPaitingDAO watercolorsPaitingDAO;
        public WatercolorsPaintingRepository(WatercolorsPaitingDAO _watercolorsPaitingDAO)
        {
            watercolorsPaitingDAO = _watercolorsPaitingDAO;
        }
        public IQueryable<WatercolorsPainting> GetAll()
            => watercolorsPaitingDAO.GetAll();
        public IQueryable<WatercolorsPainting> GetById(string id)
            => watercolorsPaitingDAO.GetById(id);
        public async Task AddAsync(WatercolorsPainting watercolorsPainting)
            => await watercolorsPaitingDAO.AddAsync(watercolorsPainting);
        public async Task UpdateAsync(WatercolorsPainting watercolorsPainting)
            => await watercolorsPaitingDAO.UpdateAsync(watercolorsPainting);
        public async Task DeleteAsync(string id)
            => await watercolorsPaitingDAO.DeleteAsync(id);
    }
}

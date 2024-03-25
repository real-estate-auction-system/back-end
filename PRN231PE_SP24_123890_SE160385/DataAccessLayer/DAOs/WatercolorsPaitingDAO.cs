using DataAccessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAOs
{
    public class WatercolorsPaitingDAO
    {
        public IQueryable<WatercolorsPainting> GetAll()
        {
            IQueryable<WatercolorsPainting> tattoos;
            var context = new WatercolorsPainting2024DBContext();
            tattoos = context.WatercolorsPaintings;

            return tattoos;
        }
        public IQueryable<WatercolorsPainting> GetById(string id)
        {
            IQueryable<WatercolorsPainting> tattoos;
            var context = new WatercolorsPainting2024DBContext();
            tattoos = context.WatercolorsPaintings.Where(x => x.PaintingId == id);
            return tattoos;
        }
        public async Task AddAsync(WatercolorsPainting watercolorsPainting)
        {
            using (var context = new WatercolorsPainting2024DBContext())
            {
                watercolorsPainting.CreatedDate = DateTime.Now;
                context.WatercolorsPaintings.Add(watercolorsPainting);
                await context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(WatercolorsPainting watercolorsPainting)
        {
            using (var context = new WatercolorsPainting2024DBContext())
            {
                context.Entry(watercolorsPainting).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(string id)
        {
            using (var context = new WatercolorsPainting2024DBContext())
            {
                var tattoo = await context.WatercolorsPaintings.Where(x => x.PaintingId == id).FirstOrDefaultAsync();
                context.WatercolorsPaintings.Remove(tattoo);
                await context.SaveChangesAsync();
            }
        }
    }
}

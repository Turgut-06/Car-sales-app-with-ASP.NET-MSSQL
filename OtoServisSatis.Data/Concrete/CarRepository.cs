using Microsoft.EntityFrameworkCore;
using OtoServisSatis.Data.Abstract;
using OtoServisSatis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisSatis.Data.Concrete
{
    public class CarRepository : Repository<Arac>, ICarRepository
    {
        public CarRepository(DataBaseContext context) : base(context)
        {
        }

        public async Task<Arac> GetCustomCar(int id)
        {
            return await _dbSet.AsNoTracking().Include(x => x.marka).FirstOrDefaultAsync(c => c.Id == id); //tek kayıt için bu detay sayfası için
        }

        public async Task<List<Arac>> GetCustomCarList()
        {
            return await _dbSet.AsNoTracking().Include(x => x.marka).ToListAsync(); //Marka sınıfıyla ilişkili olduğu için direk gelmiyor bu şekilde geliyor
        }

        public async Task<List<Arac>> GetCustomCarList(Expression<Func<Arac, bool>> expression) //içine koşul girebilmek için
        {
            return await _dbSet.Where(expression).AsNoTracking().Include(x => x.marka).ToListAsync();
        }
    }
}

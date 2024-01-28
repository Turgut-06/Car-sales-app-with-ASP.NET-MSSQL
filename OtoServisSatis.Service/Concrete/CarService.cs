using OtoServisSatis.Data;
using OtoServisSatis.Data.Concrete;
using OtoServisSatis.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisSatis.Service.Concrete
{
    public class CarService : CarRepository , ICarService
    {
        public CarService(DataBaseContext context) : base(context)
        {
        }
    }
}

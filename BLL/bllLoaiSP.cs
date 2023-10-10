using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class bllLoaiSP
    {
        public List<LoaiSP> GetAll()
        {
            ItemModel ctx = new ItemModel();
            return ctx.LoaiSPs.ToList();
        }
    }
}

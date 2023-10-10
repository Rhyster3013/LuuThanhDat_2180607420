using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class bllSanPham
    {
        public List<SanPham> GetAll()
        {
            ItemModel ctx = new ItemModel();
            return ctx.SanPhams.ToList();
        }

        public List<SanPham> GetFind(string name, ItemModel ctx)
        {
            var result = from s in ctx.SanPhams
                         where s.TenSP.Contains(name)
                         select s;
            return result.ToList();
        }

        public SanPham loadFind(string Id, ItemModel ctx)
        {
            return ctx.SanPhams.FirstOrDefault(p => p.MaSP == Id);
        }

        public void Add (SanPham s)
        {
            ItemModel ctx = new ItemModel();
            ctx.SanPhams.AddOrUpdate(s);
            ctx.SaveChanges();
        }

        public void Del (SanPham s, ItemModel ctx)
        {
            var kiemtra = ctx.SanPhams.Local.FirstOrDefault(st => st.MaSP == s.MaSP);
            if (kiemtra != null)
            {
                ctx.Entry(kiemtra).State = EntityState.Detached; 
            }

            ctx.SanPhams.Add(s);
            ctx.Entry(s).State = EntityState.Deleted; 
            ctx.SaveChanges();
        }
    }
}

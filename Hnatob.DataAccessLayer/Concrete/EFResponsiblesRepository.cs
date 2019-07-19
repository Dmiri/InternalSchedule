using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Project
using Hnatob.DataAccessLayer.Abstract;
using Hnatob.DataAccessLayer.Context;
using Hnatob.Domain.Models;

namespace Hnatob.DataAccessLayer.Concrete
{
    public class EFResponsiblesRepository : IResponsiblesRepository
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public override IQueryable<Responsible> GetResponsibles()
        {
            var dbEntry = context.Responsibles;
            return dbEntry;
        }

        public override void UpdateResponsible(Responsible model)
        {
            //if (model == null) return;

            //if (model.Id == 0) return;
            ////context.Responsibles.Add(model);

            //else
            //{
            //    var dbEntry = context.Responsibles.FirstOrDefault( r => r.Id == model.Id);
            //    if (dbEntry != null)
            //    {
            //        dbEntry.Position = model.Position;
            //    }
            //    else
            //    {
            //        //throw new Exception("Record didn't found");
            //    }
            //}
            //context.SaveChanges();
        }

        public override void UpdateResponsible(IEnumerable<Responsible> models)
        {
            //if (models == null) return;

            //var dbAllEntrys = context.Responsibles.ToList();
            //foreach (var dbEntry in dbAllEntrys)
            //{
            //    foreach (var model in models)
            //    {
            //        if (model.Id == 0) continue;

            //        else
            //        {
            //            if (dbEntry.Id == model.Id)
            //            {
            //                dbEntry.Position = model.Position;
            //            }
            //            else
            //            {
            //                //throw new Exception("Record didn't found");
            //            }
            //        }
            //    }
            //}
            //context.SaveChanges();
        }
    }
}

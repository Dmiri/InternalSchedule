// System
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Project
using Hnatob.Domain.Abstract;
using Hnatob.Domain.Helper;

namespace Hnatob.Domain.Concrete
{
    class EFCommentToServiceRepository : ICommentToServiceRepository
    {
        EfDbContext context = new EfDbContext();

        public override IQueryable<CommentToService> GetCommentToServices()
        {
            var dbEntry = context.CommentsToservices;
            return dbEntry;
        }

        public override IQueryable<CommentToService> GetCommentToServices(int EventId)
        {
            var dbEntry = context.CommentsToservices.Where(o => o.EventId == EventId);
            return dbEntry;
        }

        public override void UpdateCommentToService(CommentToService model)
        {
            throw new NotImplementedException();
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

        public override void UpdateCommentToService(IEnumerable<CommentToService> models)
        {
            throw new NotImplementedException();
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

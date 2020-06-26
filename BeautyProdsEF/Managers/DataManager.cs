using BeautyProdsEF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendNotifWebJob.Managers
{
    public class DataManager
    {

        public void saveEntity<T>(T newDataModel)  where T : class 
        {
            using (var context = new BeautyProdsEntities())
            {
                context.Set<T>().Add(newDataModel);
                context.SaveChanges();
            }
        }

        public void updateEntity<T>(T newDataModel) where T : class
        {
            try
            {
                using (var context = new BeautyProdsEntities())
                {
                    context.Set<T>().Attach(newDataModel);
                    context.Entry(newDataModel).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T getEntity<T>(Func<T, bool> filter, string[] includes = null) where T : class
        {
            T dataOut = null;
            using (var context = new BeautyProdsEntities())
            {
                var query = context.Set<T>().AsQueryable();
                if (includes != null)
                {
                    foreach (string include in includes)
                    {
                        query = query.Include(include);
                    }
                }
                dataOut = query.FirstOrDefault(filter);
            }
            return dataOut;
        }

        public List<T> getList<T>(Func<T, bool> filter = null, string[] includes = null) where T : class
        {
            List<T> modelList = null;
            using (var context = new BeautyProdsEntities())
            {
                var query = context.Set<T>().AsQueryable();
                if (includes != null)
                {
                    foreach (string include in includes)
                    {
                        query = query.Include(include);
                    }
                }

                if (filter != null)
                {
                    query = query.Where<T>(filter).AsQueryable();
                }
                modelList = query.ToList();
            }
            return modelList;
        }

        public void saveList<T>(List<T> list) where T : class
        {
            if (list != null)
            {
                using (var context = new BeautyProdsEntities())
                {
                    foreach (var entityToSave in list)
                    {
                        context.Set<T>().Add(entityToSave);
                    }
                    context.SaveChanges();
                }
            }
        }

    }
}

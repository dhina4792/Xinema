using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XinemaActual.Models;

namespace XinemaActual.DAL
{
    public class DataGateway<T> : IDataGateway<T> where T : class
    {
        internal XinemaActualContext db = new XinemaActualContext();
        internal DbSet<T> data = null;


        public DataGateway()
        {
            this.data = db.Set<T>();
        }
        public T Delete(int? id)
        {
            T obj = data.Find(id);
            data.Remove(obj);
            db.SaveChanges();
            return obj;
        }

        public void Insert(T obj)
        {
            data.Add(obj);
            db.SaveChanges();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> SelectAll()
        {
            return data;
        }

        public T SelectById(int? id)
        {
            T obj = data.Find(id);
            return obj;
        }

        public void Update(T obj)
        {
            db.Entry(data).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
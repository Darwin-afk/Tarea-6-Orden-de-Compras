using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OrdenDeCompras.Entidades;
using OrdenDeCompras.DAL;
using System.Linq;
using System.Linq.Expressions;

namespace OrdenDeCompras.BLL
{
    public class OrdenesBLL
    {
        public static bool Guardar(Ordenes orden)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Ordenes.Add(orden) != null)
                    paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        public static bool Modificar(Ordenes orden)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Database.ExecuteSqlRaw($"Delete FROM OrdenesDetalle Where OrdenId={orden.OrdenId}");
                foreach (var item in orden.Detalle)
                {
                    db.Entry(item).State = EntityState.Added;
                }

                db.Entry(orden).State = EntityState.Modified;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var eliminar = OrdenesBLL.Buscar(id);
                db.Entry(eliminar).State = EntityState.Deleted;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        public static Ordenes Buscar(int id)
        {
            Contexto db = new Contexto();
            Ordenes orden = new Ordenes();

            try
            {
                orden = db.Ordenes.Include(x => x.Detalle)
                    .Where(x => x.OrdenId == id)
                    .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return orden;
        }

        public static List<Ordenes> GetList(Expression<Func<Ordenes, bool>> orden)
        {
            List<Ordenes> lista = new List<Ordenes>();
            Contexto db = new Contexto();

            try
            {
                lista = db.Ordenes.Where(orden).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return lista;
        }
    }
}

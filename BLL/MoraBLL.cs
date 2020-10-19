using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Registro_prestamos.DAL;
using Registro_prestamos.Entidades;

namespace Registro_prestamos.BLL
{
    public class MorasBLL
    {
        public static bool Guardar(Moras mora){
            if(!Existe(mora.MoraId))
                return Insertar(mora); 
            else    
                return Modificar(mora);
        }
        private static bool Insertar(Moras mora){
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Moras.Add(mora);
                paso = contexto.SaveChanges() > 0;

            } catch{
                throw;
            
            } finally{
                contexto.Dispose();
            }

            return paso;
        }
        public static bool Modificar(Moras mora){
            Contexto contexto = new Contexto();
            bool paso = false;

            try
            {
                contexto.Database.ExecuteSqlRaw($"delete from MorasDetalle where MoraID = {mora.MoraId}");
                foreach(var anterior in mora.Detalle)
                {
                    contexto.Entry(anterior).State = EntityState.Added;
                }

                contexto.Entry(mora).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            } 
            catch
            {
                throw;
            } 
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }
        public static bool Eliminar(int id){
            Contexto contexto = new Contexto();
            bool paso = false;

            try{
                var mora = contexto.Moras.Find(id);

                if(mora != null){
                    contexto.Entry(mora).State = EntityState.Deleted;
                    paso = contexto.SaveChanges() > 0;
                }
            
            } catch{
                throw;
            
            } finally{
                contexto.Dispose();
            }

            return paso;
        }
        public static Moras Buscar(int id){
            Contexto contexto = new Contexto();
            Moras mora;

            try{
                mora = contexto.Moras
                    .Include(m => m.Detalle)
                    .Where(m => m.MoraId == id)
                    .SingleOrDefault();
                
            } catch{
                throw;
            
            } finally{
                contexto.Dispose();
            }

            return mora;
        }
        public static bool Existe(int id){
            Contexto contexto = new Contexto();
            bool paso = false;

            try{
                paso = contexto.Moras.Any(p => p.MoraId == id);
            
            } catch{
                throw;
            
            } finally{
                contexto.Dispose();
            }

            return paso;
        }

        public static List<Moras> GetList(Expression<Func<Moras, bool>> criterio)
        {
            List<Moras> list = new List<Moras>();
            Contexto contexto = new Contexto();

            try {
                list = contexto.Moras.Where(criterio).AsNoTracking().ToList();

            } catch  {
                throw;

            } finally {
                contexto.Dispose();
            }

            return list;
        }
    }
}
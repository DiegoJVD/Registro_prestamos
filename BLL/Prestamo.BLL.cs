
using Registro_prestamos.DAL;
using Registro_prestamos.BLL;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Registro_prestamos.Entidades;

namespace Registro_prestamos.BLL
{
    public class PrestamoBLL
    {
        /// <summary>
        /// Permite insertar o modificar una entidad en la base de datos
        /// </summary>
        /// <param name="prestamo">La entidad que se desea guardar</param> 
        public static bool Guardar(Prestamo prestamo)
        {
            if (!Existe(prestamo.PrestamoId))//si no existe insertamos
                return Insertar(prestamo);
            else
                return Modificar(prestamo);
        }

        /// <summary>
        /// Permite insertar una entidad en la base de datos
        /// </summary>
        /// <param name="Prestamo">La entidad que se desea guardar</param>
        private static bool Insertar(Prestamo prestamo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {

                int[] a ={1};

                a[3]=1;

                contexto.Prestamo.Add(prestamo);
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        /// <summary>
        /// Permite modificar una entidad en la base de datos
        /// </summary>
        /// <param name="prestamo">La entidad que se desea modificar</param> 
        public static bool Modificar(Prestamo prestamo)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                //marcar la entidad como modificada para que el contexto sepa como proceder
                contexto.Entry(prestamo).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        /// <summary>
        /// Permite eliminar una entidad de la base de datos
        /// </summary>
        /// <param name="id">El Id de la entidad que se desea eliminar</param> 
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                //buscar la entidad que se desea eliminar
                var prestamo = contexto.Prestamo.Find(id);

                if (prestamo != null)
                {
                    contexto.Prestamo.Remove(prestamo);//remover la entidad
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        /// <summary>
        /// Permite buscar una entidad en la base de datos
        /// </summary>
        /// <param name="id">El Id de la entidad que se desea buscar</param> 
        public static Prestamo Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Prestamo prestamo;

            try
            {
                prestamo = contexto.Prestamo.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return prestamo;
        }

        /// <summary>
        /// Permite obtener una lista filtrada por un criterio de busqueda
        /// </summary>
        /// <param name="criterio">La expresión que define el criterio de busqueda</param>
        /// <returns></returns>
        public static List<Prestamo> GetList(Expression<Func<Prestamo, bool>> criterio)
        {
            List<Prestamo> lista = new List<Prestamo>();
            Contexto contexto = new Contexto();
            try
            {
                //obtener la lista y filtrarla según el criterio recibido por parametro.
                lista = contexto.Prestamo.Where(criterio).AsNoTracking().ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Prestamo
                    .Any(e => e.PrestamoId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return encontrado;
        }

        public static List<Prestamo> GetPrestamo()
        {
            List<Prestamo> lista = new List<Prestamo>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Prestamo.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return lista;
        }
    }
}
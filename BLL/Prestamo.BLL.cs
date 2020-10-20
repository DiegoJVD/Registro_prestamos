
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
        public static bool Guardar(Prestamos prestamo)
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
        private static bool Insertar(Prestamos prestamos)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                Personas person = new Personas();
                person = PersonaBLL.Buscar(prestamos.PersonaId);
                prestamos.Balance = prestamos.monto;
                person.Balance += prestamos.Balance;
                PersonaBLL.Guardar(person);

                contexto.Prestamos.Add(prestamos);
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
        /// <param name="prestamos">La entidad que se desea modificar</param> 
        public static bool Modificar(Prestamos prestamo)
        {
            bool paso = false;
            decimal balanceAntes;
            Contexto contexto = new Contexto();

            try
            {
                Personas persona = new Personas();
                persona = PersonaBLL.Buscar(prestamo.PersonaId);
                balanceAntes = prestamo.Balance;
                prestamo.Balance = prestamo.monto;
                persona.Balance -= balanceAntes;
                persona.Balance += prestamo.Balance;
                PersonaBLL.Guardar(persona);
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
                var pres = contexto.Prestamos.Find(id);
                Personas persona = new Personas();
                persona = PersonaBLL.Buscar(pres.PersonaId);
                persona.Balance -= pres.Balance;
                PersonaBLL.Guardar(persona);

                //buscar la entidad que se desea eliminar
                var prestamo = contexto.Prestamos.Find(id);

                if (prestamo != null)
                {
                    contexto.Prestamos.Remove(prestamo);//remover la entidad
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
        public static Prestamos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Prestamos prestamo;

            try
            {
                prestamo = contexto.Prestamos.Find(id);
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
        public static List<Prestamos> GetList(Expression<Func<Prestamos, bool>> criterio)
        {
            List<Prestamos> lista = new List<Prestamos>();
            Contexto contexto = new Contexto();
            // try
            // {
                lista = contexto.Prestamos.Where(criterio).AsNoTracking().ToList();
            // }
            // catch (Exception)
            // {
            //     throw;
            // }
            // finally
            // {
            //     contexto.Dispose();
            // }
            return lista;
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try
            {
                encontrado = contexto.Prestamos.Any(e => e.PrestamoId == id);
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

        public static List<Prestamos> GetPrestamo()
        {
            List<Prestamos> lista = new List<Prestamos>();
            Contexto contexto = new Contexto();
            try
            {
                lista = contexto.Prestamos.ToList();
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
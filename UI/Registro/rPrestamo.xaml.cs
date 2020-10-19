using System;
using System.Windows;
using Registro_prestamos.DAL;
using Registro_prestamos.Entidades;
using Registro_prestamos.BLL;
using Registro_prestamos.UI;

namespace Registro_prestamos.UI.Registro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class rPrestamo : Window
    {

        private Prestamos prestamo;
        private Personas persona;
        public rPrestamo()
        {
            InitializeComponent();
            prestamo = new Prestamos();
            persona = new Personas();
            this.DataContext = this.prestamo;
        }

        public void GuardarButton_Click(object render, RoutedEventArgs e)
        {
            if (!Validar()){
                return;
            }   

                var paso = PrestamoBLL.Guardar(prestamo);
                if (paso)
                {
                    Limpiar();
                    MessageBox.Show("Guardado con Exito"," ",MessageBoxButton.OK);
                }
                else
                    MessageBox.Show("Error al guardar", "", MessageBoxButton.OK);


        }

        public void Limpiar()
        {
            this.prestamo = new Prestamos();
            this.DataContext = prestamo;
        }

        private void BuscarButton_Click(object render, RoutedEventArgs e)
        {

            Contexto context = new Contexto();

            var found = PrestamoBLL.Buscar(Convert.ToInt32(IdTextBox.Text));


            if (found != null)
                this.prestamo = found;
            else
            {
                this.prestamo = new Prestamos();
                MessageBox.Show("No encontrado, por favor confirme que sea un id valido e intente de nuevo ","", MessageBoxButton.OK);
            }


            this.DataContext = this.prestamo;
        }

        private bool Validar()
        {
            bool Valido = true;
            if (FechaDateTimePicker.Text.Length == 0)
            {
                Valido = false;
                MessageBox.Show("Introduzca una fecha e intente de nuevo", "Error al guardad", MessageBoxButton.OK);
            }
            if (PersonaIdTextBox.Text.Length == 0)
            {
                Valido = false;
                MessageBox.Show("Introduzca un id de persona e intente de nuevo ", "error al guardar", MessageBoxButton.OK);
            }
            else if (MontoTextBox.Text.Length == 0){
                Valido = false;
                MessageBox.Show("Introduzca un monto e intente de nuevo ", "error al guardar", MessageBoxButton.OK);
            }
            {

            }

            return Valido;
        }

        private void NuevoButton_Click(object render, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void EliminarButton_Click(object render, RoutedEventArgs e)
        {
            if (PrestamoBLL.Eliminar(Convert.ToInt32(IdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Eliminado");
            }
            else
                MessageBox.Show("Error ");

        }


    }
}

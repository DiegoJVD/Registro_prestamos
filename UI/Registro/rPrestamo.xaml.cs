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

        private Prestamo prestamo;
        private Persona persona;
        public rPrestamo()
        {
            InitializeComponent();
            prestamo = new Prestamo();
            persona = new Persona();
            this.DataContext = prestamo;
        }

        public void GuardarButton_Click(object render, RoutedEventArgs e)
        {
            if (!Validar())
                return;
            var person = PersonaBLL.Buscar(Convert.ToInt32(PersonaIdTextBox.Text));
            if (person != null)
            {
                person.Balance = person.Balance + Convert.ToDecimal(MontoTextBox.Text);
                BalanceTextBox.Text =  MontoTextBox.Text;

                var paso = PrestamoBLL.Guardar(prestamo);
                if (paso)
                {
                    Limpiar();
                    MessageBox.Show("Guardado con Exito", "Exito!!", MessageBoxButton.OK);
                }
                else
                    MessageBox.Show("Error al guardar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        public void Limpiar()
        {
            this.prestamo = new Prestamo();
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
                this.prestamo = new Prestamo();
                MessageBox.Show("No encontrado", "Error", MessageBoxButton.OK);
            }


            this.DataContext = this.prestamo;
        }

        private void BuscarPersona(object render, RoutedEventArgs e)
        {
            var found = PersonaBLL.Buscar(Convert.ToInt32(PersonaIdTextBox.Text));

            if (found != null)
            {
                BalanceTextBox.Text = Convert.ToString(persona.Balance);
            }
        }

        private bool Validar()
        {
            bool esValido = true;
            if (IdTextBox.Text.Length == 0 && PersonaIdTextBox.Text.Length == 0 && BalanceTextBox.Text.Length == 0 && MontoTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Error, Intentelo de nuevo", "Error", MessageBoxButton.OKCancel);


            }
            return esValido;
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
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("Error al eliminar", "Error", MessageBoxButton.OK);

        }


    }
}

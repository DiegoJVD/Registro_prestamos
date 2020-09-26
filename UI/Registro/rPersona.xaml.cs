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
    public partial class rPersona : Window
    {
        
        private Persona persona;
        public rPersona()
        {
            InitializeComponent();
            persona = new Persona();
            this.DataContext = persona;
        }

        public void GuardarButton_Click(object render, RoutedEventArgs e)
        {
            if(!Validar())
                return;
            var paso = PersonaBLL.Guardar(this.persona);
            if(paso){
                Limpiar();
                MessageBox.Show("Guardado con Exito", "Exito!!",MessageBoxButton.OK);
            }
            else
            MessageBox.Show("Error al guardar", "Error",MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void Limpiar(){
            this.persona = new Persona();
            this.DataContext = persona;
        }

        private void BuscarButton_Click(object render, RoutedEventArgs e)
        {

            Contexto context = new Contexto();

            var found = PersonaBLL.Buscar(Convert.ToInt32(IdTextBox.Text));


            if(found != null)
            this.persona = found;
            else{
            this.persona = new Persona();
            MessageBox.Show("No encontrado", "Error",MessageBoxButton.OK);
            }
            

            this.DataContext = this.persona;
        }

        private bool Validar(){
            bool esValido = true;
            if(IdTextBox.Text.Length == 0 && NombreTextBox.Text.Length == 0 && BalanceTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Error, Intentelo de nuevo", "Error",MessageBoxButton.OKCancel);

                
            }
            return esValido;
        }

        private void NuevoButton_Click(object render, RoutedEventArgs e){
            Limpiar();
        }

        private void EliminarButton_Click(object render, RoutedEventArgs e){
            if(PersonaBLL.Eliminar(Convert.ToInt32(IdTextBox.Text))){
                Limpiar();
                MessageBox.Show("Eliminado", "Exito",MessageBoxButton.OK);
            }
            else
            MessageBox.Show("Error al eliminar", "Error",MessageBoxButton.OK);
            
        }
        

    }
}

using System;
using System.Windows;
using Registro_prestamos.DAL;
using Registro_prestamos.Entidades;
using Registro_prestamos.BLL;
using Registro_prestamos.UI;
using Microsoft.EntityFrameworkCore;

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
        }

        public void GuardarButton_Click(object render, RoutedEventArgs e)
        {
            if(!Validar())
                return;
            var paso = PersonaBLL.Guardar(persona);
            if(paso){
                Limpiar();
                MessageBox.Show("Guardado con Exito");
            }
            else
            MessageBox.Show("Error al guardar");
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
            MessageBox.Show("No encontrado");
            }
            

            this.DataContext = this.persona;
        }

        private bool Validar(){
            bool Valido = true;
            if(IdTextBox.Text.Length == 0 && NombreTextBox.Text.Length == 0 )
            {
                Valido = false;
                MessageBox.Show("Error");

                
            }
            return Valido;
        }

        private void NuevoButton_Click(object render, RoutedEventArgs e){
            Limpiar();
        }

        private void EliminarButton_Click(object render, RoutedEventArgs e){
            if(PersonaBLL.Eliminar(Convert.ToInt32(IdTextBox.Text))){
                Limpiar();
                MessageBox.Show("Eliminado");
            }
            else
            MessageBox.Show("Error ");
            
        }
        

    }
}

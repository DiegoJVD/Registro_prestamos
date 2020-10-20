using System.Windows;
using Registro_prestamos.Entidades;
using Registro_prestamos.BLL;
using System.Linq;
using System.Windows.Documents;
using System.Collections.Generic;
using System;
using Registro_prestamos.UI.Registro;


namespace Registro_prestamos.UI.Registro
{
    public partial class rMoras : Window 
    {
        Moras mora;
        public rMoras(){
            InitializeComponent();
            mora = new Moras();
            this.DataContext = mora;

            PrestamoComboBox.ItemsSource = PrestamoBLL.GetList(p => true);
            PrestamoComboBox.SelectedValuePath = "PrestamoId ";
            PrestamoComboBox.DisplayMemberPath = "Concepto";
        }

        private void Limpiar()
        {
            mora = new Moras();
            Actualizar();
        }

        private void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = mora;
        }

        private bool ExisteDB()
        {
            mora = MorasBLL.Buscar(Convert.ToInt32(IdTextBox.Text));

            return (mora != null);
        }
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Moras anterior = MorasBLL.Buscar(Utilidades.ToInt(IdTextBox.Text));

            if(anterior != null)
            {
                mora = anterior;
                Actualizar();
            }
            else
            {
                MessageBox.Show("Mora no encontrada.", "Error al buscar Mora");
            }
        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            if(!ValidarDetalle()){
                return;
            }
                
             
                MorasDetalle detalle = new MorasDetalle(
                Convert.ToInt32(IdTextBox.Text),
                Convert.ToInt32(PrestamoComboBox.SelectedValue.ToString()),
                Convert.ToDecimal(ValorTextBox.Text)
           );

            mora.Detalle.Add(detalle);
            mora.Total += detalle.Valor;

            Actualizar();

            PrestamoComboBox.SelectedIndex = -1;
            ValorTextBox.Clear();
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            if(DetalleDataGrid.Items.Count > 0 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
            {
                MorasDetalle detalle = (MorasDetalle)DetalleDataGrid.SelectedItem;

                mora.Total -= detalle.Valor;
                mora.Detalle.Remove(detalle);
                
                Actualizar();
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if(!ValidarMora())
                return;

            bool paso = false;

            if (string.IsNullOrWhiteSpace(IdTextBox.Text) || IdTextBox.Text == "0")
                paso = MorasBLL.Guardar(mora);

           
            else
            {
                if(!ExisteDB())
                {
                    MessageBox.Show(" la mora porque no existe.", "Error al modificar ", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                paso = MorasBLL.Modificar(mora);
            }

            if(paso)
            {
                Limpiar();
                MessageBox.Show("Mora guardada ", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Revise los datos e intente de nuevo", "Error al Guardar", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id = Utilidades.ToInt(IdTextBox.Text);

            Limpiar();

            if (MorasBLL.Eliminar(id))
                MessageBox.Show("Mora eliminada", "", MessageBoxButton.OK, MessageBoxImage.Information);
            
        }

        private bool ValidarDetalle()
        {
            if(!ValorTextBox.Text.All(char.IsNumber))
            {
                MessageBox.Show("Ingrese un valor válido e intente de nuevo", "Error al guardar", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

             if( ValorTextBox.Text.Length  == 0){
                  MessageBox.Show("Ingrese un valor e intente de nuevo", "Error al guardar", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
             }

            if(PrestamoComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un préstamo e intente de nuevo", "Error al guardar", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool ValidarMora()
        {
            DateTime fecha;

            if(!DateTime.TryParse(FechaDatePicker.Text, out fecha))
            {
                MessageBox.Show("Ingrese una fecha e intente de nuevo", "Error al guardar", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if(mora.Detalle.Count == 0)
            {
                MessageBox.Show("Ingrese  un préstamo e intente de nuevo", "Error al guardar", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
     }
}
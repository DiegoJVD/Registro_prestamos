using System;
using System.Windows;
using Registro_prestamos.DAL;
using Registro_prestamos.Entidades;
using Registro_prestamos.BLL;
using Registro_prestamos.UI.Consulta;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Registro_prestamos.UI.Consulta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class cPrestamo : Window
    {
        public cPrestamo()
        {
            InitializeComponent();
            
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
             var listado = new List<Prestamo>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0: 
                        listado = PrestamoBLL.GetList(e => e.PrestamoId == this.ToInt(CriterioTextBox.Text));
                        break;

                    case 1:                     
                        listado = PrestamoBLL.GetList(e => e.PersonaId == this.ToInt(CriterioTextBox.Text));
                        break;
                }
            }
            else
            {
                listado = PrestamoBLL.GetList(c => true);
            }          

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        } 
        public int ToInt(string valor)
        {
            int retorno = 0;

            int.TryParse(valor,out retorno);

            return retorno;
        }



    }
}

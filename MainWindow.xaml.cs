using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Registro_prestamos.UI.Registro;
using Registro_prestamos.UI.Consulta;

namespace Registro_prestamos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

         public void rPersonaMenuItem_CLick(object render, RoutedEventArgs e)
        {
            rPersona registroPersona = new rPersona();
            registroPersona.Show();
        }

         public void rPrestamoMenuItem_CLick(object render, RoutedEventArgs e)
        {
            rPrestamo registroPrestamo = new rPrestamo();
            registroPrestamo.Show();
        }

         public void cPrestamoMenuItem_CLick(object render, RoutedEventArgs e)
        {
            cPrestamo ConsultaPrestamo = new cPrestamo();
            ConsultaPrestamo.Show();
        }

        public void cPersonaMenuItem_CLick(object render, RoutedEventArgs e)
        {
            cPersona ConsultaPersona = new cPersona();
            ConsultaPersona.Show();
        }
    }
}

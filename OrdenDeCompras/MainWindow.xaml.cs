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
using OrdenDeCompras.UI.RegistroCliente;
using OrdenDeCompras.UI.RegistroProducto;
using OrdenDeCompras.UI.RegistroOrden;
using OrdenDeCompras.Entidades;
using OrdenDeCompras.BLL;

namespace OrdenDeCompras
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

        private void ClienteButton_Click(object sender, RoutedEventArgs e)
        {
            rCliente c = new rCliente();
            c.Show();
        }

        private void ProductoButton_Click(object sender, RoutedEventArgs e)
        {
            rProducto p = new rProducto();
            p.Show();
        }

        private void OrdenButton_Click(object sender, RoutedEventArgs e)
        {
            List<Productos> productos = ProductosBLL.GetList(p => true);

            if(productos.Count>0)
            {
                rOrden o = new rOrden();
                o.Show();
            }
            else
            {
                MessageBox.Show("Se necesitan productos para hacer una orden");
            }
        }
    }
}

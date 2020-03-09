using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OrdenDeCompras.Entidades;
using OrdenDeCompras.BLL;
using OrdenDeCompras.Validaciones;

namespace OrdenDeCompras.UI.RegistroProducto
{
    /// <summary>
    /// Interaction logic for rProducto.xaml
    /// </summary>
    public partial class rProducto : Window
    {
        Productos producto = new Productos();
        public rProducto()
        {
            InitializeComponent();
            this.DataContext = producto;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (producto.ProductoId == 0)
            {
                paso = ProductosBLL.Guardar(producto);
            }
            else
            {
                if (existeEnLaBaseDeDatos())
                    paso =ProductosBLL.Modificar(producto);
                else
                {
                    MessageBox.Show("No se puede modificar un cliente que no existe");
                    return;
                }
            }

            if (paso)
            {
                limpiar();
                MessageBox.Show("Guardado");
            }
            else
            {
                MessageBox.Show("No se pudo guardar");
            }
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Productos productoAnterior = ProductosBLL.Buscar(producto.ProductoId);

            if (productoAnterior != null)
            {
                producto = productoAnterior;
                reCargar();
            }
            else
            {
                MessageBox.Show("Persona no encontrada");
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductosBLL.Eliminar(producto.ProductoId))
            {
                MessageBox.Show("Eliminado");
                limpiar();
            }
            else
            {
                MessageBox.Show("No se pudo eliminar una persona que no existe");
            }
        }

        private void limpiar()
        {
            ProductoIdTextBox.Text = "0";
            DescripcionTextBox.Text = string.Empty;
            PrecioTextBox.Text = "1";
        }

        private bool existeEnLaBaseDeDatos()
        {
            Productos productoAnterior = ProductosBLL.Buscar(producto.ProductoId);

            return productoAnterior != null;
        }

        private void reCargar()
        {
            this.DataContext = null;
            this.DataContext = producto;
        }
    }
}

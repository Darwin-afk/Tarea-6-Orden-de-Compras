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

namespace OrdenDeCompras.UI.RegistroOrden
{
    /// <summary>
    /// Interaction logic for rOrden.xaml
    /// </summary>
    public partial class rOrden : Window
    {
        Contenedor contenedor = new Contenedor();
        public rOrden()
        {
            InitializeComponent();
            this.DataContext = contenedor;

            obtenerProductos();
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            contenedor.orden.ClienteId = contenedor.cliente.ClienteId;

            contenedor.orden.OrdenId = Convert.ToInt32(OrdenIdComboBox.SelectedItem);

            if (contenedor.orden.OrdenId == 0)
                paso = OrdenesBLL.Guardar(contenedor.orden);
            else
            {
                if (existeEnLaBaseDeDatos())
                    paso = OrdenesBLL.Modificar(contenedor.orden);
                else
                {
                    MessageBox.Show("No se puede modificar una Orden que no existe");
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
            Clientes cliente = ClientesBLL.Buscar(contenedor.cliente.ClienteId);

            if(cliente != null)
            {
                contenedor.cliente = cliente;
                obtenerOrdenes(contenedor.cliente.ClienteId);
                reCargar();
            }
            else
            {
                MessageBox.Show("Cliente no encontrada");
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(OrdenIdComboBox.SelectedItem);

            limpiar();

            if(id > 0)
            {
                OrdenesBLL.Eliminar(id);
            }
            else
                MessageBox.Show("No se puede eliminar una Orden que no existe");
        }

        private void MasButton_Click(object sender, RoutedEventArgs e)
        {
            int precio = obtenerPrecio();

            contenedor.orden.Detalle.Add(new OrdenesDetalle(Convert.ToInt32(ProductoIdComboBox.SelectedItem), contenedor.detalle.Cantidad, precio));

            contenedor.orden.Monto += contenedor.detalle.Cantidad * precio;

            reCargar();

            CantidadTextBox.Clear();
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.Items.Count > 0 && DetalleDataGrid.SelectedIndex < DetalleDataGrid.Items.Count - 1)
            {
                contenedor.orden.Detalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                reCargar();
            }
        }

        private void limpiar()
        {
            reiniciaOrdenes();
            contenedor = new Contenedor();

            reCargar();
        }

        private void reiniciaOrdenes()
        {
            OrdenIdComboBox.Items.Clear();
            OrdenIdComboBox.Items.Add("0");
            OrdenIdComboBox.SelectedIndex = 0;
        }

        private void obtenerOrdenes(int id)
        {
            reiniciaOrdenes();
            List<Ordenes> lista = OrdenesBLL.GetList(o => o.ClienteId == id);
            foreach(var item in lista)
            {
                OrdenIdComboBox.Items.Add(item.OrdenId);
            }
        }

        private void obtenerProductos()
        {
            List<Productos> productos = ProductosBLL.GetList(p => true);

            foreach(var item in productos)
            {
                ProductoIdComboBox.Items.Add(item.ProductoId);
            }
        }

        private int obtenerPrecio()
        {
            Productos producto = ProductosBLL.Buscar(Convert.ToInt32(ProductoIdComboBox.SelectedItem));

            return producto.Precio;
        }

        private bool existeEnLaBaseDeDatos()
        {
            Ordenes orden = OrdenesBLL.Buscar(Convert.ToInt32(OrdenIdComboBox.SelectedItem));

            return (orden.ClienteId == contenedor.cliente.ClienteId);
        }

        private void reCargar()
        {
            this.DataContext = null;
            this.DataContext = contenedor;
        }

        private class Contenedor
        {
            public Ordenes orden { get; set; }
            public Clientes cliente { get; set; }
            public OrdenesDetalle detalle { get; set; }

            public Contenedor()
            {
                orden = new Ordenes();
                cliente = new Clientes();
                detalle = new OrdenesDetalle();
            }
        }

        private void OrdenIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DetalleDataGrid  != null)
            {
                if (OrdenIdComboBox.SelectedIndex == 0)
                {
                    contenedor.orden = new Ordenes();
                    reCargar();
                }
                else
                {
                    contenedor.orden = OrdenesBLL.Buscar(Convert.ToInt32(OrdenIdComboBox.SelectedItem));
                    reCargar();
                }
            }
        }
    }
}

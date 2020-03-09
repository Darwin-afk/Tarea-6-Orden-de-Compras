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

namespace OrdenDeCompras.UI.RegistroCliente
{
    /// <summary>
    /// Interaction logic for rCliente.xaml
    /// </summary>
    public partial class rCliente : Window
    {
        Clientes cliente = new Clientes();
        public rCliente()
        {
            InitializeComponent();
            this.DataContext = cliente;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if(cliente.ClienteId==0)
            {
                paso = ClientesBLL.Guardar(cliente);
            }
            else
            {
                if (existeEnLaBaseDeDatos())
                    paso = ClientesBLL.Modificar(cliente);
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
            Clientes clienteAnterior = ClientesBLL.Buscar(cliente.ClienteId);

            if (clienteAnterior != null)
            {
                cliente = clienteAnterior;
                reCargar();
            }
            else
            {
                MessageBox.Show("Persona no encontrada");
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientesBLL.Eliminar(cliente.ClienteId))
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
            ClienteIdTextBox.Text = "0";
            NombreTextBox.Text = string.Empty;
        }

        private bool existeEnLaBaseDeDatos()
        {
            Clientes clienteAnterior = ClientesBLL.Buscar(cliente.ClienteId);

            return clienteAnterior != null;
        }

        private void reCargar()
        {
            this.DataContext = null;
            this.DataContext = cliente;
        }
    }
}

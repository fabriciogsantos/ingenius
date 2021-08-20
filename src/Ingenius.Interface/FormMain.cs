using System;
using System.Windows.Forms;

namespace Ingenius.Interface
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            
            menuMain.Items.Add("Ingreso",null, MenuAdd);
            menuMain.Items.Add("Retiro",null, MenuRemove);
            menuMain.Items.Add("Consulta de producto", null, MenuBusqueda);
            menuMain.Items.Add("Consulta Stock Actual", null, MenuStock);
            menuMain.Items.Add("Registrar Usuario", null, MenuUsuario);
        }

        private void MenuAdd(Object sender, EventArgs e)
        {
            var formAdd =new FormIngreso();
            formAdd.ShowDialog();
            formAdd.Dispose();
        }

        private void MenuRemove(Object sender, EventArgs e)
        {
            var formRemove = new FormRemove();
            formRemove.ShowDialog();
            formRemove.Dispose();
        }

        private void MenuBusqueda(Object sender, EventArgs e)
        {
            var formBusqueda = new FormBusqueda();
            formBusqueda.ShowDialog();
            formBusqueda.Dispose();
        }

        private void MenuStock(Object sender, EventArgs e)
        {
            var formStock = new FormStock();
            formStock.ShowDialog();
            formStock.Dispose();
        }

        private void MenuUsuario(Object sender, EventArgs e)
        {
            var formUsuario = new FormUsuario();
            formUsuario.ShowDialog();
            formUsuario.Dispose();
        }
    }
}

using Ingenius.Data.Context;
using Ingenius.Data.Repositories;
using Ingenius.Domain;
using PredicateExtensions.Core;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Ingenius.Interface
{
    public partial class FormBusqueda : Form
    {
        IngeniusContext _repository = new IngeniusContext();
        public FormBusqueda()
        {
            InitializeComponent();

            lblIngreso.Text = string.Empty;
            lblRetiro.Text = string.Empty;
            lblStock.Text = string.Empty;

            var sizeRepository = new SizeProductRepository(_repository);
            cbSize.Items.AddRange(sizeRepository.GeetAll().Select(s => s.Size).ToArray());

            this.FormClosing += FormIngreso_FormClosing;
            this.txtCode.KeyPress += TxtCode_KeyPress;
        }

        private void TxtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }
        private void FormIngreso_FormClosing(object sender, FormClosingEventArgs e)
        {
            _repository.Dispose();
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            var productRepository = new ProductRepository(_repository);
            var productos= productRepository.GeetAll(BusquedaFilter())
                .Select(s => new
                {
                    s.Code,
                    s.Name,
                    s.Amount,
                    s.Size,
                    s.Date,
                    s.User,
                    action = s.Action == Actions.Add ? "INGRESO" : "RETIRO"
                }).ToList();

            dataGridView1.DataSource = productos;
            var ingreso = productos.Where(p=>p.action == "INGRESO").Sum(p=>p.Amount);
            var retiro = productos.Where(p => p.action == "RETIRO").Sum(p => p.Amount);
            dataGridView1.Columns[0].HeaderCell.Value = "Código";
            dataGridView1.Columns[1].HeaderCell.Value = "Nombre";
            dataGridView1.Columns[2].HeaderCell.Value = "Cantidad";
            dataGridView1.Columns[3].HeaderCell.Value = "Tamaño";
            dataGridView1.Columns[4].HeaderCell.Value = "Fecha";
            dataGridView1.Columns[5].HeaderCell.Value = "Usuario";
            dataGridView1.Columns[6].HeaderCell.Value = "Accion";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if ((string)row.Cells[6].Value == "RETIRO")
                    row.DefaultCellStyle.BackColor = Color.IndianRed;
            }
            lblIngreso.Text = ingreso.ToString("N0");
            lblRetiro.Text = retiro.ToString("N0");
            lblStock.Text = (ingreso - retiro).ToString("N0");
        }

        private Expression<Func<Product, bool>> BusquedaFilter()
        {
            var expr = PredicateExtensions.Core.PredicateExtensions.Begin<Product>(true);
            if (!String.IsNullOrWhiteSpace(txtCode.Text))
                expr = expr.And(s=>s.Code==txtCode.Text);

            if (!String.IsNullOrWhiteSpace(txtName.Text))
                expr = expr.And(s => s.Name.ToLower().Contains(txtName.Text.ToLower()));

            if (!String.IsNullOrWhiteSpace(cbSize.SelectedItem?.ToString()))
                expr = expr.And(s => s.Size== cbSize.SelectedItem.ToString());

            if (!String.IsNullOrWhiteSpace(cbAccion.SelectedItem?.ToString()))
            {
                var accion = cbAccion.SelectedItem.ToString() == "INGRESO" ? Actions.Add : Actions.Remove;
                expr = expr.And(s => s.Action == accion);
            }

            expr = expr.And(s => s.Date >= dtDateBegin.Value.Date && s.Date <= dtDateEnd.Value.Date.AddDays(1));

            return expr;
        }
        
    }
}

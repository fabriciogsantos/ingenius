using Ingenius.Data.Context;
using Ingenius.Data.Repositories;
using Ingenius.Domain;
using PredicateExtensions.Core;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace Ingenius.Interface
{
    public partial class FormStock : Form
    {
        IngeniusContext _repository = new IngeniusContext();

        public FormStock()
        {
            InitializeComponent();

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
            var productRepository = new InventoryRepository(_repository);
            var productos = productRepository.GeetAll(BusquedaFilter())
                .Select(s => new
                {
                    s.Code,
                    s.Size,
                    s.Amount
                }).ToList();

            dataGridView1.DataSource = productos;
            dataGridView1.Columns[0].HeaderCell.Value = "Código";
            dataGridView1.Columns[1].HeaderCell.Value = "Tamaño";
            dataGridView1.Columns[2].HeaderCell.Value = "Cantidad";

        }

        private Expression<Func<Inventory, bool>> BusquedaFilter()
        {
            var expr = PredicateExtensions.Core.PredicateExtensions.Begin<Inventory>(true);
            if (!String.IsNullOrWhiteSpace(txtCode.Text))
                expr = expr.And(s => s.Code == txtCode.Text);
            if (!String.IsNullOrWhiteSpace(cbSize.SelectedItem?.ToString()))
                expr = expr.And(s => s.Size == cbSize.SelectedItem.ToString());

            return expr;
        }
    }
}

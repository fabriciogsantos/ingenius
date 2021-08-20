using Ingenius.Data.Context;
using Ingenius.Data.Repositories;
using Ingenius.Domain;
using Ingenius.Services;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Ingenius.Interface
{
    public partial class FormIngreso : Form
    {
        IngeniusContext _repository = new IngeniusContext();
        
        public FormIngreso()
        {
            InitializeComponent();
            var sizeRepository = new SizeProductRepository(_repository);
            cbSize.Items.AddRange(sizeRepository.GeetAll().Select(s=>s.Size).ToArray());

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            var ProductService = new ProductService();

            var product = new Product
            {
                Code = txtCode.Text,
                Name = txtName.Text,
                Amount = nbCount.Value,
                Size = cbSize.SelectedItem?.ToString() ?? string.Empty,
                Date = dtDateAdd.Value,
                Action = Actions.Add
            };

            var response = ProductService.Add(product);

            MessageBox.Show(response.Message,response.Title,MessageBoxButtons.OK , (System.Windows.Forms.MessageBoxIcon) response.MessageBoxIcon);

            if ((System.Windows.Forms.MessageBoxIcon)response.MessageBoxIcon == System.Windows.Forms.MessageBoxIcon.Information)
            {
                txtCode.Text = string.Empty;
                txtName.Text = string.Empty;
                nbCount.Value = 0;
            }

            ProductService.Dispose();

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _repository.Dispose();
            this.Close();
        }
    }
}

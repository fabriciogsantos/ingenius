using Ingenius.Data.Context;
using Ingenius.Data.Repositories;
using Ingenius.Domain;
using Ingenius.Services;
using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ingenius.Interface
{
    public partial class FormLogin : Form
    {
        IngeniusContext _repository = new IngeniusContext();

        public FormLogin()
        {
            InitializeComponent();
            this.FormClosing += FormIngreso_FormClosing;
            this.txtLogin.KeyPress += TxtCode_KeyPress;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var userRepository = new UserRepository(_repository);
            var password= Convert.ToBase64String(Encoding.ASCII.GetBytes(txtPassword.Text));
            var user = userRepository.GeetAll(u => u.Login == txtLogin.Text && u.Password == password).FirstOrDefault();

            if (user == default) {
                MessageBox.Show("Login o Clave incorrecto", "Falla", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            
            UserActual.userActual = user;
            this.Hide();
            _repository.Dispose();

            var fromMain = new FormMain();
            fromMain.Show();
            

        }

        private void TxtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void FormIngreso_FormClosing(object sender, FormClosingEventArgs e)
        {
            _repository.Dispose();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            _repository.Dispose();
            Application.Exit();
        }
    }
}

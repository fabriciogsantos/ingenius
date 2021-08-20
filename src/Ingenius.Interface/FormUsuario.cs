using Ingenius.Data.Context;
using Ingenius.Data.Repositories;
using Ingenius.Domain;
using Ingenius.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Ingenius.Interface
{
    public partial class FormUsuario : Form
    {
        IngeniusContext _repository = new IngeniusContext();
        bool UserNew = true;
        public FormUsuario()
        {
            InitializeComponent();

            this.FormClosing += FormIngreso_FormClosing;
            this.txtLogin.KeyPress += TxtCode_KeyPress;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var userService = new UserService();
            var userEdit = userService.GetByLogin(txtLogin.Text);

            if (userEdit!=null && UserNew)
            {
                MessageBox.Show("Usuario ya Registrado", "Error", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                userService.Dispose();
                return;
            }
                      
            var user = new User
            {
                Login = txtLogin.Text,
                Name = txtName.Text,
               Password= txtPassword.Text,
               CheckPassword= txtCheckPassword.Text
            };

            MessageNotification response = null;

            if (UserNew)
                response = userService.Add(user);
            else
            {
                user.Id = GetUserEdit();
                response = userService.Update(user);
            }

            MessageBox.Show(response.Message, response.Title, MessageBoxButtons.OK, (System.Windows.Forms.MessageBoxIcon)response.MessageBoxIcon);

            if ((System.Windows.Forms.MessageBoxIcon)response.MessageBoxIcon == System.Windows.Forms.MessageBoxIcon.Information)
            {
                txtLogin.Text = string.Empty;
                txtName.Text = string.Empty;
                txtName.Text = string.Empty;
                txtCheckPassword.Text = string.Empty;
                txtLogin.Enabled = true;
            }

            userService.Dispose();
            UserNew = true;

        }

        private void TxtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void FormIngreso_FormClosing(object sender, FormClosingEventArgs e)
        {
            _repository.Dispose();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _repository.Dispose();
            this.Close();
        }

        private void FormUsuario_Load(object sender, EventArgs e)
        {
            var userRepository = new UserRepository(_repository);
            var usuarios = userRepository.GeetAll()
                .Select(s => new
                {
                    s.Id,
                    s.Login,
                    s.Name
                }).ToList();

            dataGridView1.DataSource = usuarios;
            dataGridView1.Columns[0].Visible= false;
            dataGridView1.Columns[1].HeaderCell.Value = "Login";
            dataGridView1.Columns[2].HeaderCell.Value = "Nombre";
            userRepository.Dispose();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                 var userService = new UserService();
                var user = userService.GetById(GetUserEdit());
                if (user == null)
                {
                    MessageBox.Show("Usuario no Localizado", "Error", MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }

                UserNew = false;
                txtLogin.Enabled = false;
                txtLogin.Text = user.Login;
                txtName.Text = user.Name;
                dataGridView1.Enabled = false;
                userService.Dispose();
            }
        }

        private Guid GetUserEdit()
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                return Guid.Parse(selectedRow.Cells[0].Value.ToString());
            }
            return Guid.Empty;
        }
    }
}

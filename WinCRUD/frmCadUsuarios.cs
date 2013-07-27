using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Repository;
using Repository.Entities;

namespace WinCRUD
{
    public partial class frmCadUsuarios : Form
    {
        int IDRegistro = 0;

        private enum Tipo
        { 
            insert,
            update,
            reload
        }

        private Tipo _tipo;

        private UsuariosRepository usuariosRepository;        

        private void CarregarRegistros()
        {
            if (usuariosRepository == null)
                usuariosRepository = new UsuariosRepository();

            grvDados.DataSource = usuariosRepository.Consultar();
        }

        private void Limpar()
        {
            IDRegistro    = 0;
            txtSenha.Text = string.Empty;
            txtNome.Text  = string.Empty;
            txtLogin.Text = string.Empty;
        }

        private void HabilitaDesabilitaBotoes()
        {
            btnSalvar.Enabled    = _tipo == Tipo.insert || _tipo == Tipo.update;
            btnAlterar.Enabled   = grvDados.Rows.Count > 0 && _tipo == Tipo.reload;
            btnExcluir.Enabled   = grvDados.Rows.Count > 0 && _tipo == Tipo.reload;
            btnCadastrar.Enabled = _tipo == Tipo.reload;
            btnCancelar.Enabled  = btnSalvar.Enabled || btnAlterar.Enabled;
        }

        public frmCadUsuarios()
        {
            InitializeComponent();            
        }

        private void frmCadUsuarios_Load(object sender, EventArgs e)
        {
            
        }        

        private void button1_Click_1(object sender, EventArgs e)
        {
            CarregarRegistros();
            _tipo = Tipo.reload;
            HabilitaDesabilitaBotoes();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            _tipo = Tipo.insert;
            groupBox2.Enabled = true;
            txtNome.Focus();
            HabilitaDesabilitaBotoes();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (grvDados.RowCount > 0)
            {
                try
                {
                    usuariosRepository = new UsuariosRepository();
                    Usuarios model     = usuariosRepository.RetornarPorId(Convert.ToInt32(grvDados.CurrentRow.Cells[0].Value));
                    usuariosRepository.Excluir(model);
                    CarregarRegistros();
                    MessageBox.Show("Registro excluído com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir usuário: " + ex.Message, "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            if (txtNome.Text == "")
            {
                errorProvider1.SetError(txtNome, "Informe o campo nome.");
                return;
            }

            if (txtLogin.Text == "")
            {
                errorProvider1.SetError(txtLogin, "Informe o campo login.");
                return;
            }

            if (txtSenha.Text == "")
            {
                errorProvider1.SetError(txtSenha, "Informe o campo senha.");
                return;
            }

            usuariosRepository = new UsuariosRepository();
            if (_tipo == Tipo.insert)
            {
                if (usuariosRepository.ValidaLogin(txtLogin.Text.Trim()))
                {
                    MessageBox.Show("Login encontra-se cadastrado.", "Anteção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                Usuarios usuariosModel = new Usuarios();
                usuariosModel.login    = txtLogin.Text;
                usuariosModel.nome     = txtNome.Text;
                usuariosModel.senha    = txtSenha.Text;
                usuariosModel.status   = 'A';
                if (_tipo == Tipo.insert)
                {
                    usuariosRepository.Inserir(usuariosModel);
                    MessageBox.Show("Registro cadastrado com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNome.Focus(); 
                }
                else if (_tipo == Tipo.update)
                {
                    usuariosModel.id = IDRegistro;
                    usuariosRepository.Alterar(usuariosModel);
                    MessageBox.Show("Registro atualizado com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    groupBox2.Enabled = false;
                    _tipo = Tipo.reload;
                }                
                CarregarRegistros();
                Limpar();                          
                HabilitaDesabilitaBotoes();
            }
            catch (Exception ex)
            {
                if (_tipo == Tipo.insert)
                    MessageBox.Show("Erro ao cadastrar usuário: "+ex.Message,"Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
                if (_tipo == Tipo.update)
                    MessageBox.Show("Erro ao atualizar usuário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            _tipo = Tipo.update;
            groupBox2.Enabled = true;
            HabilitaDesabilitaBotoes();
            IDRegistro        = Convert.ToInt32(grvDados.CurrentRow.Cells[0].Value);
            txtLogin.Text     = grvDados.CurrentRow.Cells[2].Value.ToString();
            txtNome.Text      = grvDados.CurrentRow.Cells[1].Value.ToString();
            txtSenha.Text     = grvDados.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            _tipo = Tipo.reload;
            Limpar();
            HabilitaDesabilitaBotoes();
            groupBox2.Enabled = false;
        }
    }
}

using Repository;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinCRUD
{
    public partial class frmCadContatos : Form
    {
        int IDRegistro = 0;

        private enum Tipo
        {
            insert,
            update,
            reload
        }

        private Tipo _tipo;
        private ContatosRepository contatosRepository;

        private void CarregarRegistros()
        {
            if (contatosRepository == null)
                contatosRepository = new ContatosRepository();

            grvDados.DataSource = contatosRepository.Consultar();
        }

        private void Limpar()
        {
            IDRegistro = 0;            
        }

        private void HabilitaDesabilitaBotoes()
        {
            btnSalvar.Enabled    = _tipo == Tipo.insert || _tipo == Tipo.update;
            btnAlterar.Enabled   = grvDados.Rows.Count > 0 && _tipo == Tipo.reload;
            btnExcluir.Enabled   = grvDados.Rows.Count > 0 && _tipo == Tipo.reload;
            btnCadastrar.Enabled = _tipo == Tipo.reload;
            btnCancelar.Enabled  = btnSalvar.Enabled || btnAlterar.Enabled;
        }

        public frmCadContatos()
        {
            InitializeComponent();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            _tipo = Tipo.insert;
            groupBox2.Enabled = true;
            txtNome.Focus();
            HabilitaDesabilitaBotoes();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            if (txtNome.Text == "")
            {
                errorProvider1.SetError(txtNome, "Informe o campo nome.");
                return;
            }

            contatosRepository = new ContatosRepository();
            if (_tipo == Tipo.insert)
            {
                if (contatosRepository.RegistroCadastrado(txtNome.Text.Trim()))
                {
                    MessageBox.Show("Encontra-se um contato com o mesmo nome.", "Anteção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                Contatos contatosModel = new Contatos();
                contatosModel.nome     = txtNome.Text.Trim();
                contatosModel.bairro   = txtBairro.Text.Trim();
                contatosModel.cep      = txtCEP.Text.Trim();
                contatosModel.cidade   = txtCidade.Text.Trim();
                contatosModel.endereco = txtEndereco.Text.Trim();
                contatosModel.numero   = txtNumero.Text.Trim();
                contatosModel.status   = "A";
                contatosModel.estado   = cbxEstados.Text;

                if (_tipo == Tipo.insert)
                {
                    contatosRepository.Inserir(contatosModel);
                    MessageBox.Show("Registro cadastrado com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNome.Focus();
                }
                else if (_tipo == Tipo.update)
                {
                    contatosModel.id = IDRegistro;
                    contatosRepository.Alterar(contatosModel);
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
                    MessageBox.Show("Erro ao cadastrar contato: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (_tipo == Tipo.update)
                    MessageBox.Show("Erro ao atualizar contato: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            _tipo = Tipo.update;
            groupBox2.Enabled = true;
            HabilitaDesabilitaBotoes();
            IDRegistro         = Convert.ToInt32(grvDados.CurrentRow.Cells[0].Value);
            contatosRepository = new ContatosRepository();
            Contatos model     = contatosRepository.RetornarPorId(IDRegistro);

            txtNome.Text            = model.nome;
            txtBairro.Text          = model.bairro;
            txtCEP.Text             = model.cep;
            txtCidade.Text          = model.cidade;
            txtNumero.Text          = model.numero;
            txtEndereco.Text        = model.endereco;
            cbxEstados.SelectedItem = model.estado;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (grvDados.RowCount > 0)
            {
                try
                {
                    contatosRepository = new ContatosRepository();
                    Contatos model     = contatosRepository.RetornarPorId(Convert.ToInt32(grvDados.CurrentRow.Cells[0].Value));
                    contatosRepository.Excluir(model);
                    CarregarRegistros();
                    MessageBox.Show("Registro excluído com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir contato: " + ex.Message, "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            _tipo = Tipo.reload;
            Limpar();
            HabilitaDesabilitaBotoes();
            groupBox2.Enabled = false;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarRegistros();
            _tipo = Tipo.reload;
            HabilitaDesabilitaBotoes();
        }

        private void grvDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

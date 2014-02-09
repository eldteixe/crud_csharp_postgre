using Repository;
using Repository.Entities;
using System;
using System.Windows.Forms;
using WinCRUD.Helper;

namespace WinCRUD
{
    public partial class frmTelefones : Form
    {
        private static TelefonesRepository repositorioTelefones = new TelefonesRepository();

        public frmTelefones()
        {
            InitializeComponent();
        }

        private void frmTelefones_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void frmTelefones_Load(object sender, EventArgs e)
        {
            ContatosRepository repositorioContatos = new ContatosRepository();            
            cbxContatos.DataSource                 = repositorioContatos.Consultar();
            cbxContatosPesquisa.DataSource         = repositorioContatos.Consultar();
            cbxTipo.SelectedIndex = 0;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            errorProvider1.SetError(txtTelefone, "");
            if (txtTelefone.Text.Replace("_", "").Replace("(", "").Replace(")", "").Replace("-", "").Trim().Length < 10)            
            {
                errorProvider1.SetError(txtTelefone, "Telefone inválido.");
                return;
            }
            if (!string.IsNullOrEmpty(txtEmail.Text) && !Funcoes.validarEmail(txtEmail,errorProvider1))
            {
                return;
            }
            if (!string.IsNullOrEmpty(txtSite.Text) && !Funcoes.validarURL(txtSite, errorProvider1))
            {
                return;
            }
            try
            {
                TelefonesRepository repositorioTelefone = new TelefonesRepository();
                Telefones telefoneModel                 = new Telefones();

                telefoneModel.email       = txtEmail.Text.Trim();
                telefoneModel.id_contatos = Convert.ToInt32(cbxContatos.SelectedValue);
                telefoneModel.site        = txtSite.Text.Trim();
                telefoneModel.telefone    = txtTelefone.Text.Trim();
                telefoneModel.tipo        = cbxTipo.Text;
                repositorioTelefone.Inserir(telefoneModel);
                Limpar();
                MessageBox.Show("Registro cadastro com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);                            
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar telefone: "+ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);                            
            }            
        }

        private void Limpar()
        {
            foreach (var item in groupBox1.Controls)
            {
                if (item is TextBox)
                    (item as TextBox).Text = string.Empty;
                if (item is MaskedTextBox)
                    (item as MaskedTextBox).Text = string.Empty;
            }
            cbxContatos.SelectedIndex = 0;
            cbxTipo.SelectedIndex     = 0;
            cbxContatos.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            dgvContatos.DataSource = 
                repositorioTelefones.RetornarPorContato(Convert.ToInt32(cbxContatosPesquisa.SelectedValue));            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvContatos.RowCount > 0)
            {
                if (MessageBox.Show("Deseja realmente excluir?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == System.Windows.Forms.DialogResult.Yes)
                {                    
                    int codigo = Convert.ToInt32(dgvContatos.CurrentRow.Cells[0].Value.ToString());
                    Telefones telefoneModel = repositorioTelefones.RetornarPorId(codigo);
                    try
                    {
                        repositorioTelefones.Excluir(telefoneModel);
                        MessageBox.Show("Registro excluído com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button1_Click(sender,e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao excluir: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else 
            {
                MessageBox.Show("Não há registro(s) para excluir!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }       
    }
}

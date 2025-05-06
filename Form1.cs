using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Projeto_DotNetAppWin
{
    public partial class Frm_HelloActions : Form
    {
        public Frm_HelloActions()
        {
        InitializeComponent();
        }
        private async Task EnviarDadosParaAPIAsync(string titulo, string descricao)
        {
            string apiUrl = "https://api.github.com/repos/AndressaFontes/Projeto-DotNetAppWin/issues\r\n"; // Substitua pela URL da API
            string token = Environment.GetEnvironmentVariable("AGITHUB_TOKEN");

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"token {token}");
                client.DefaultRequestHeaders.Add("User-Agent", "DotNetAppWin");

                var issueData = new
                {
                    title = titulo,
                    body = descricao
                };

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(issueData);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Issue criada com sucesso no GitHub!");
                    }
                    else
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Erro ao criar a issue: {responseContent}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro: {ex.Message}");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblMensagem.Text = "Formulário para reportar bugs na nova pipeline do GitHub Actions da Avanade.";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void lblMensagem_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private bool isSecondClick = false; // Variável de controle para rastrear o estado do botão

        private async void button1_Click_1(object sender, EventArgs e)
        {
            if (!isSecondClick)
            {
                // Primeiro clique: Configurar o formulário
                btnEnviar.Text = "Enviar";
                lblMensagem.Visible = false;

                txtTitulo.Visible = true;
                txtTitulo.Enabled = true;
                

                txtDescricao.Visible = true;
                txtDescricao.Enabled = true;

                isSecondClick = true; // Alterar o estado para o segundo clique
            }
            else
            {
                // Segundo clique: Enviar os dados para a API
                string titulo = txtTitulo.Text;
                string descricao = txtDescricao.Text;

                if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(descricao))
                {
                    MessageBox.Show("Por favor, preencha o título e a descrição antes de enviar.");
                    return;
                }

                await EnviarDadosParaAPIAsync(titulo, descricao);

                // Resetar o estado do botão após o envio
                btnEnviar.Text = "Concluir";
                lblMensagem.Visible = true;

                txtTitulo.Visible = false;
                txtTitulo.Enabled = false;

                txtDescricao.Visible = false;
                txtDescricao.Enabled = false;

                isSecondClick = false;
            }
        }


        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private void txtTitulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtDescricao.Focus();
        }

        private void CentralizarControles()
        {
            foreach (Control controle in this.Controls)
            {
                // Centraliza horizontalmente
                controle.Left = (this.ClientSize.Width - controle.Width) / 2;

                // Centraliza verticalmente (opcional, dependendo do layout desejado)
                // Aqui, os controles são distribuídos verticalmente com espaçamento
                int index = this.Controls.GetChildIndex(controle);
                controle.Top = (this.ClientSize.Height / (this.Controls.Count + 1)) * (index + 1) - (controle.Height / 2);
            }
        }

        private void Frm_HelloActions_Resize(object sender, EventArgs e)
        {
           CentralizarControles();
        
        }
    }
}

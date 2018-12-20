using System;
using Android.App;
using Android.OS;
using Android.Widget;
using System.IO;
using System.Text;
using SQLite;
using System.Net.Http;
using Newtonsoft.Json;

namespace Projeto.Cliente
{
    [Activity(Label = "Inserir Cliente")]
    public class InserirClienteActivity : Activity
    {
        /// <summary>
        /// Botão salvar cliente no banco de dados
        /// </summary>
        Button btnSalvar;

        /// <summary>
        /// Botão salvar cliente via Json
        /// </summary>
        Button btnSalvarJson;

        /// <summary>
        /// EditText nome do cliente
        /// </summary>
        EditText edtNome;

        /// <summary>
        /// EditText email do cliente
        /// </summary>
        EditText edtEmail;

        /// <summary>
        /// EditText CPF/CNPJ do cliente
        /// </summary>
        EditText edtCpfCnpj;

        /// <summary>
        /// Cria activity
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.InserirCliente);

            edtNome = FindViewById<EditText>(Resource.Id.edt_nome);
            edtEmail = FindViewById<EditText>(Resource.Id.edt_email);
            edtCpfCnpj = FindViewById<EditText>(Resource.Id.edt_cpfCnpj);

            btnSalvar = FindViewById<Button>(Resource.Id.btn_salvar);
            btnSalvarJson = FindViewById<Button>(Resource.Id.btn_salvarJson);

            btnSalvar.Click += BtnSalvar_Click;
            btnSalvarJson.Click += BtnSalvarJson_Click;
        }

        /// <summary>
        /// Evento criar categoria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                // Seleciona o caminho do banco de dados
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "projeto.db3");

                // Cria instância do banco de dados
                var db = new SQLiteConnection(dpPath);

                // Cria tabela cliente
                db.CreateTable<Cliente>();

                Cliente tbl = new Cliente();
                tbl.Nome = edtNome.Text.Trim();
                tbl.Email = edtEmail.Text.Trim();
                tbl.CPFCNPJ = edtCpfCnpj.Text.Trim();

                // Maior que 11 digitos, é pessoa fisica
                if (tbl.CPFCNPJ.Length > 11)
                {
                    tbl.Tipo = "PJ";
                }
                else
                {
                    tbl.Tipo = "PF";
                }

                // Insere os dados
                db.Insert(tbl);

                Clear();

                Toast.MakeText(this, "Cliente adicionado com sucesso...,", ToastLength.Short).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        /// <summary>
        /// Evento criar categoria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnSalvarJson_Click(object sender, EventArgs e)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string tipoPessoa = "PF";
                    
                    // Maior que 11 digitos, é pessoa fisica
                    if (edtCpfCnpj.Text.Length > 11)
                    {
                        tipoPessoa = "PJ";
                    }

                    // Cria um novo cliente
                    var novaCliente = new Cliente
                    {
                        id = 10,
                        Nome = edtNome.Text.Trim(),
                        Email = edtEmail.Text.Trim(),
                        CPFCNPJ = edtCpfCnpj.Text.Trim(),
                        Tipo = tipoPessoa
                    };

                    // Cria o conteudo da requisição e define o tipo Json
                    var json = JsonConvert.SerializeObject(novaCliente);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Envia a requisição POST
                    var uri = "http://localhost:8080/Cliente";

                    var result = await client.PostAsync(uri, content);

                    // Se ocorrer um erro lança uma exceção
                    result.EnsureSuccessStatusCode();

                    // Processa a resposta
                    var resultString = await result.Content.ReadAsStringAsync();
                    var cliente = JsonConvert.DeserializeObject<Cliente>(resultString);

                    // Exibe a saida
                    Toast.MakeText(this, "Cliente adicionado com sucesso... - " + cliente.ToString(), ToastLength.Short).Show();
                }

                Clear();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();
            }
        }

        /// <summary>
        /// Limpa os campos
        /// </summary>
        void Clear()
        {
            edtNome.Text = string.Empty;
            edtEmail.Text = string.Empty;
            edtCpfCnpj.Text = string.Empty;
        }
    }
}
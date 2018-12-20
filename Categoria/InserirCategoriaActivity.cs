using System;
using Android.App;
using Android.OS;
using Android.Widget;
using System.IO;
using System.Text;
using SQLite;
using System.Net.Http;
using Newtonsoft.Json;

namespace Projeto.Categoria
{
    [Activity(Label = "Inserir Categoria")]
    public class InserirCategoriaActivity : Activity
    {
        /// <summary>
        /// Botão salvar categoria no banco de dados
        /// </summary>
        Button btnSalvar;

        /// <summary>
        /// Botão salvar categoria via Json
        /// </summary>
        Button btnSalvarJson;

        /// <summary>
        /// EditText nome da categoria
        /// </summary>
        EditText edtNome;

        /// <summary>
        /// Cria activity
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.InserirCategoria);

            edtNome = FindViewById<EditText>(Resource.Id.edt_nome);
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

                // Cria tabela categoria
                db.CreateTable<Categoria>();

                Categoria tbl = new Categoria();
                tbl.Nome = edtNome.Text.Trim();

                // Insere os dados
                db.Insert(tbl);

                Clear();

                Toast.MakeText(this, "Categoria adicionada com sucesso...,", ToastLength.Short).Show();
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
                    // Cria uma nova categoria
                    var novaCategoria = new Categoria
                    {
                        id = 10,
                        Nome = edtNome.Text.Trim()
                    };
                    
                    // Cria o conteudo da requisição e define o tipo Json
                    var json = JsonConvert.SerializeObject(novaCategoria);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Envia a requisição POST
                    var uri = "http://localhost:8080/Categoria";

                    var result = await client.PostAsync(uri, content);
                    
                    // Se ocorrer um erro lança uma exceção
                    result.EnsureSuccessStatusCode();
                    
                    // Processa a resposta
                    var resultString = await result.Content.ReadAsStringAsync();
                    var categoria = JsonConvert.DeserializeObject<Categoria>(resultString);
                    
                    // Exibe a saida
                    Toast.MakeText(this, "Categoria adicionada com sucesso... - " + categoria.ToString(), ToastLength.Short).Show();
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
        }
    }
}
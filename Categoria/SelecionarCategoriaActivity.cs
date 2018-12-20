using System;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;
using System.IO;
using SQLite;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Projeto.Categoria
{
    [Activity(Label = "Selecionar Categoria")]
    public class SelecionarCategoriaActivity : Activity
    {
        /// <summary>
        /// The BTN selecionar categoria através do banco de dados local
        /// </summary>
        Button btn_selecionarCategoria;

        /// <summary>
        /// The BTN selecionar categoria via Json
        /// </summary>
        Button btn_selecionarCategoriaJson;

        /// <summary>
        /// The text identifier
        /// </summary>
        EditText edt_id;

        /// <summary>
        /// The text nome
        /// </summary>
        TextView txt_nome;

        /// <summary>
        /// Cria activity
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SelecionarCategoria);

            edt_id = FindViewById<EditText>(Resource.Id.edt_id);
            txt_nome = FindViewById<TextView>(Resource.Id.txv_nome);

            btn_selecionarCategoria = FindViewById<Button>(Resource.Id.btn_selecionar);
            btn_selecionarCategoriaJson = FindViewById<Button>(Resource.Id.btn_selecionarJson);

            btn_selecionarCategoria.Click += btn_selecionarCategoria_Click;
            btn_selecionarCategoriaJson.Click += btn_selecionarCategoriaJson_Click;
        }

        /// <summary>
        /// Handles the Click event of the btn_selecionarCategoria control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_selecionarCategoria_Click(object sender, EventArgs e)
        {
            Clear();

            // Seleciona o caminho do banco de dados
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "projeto.db3"); //Call Database  

            // Cria instância do banco de dados
            var db = new SQLiteConnection(dpPath);

            // Seleciona tabela categoria
            var data = db.Table<Categoria>();

            int idvalue = Convert.ToInt32(edt_id.Text.Trim());

            // Seleciona os dados
            var data1 = (from values in data
                         where values.id == idvalue
                         select new Categoria
                         {
                             Nome = values.Nome
                         }).ToList<Categoria>();

            if (data1.Count > 0)
            {
                foreach (var val in data1)
                {
                    txt_nome.Text = val.Nome;
                }
            }
            else
            {
                Toast.MakeText(this, "Categoria não encontrada!", ToastLength.Short).Show();
            }
        }

        /// <summary>
        /// Handles the Click event of the btn_selecionarCategoriaJson control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btn_selecionarCategoriaJson_Click(object sender, EventArgs e)
        {
            Clear();

            using (var client = new HttpClient())
            {
                int idvalue = Convert.ToInt32(edt_id.Text.Trim());

                // Envia a requisição GET
                var uri = "http://localhost:8080/Cliente?Id=" + idvalue;

                var result = await client.GetStringAsync(uri);
                
                // Processa a resposta
                var categorias = JsonConvert.DeserializeObject<List<Categoria>>(result);

                // Gera a saida
                var categoria = categorias.First();
                txt_nome.Text = categoria.Nome;
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear()
        {
            txt_nome.Text = string.Empty;
        }
    }
}
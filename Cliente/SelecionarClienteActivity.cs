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

namespace Projeto.Cliente
{
    [Activity(Label = "Selecionar Cliente")]
    public class SelecionarClienteActivity : Activity
    {
        /// <summary>
        /// The BTN selecionar cliente através do banco de dados local
        /// </summary>
        Button btn_selecionarCliente;

        /// <summary>
        /// The BTN selecionar cliente via Json
        /// </summary>
        Button btn_selecionarClienteJson;

        /// <summary>
        /// The text identifier
        /// </summary>
        EditText edt_id;

        /// <summary>
        /// The text nome
        /// </summary>
        TextView txt_nome;

        /// <summary>
        /// The text email
        /// </summary>
        TextView txt_email;

        /// <summary>
        /// The text CPF/CNPJ
        /// </summary>
        TextView txt_cpfCnpj;

        /// <summary>
        /// The text tipo do cliente
        /// </summary>
        TextView txt_tipoCliente;

        /// <summary>
        /// Cria activity
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SelecionarCliente);

            edt_id = FindViewById<EditText>(Resource.Id.edt_id);
            txt_nome = FindViewById<TextView>(Resource.Id.txv_nome);
            txt_email = FindViewById<TextView>(Resource.Id.txv_email);
            txt_cpfCnpj = FindViewById<TextView>(Resource.Id.txv_cpfCnpj);
            txt_tipoCliente = FindViewById<TextView>(Resource.Id.txv_tipo);

            btn_selecionarCliente = FindViewById<Button>(Resource.Id.btn_selecionar);
            btn_selecionarClienteJson = FindViewById<Button>(Resource.Id.btn_selecionarJson);

            btn_selecionarCliente.Click += btn_selecionarCliente_Click;
            btn_selecionarClienteJson.Click += btn_selecionarClienteJson_Click;
        }

        /// <summary>
        /// Handles the Click event of the btn_selecionarCliente control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_selecionarCliente_Click(object sender, EventArgs e)
        {
            Clear();

            // Seleciona o caminho do banco de dados
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "projeto.db3"); //Call Database  

            // Cria instância do banco de dados
            var db = new SQLiteConnection(dpPath);

            // Seleciona tabela cliente
            var data = db.Table<Cliente>();

            int idvalue = Convert.ToInt32(edt_id.Text.Trim());

            // Seleciona os dados
            var data1 = (from values in data
                         where values.id == idvalue
                         select new Cliente
                         {
                             Nome = values.Nome,
                             Email = values.Email,
                             CPFCNPJ = values.CPFCNPJ,
                             Tipo = values.Tipo
                             
                         }).ToList<Cliente>();

            if (data1.Count > 0)
            {
                foreach (var val in data1)
                {
                    txt_nome.Text = val.Nome;
                    txt_email.Text = val.Email;
                    txt_cpfCnpj.Text = val.CPFCNPJ;
                    txt_tipoCliente.Text = (val.Tipo.Equals("PJ") ? "Pessoa Jurídica" : "Pessoa física");
                }
            }
            else
            {
                Toast.MakeText(this, "Cliente não encontrado!", ToastLength.Short).Show();
            }
        }

        /// <summary>
        /// Handles the Click event of the btn_selecionarCategoriaJson control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btn_selecionarClienteJson_Click(object sender, EventArgs e)
        {
            Clear();

            using (var client = new HttpClient())
            {
                int idvalue = Convert.ToInt32(edt_id.Text.Trim());

                // Envia a requisição GET
                var uri = "http://localhost:8080/Categoria?Id=" + idvalue;

                var result = await client.GetStringAsync(uri);

                // Processa a resposta
                var clientes = JsonConvert.DeserializeObject<List<Cliente>>(result);

                // Gera a saida
                var cliente = clientes.First();
                txt_nome.Text = cliente.Nome;
                txt_email.Text = cliente.Email;
                txt_cpfCnpj.Text = cliente.CPFCNPJ;
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear()
        {
            txt_nome.Text = string.Empty;
            txt_email.Text = string.Empty;
            txt_cpfCnpj.Text = string.Empty;
            txt_tipoCliente.Text = string.Empty;
        }
    }
}
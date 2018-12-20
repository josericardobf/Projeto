using Android.App;
using Android.OS;
using Android.Widget;

namespace Projeto.Cliente
{
    [Activity(Label = "Cliente")]
    public class ClienteActivity : Activity
    {
        /// <summary>
        /// Botão inserir cliente
        /// </summary>
        Button btnInserirCliente;

        /// <summary>
        /// Botão selecionar cliente
        /// </summary>
        Button btnSelecionarCliente;

        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Cliente);
            btnInserirCliente = FindViewById<Button>(Resource.Id.btn_inserirCliente);
            btnSelecionarCliente = FindViewById<Button>(Resource.Id.btn_selecionarCliente);

            btnInserirCliente.Click += delegate { StartActivity(typeof(InserirClienteActivity)); };
            btnSelecionarCliente.Click += delegate { StartActivity(typeof(SelecionarClienteActivity)); };
        }
    }
}
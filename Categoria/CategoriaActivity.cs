
using Android.App;
using Android.OS;
using Android.Widget;

namespace Projeto.Categoria
{
    [Activity(Label = "Categoria")]
    public class CategoriaActivity : Activity
    {
        /// <summary>
        /// Botão inserir categoria
        /// </summary>
        Button btnInserirCategoria;

        /// <summary>
        /// Botão selecionar categoria
        /// </summary>
        Button btnSelecionarCategoria;

        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Categoria);
            btnInserirCategoria = FindViewById<Button>(Resource.Id.btn_inserirCategoria);
            btnSelecionarCategoria = FindViewById<Button>(Resource.Id.btn_selecionarCategoria);

            btnInserirCategoria.Click += delegate { StartActivity(typeof(InserirCategoriaActivity)); };
            btnSelecionarCategoria.Click += delegate { StartActivity(typeof(SelecionarCategoriaActivity)); };
        }
    }
}
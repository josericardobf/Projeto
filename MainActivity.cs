using System.IO;
using Android.App;
using Android.Widget;
using Android.OS;
using SQLite;

namespace Projeto
{
    [Activity(Label = "Projeto", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        /// <summary>
        /// Botão categoria
        /// </summary>
        Button btnCategoria;

        /// <summary>
        /// Botão cliente
        /// </summary>
        Button btnCliente;

        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            btnCategoria = FindViewById<Button>(Resource.Id.btn_categoria);
            btnCliente = FindViewById<Button>(Resource.Id.btn_cliente);

            CreateDB(); //Calling DB Creation method  

            btnCategoria.Click += delegate { StartActivity(typeof(Categoria.CategoriaActivity)); };
            btnCliente.Click += delegate { StartActivity(typeof(Cliente.ClienteActivity)); };
        }

        /// <summary>
        /// Creates the database.
        /// </summary>
        /// <returns>String with message</returns>
        public string CreateDB()
        {
            var output = "";
            output += "Creating Databse if it doesnt exists";
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "projeto.db3"); //Create New Database  
            var db = new SQLiteConnection(dpPath);
            output += "\n Database Created....";
            return output;
        }
    }
}


using SQLite;
using Newtonsoft.Json;

namespace Projeto.Categoria
{
    /// <summary>
    /// Classe de categoria
    /// </summary>
    class Categoria
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int id { get; set; } // AutoIncrement and set primarykey

        /// <summary>
        /// Gets or sets the nome.
        /// </summary>
        /// <value>
        /// The nome.
        /// </value>
        [MaxLength(25)]
        [JsonProperty("nome")]
        public string Nome { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        /// <remarks>
        /// To be added.
        /// </remarks>
        public override string ToString()
        {
            return string.Format("Post Nome: {0}", Nome);
        }
    }
}
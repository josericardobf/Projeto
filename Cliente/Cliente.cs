using SQLite;
using Newtonsoft.Json;

namespace Projeto.Cliente
{
    /// <summary>
    /// Classe de cliente
    /// </summary>
    class Cliente
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
        [MaxLength(50)]
        [JsonProperty("nome")]
        public string Nome { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The nome.
        /// </value>
        [MaxLength(25)]
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the document of client.
        /// </summary>
        /// <value>
        /// The nome.
        /// </value>
        [MaxLength(20)]
        [JsonProperty("cpfOuCnpj")]
        public string CPFCNPJ { get; set; }

        /// <summary>
        /// Gets or sets the type of client.
        /// </summary>
        /// <value>
        /// The nome.
        /// </value>
        [MaxLength(2)]
        [JsonProperty("tipo")]
        public string Tipo { get; set; }

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
            return string.Format("Post Nome: {0}\nEmail: {1}\nCPF/CNPJ: {2}", Nome, Email, CPFCNPJ);
        }
    }
}
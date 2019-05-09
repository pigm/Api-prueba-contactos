using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace prueba.Models
{
    /// <summary>
    /// Contacto.
    /// </summary>
    public class ContactoModels
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("apellido")]
        public string Apellido { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("telefono")]
        public string Telefono { get; set; }

        [BsonElement("empresa")]
        public string Empresa { get; set; }

        [BsonElement("cargo")]
        public string Cargo { get; set; }

        [BsonElement("fecha")]
        public string Fecha { get; set; }
    }
}
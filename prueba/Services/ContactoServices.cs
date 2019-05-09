using System.Collections.Generic;
using System.Linq;
using prueba.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace prueba.Services
{
    public class ContactoServices
    {
        private readonly IMongoCollection<ContactoModels> _contacto;

        public ContactoServices(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("ContactostoreDb"));
            var database = client.GetDatabase("ContactostoreDb");
            _contacto = database.GetCollection<ContactoModels>("contactos");
        }

        public List<ContactoModels> Get()
        {
            return _contacto.Find(cont => true).ToList();
        }

        public ContactoModels Get(string id)
        {
            return _contacto.Find<ContactoModels>(cont => cont.Id == id).FirstOrDefault();
        }

        public ContactoModels Create(ContactoModels cont)
        {
            _contacto.InsertOne(cont);
            return cont;
        }

        public void Update(string id, ContactoModels contIn)
        {
            _contacto.ReplaceOne(book => book.Id == id, contIn);
        }

        public void Remove(ContactoModels contIn)
        {
            _contacto.DeleteOne(cont => cont.Id == contIn.Id);
        }

        public void Remove(string id)
        {
            _contacto.DeleteOne(cont => cont.Id == id);
        }


    }
}
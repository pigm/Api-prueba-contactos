using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using prueba.Models;
using prueba.Services;

namespace prueba.Controllers
{
    /// <summary>
    /// Contacto controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {

        private readonly ContactoServices _contactoService;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:prueba.Controllers.ContactoController"/> class.
        /// </summary>
        /// <param name="contactoService">Contacto service.</param>
        public ContactoController(ContactoServices contactoService)
        {
            _contactoService = contactoService;
        }

        /// <summary>
        /// Get this instance.
        /// </summary>
        /// <returns>The get.</returns>
        [HttpGet]
        public ActionResult<List<ContactoModels>> Get()
        {
            return _contactoService.Get();
        }


        /// <summary>
        /// Get the specified id.
        /// </summary>
        /// <returns>The get.</returns>
        /// <param name="id">Identifier.</param>
        [HttpGet("{id:length(24)}", Name = "GetContacto")]
        public ActionResult<ContactoModels> Get(string id)
        {
            var cont = _contactoService.Get(id);
            if (cont == null)
            {
                return NotFound();
            }
            return cont;
        }

        /// <summary>
        /// Create the specified cont.
        /// </summary>
        /// <returns>The create.</returns>
        /// <param name="cont">Cont.</param>
        [HttpPost]
        public ActionResult<ServiceResult> Create(ContactoModels cont)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(cont.Nombre)
                      && !string.IsNullOrEmpty(cont.Apellido)
                      && !string.IsNullOrEmpty(cont.Email))
                    {
                        _contactoService.Create(cont);
                        result.Success = true;
                        result.StatusCode = 200;
                        result.Message = "OK";
                        result.Response = cont;
                        result.TokenResponse = null;
                    }
                    else
                    {
                        result.Success = false;
                        result.StatusCode = 800;
                        result.Message = "Business Exception";
                        result.Response = "Business Exception";
                        result.TokenResponse = null;
                    }
                }
                    

            }
            catch (MongoDB.Driver.MongoConnectionException ex)
            {
                result.Success = false;
                result.StatusCode = 406;
                result.Message = "Mongo Connection Exception";
                result.Response = ex.Message;
                result.TokenResponse = null;

            }
            catch (MongoDB.Driver.MongoExecutionTimeoutException ex)
            {
                result.Success = false;
                result.StatusCode = 408;
                result.Message = "Timeout Exception";
                result.Response = ex.Message;
                result.TokenResponse = null;

            }
            catch (MongoDB.Driver.MongoInternalException ex)
            {
                result.Success = false;
                result.StatusCode = 500;
                result.Message = "Internal mongo error";
                result.Response = ex.Message;
                result.TokenResponse = null;

            }
            catch (MongoDB.Driver.MongoException ex)
            {
                result.Success = false;
                result.StatusCode = -1;
                result.Message = ex.Message;
                result.Response = ex;
                result.TokenResponse = null;
     
            }

            return result;
        }

        /// <summary>
        /// Update the specified id and contIn.
        /// </summary>
        /// <returns>The update.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="contIn">Cont in.</param>
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, ContactoModels contIn)
        {
            var cont = _contactoService.Get(id);

            if (cont == null)
            {
                return NotFound();
            }
            _contactoService.Update(id, contIn);

            return NoContent();
        }

        /// <summary>
        /// Delete the specified id.
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="id">Identifier.</param>
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var cont = _contactoService.Get(id);

            if (cont == null)
            {
                return NotFound();
            }

            _contactoService.Remove(cont.Id);
            return NoContent();
        }
    }
}
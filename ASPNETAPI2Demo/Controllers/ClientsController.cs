using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ASPNETAPI2Demo.Models;

namespace ASPNETAPI2Demo.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("clients")]
    public class ClientsController : ApiController
    {
        private FabricsEntities1 db = new FabricsEntities1();
        public ClientsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: api/Clients
        [Route("")]
        public IQueryable<Client> GetClient()
        {
            return db.Client;
        }

        // GET: api/Clients/5
        [ResponseType(typeof(Client))]
        [Route("{id:int}",Name = "GetClientById")]
        public IHttpActionResult GetClient(int id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

       
        [Route("~/api/clients/type2/{id:int}")]
        public Client GetClientType2(int id)
        {
            Client client = db.Client.Find(id);
            return client;
        }

        
        [Route("~/api/clients/type3/{id:int}")]
        public IHttpActionResult GetClientType3(int id)
        {
            return Json(db.Client.Find(id));
        }

        [ResponseType(typeof(Order))]
        [Route("{id}/orders")]
        public IHttpActionResult GetClientOrders(int id)
        {
            var orders = db.Order.Where(p => p.ClientId == id);

            return Ok(orders);
        }

        [ResponseType(typeof(Order))]
        [Route("{id}/orders/{orderId}")]
        public IHttpActionResult GetClientOrder(int id, int orderId)
        {
            var order = db.Order.FirstOrDefault(p => p.ClientId == id && p.OrderId == orderId);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [Route("{id}/orders/pending")]
        public IHttpActionResult GetClientOrdersPending(int id)
        {
            var orders = db.Order.Where(p => p.ClientId == id && p.OrderStatus == "P");

            return Ok(orders);
        }

        [Route("clients/{id}/orders/{*date}")]
        public IHttpActionResult GetClientOrdersByDate(int id, DateTime date)
        {
            var orders = db.Order.Where(p => p.ClientId == id
                && p.OrderDate.Value.Year == date.Year
                && p.OrderDate.Value.Month == date.Month
                && p.OrderDate.Value.Day == date.Day);

            return Ok(orders);
        }


        // PUT: api/Clients/5
        [ResponseType(typeof(void))]
        [Route("{id}")]
        public IHttpActionResult PutClient(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.ClientId)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Clients
        [Route("{id}")]
        [ResponseType(typeof(Client))]
        public IHttpActionResult PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Client.Add(client);
            db.SaveChanges();

            return CreatedAtRoute("GetClientById", new { id = client.ClientId }, client);
        }

        // DELETE: api/Clients/5
        [ResponseType(typeof(Client))]
        [Route("{id}")]
        public IHttpActionResult DeleteClient(int id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            db.Client.Remove(client);
            db.SaveChanges();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Client.Count(e => e.ClientId == id) > 0;
        }
    }
}
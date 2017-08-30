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
using WebApiWithMVC.Data;

namespace WebApiWithMVC.Controllers
{

    public class CarEntitiesController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/CarEntities
        public IQueryable<CarEntities> GetCarEntities()
        {
            return db.CarEntities;
        }

        // GET: api/CarEntities/5
        [ResponseType(typeof(CarEntities))]
        public IHttpActionResult GetCarEntities(int id)
        {
            CarEntities carEntities = db.CarEntities.Find(id);
            if (carEntities == null)
            {
                return NotFound();
            }

            return Ok(carEntities);
        }

        // PUT: api/CarEntities/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCarEntities(int id, CarEntities carEntities)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != carEntities.Id)
            //{
            //    return BadRequest();
            //}

            db.Entry(carEntities).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!CarEntitiesExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CarEntities
        [ResponseType(typeof(CarEntities))]
        public IHttpActionResult PostCarEntities(CarEntities carEntities)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CarEntities.Add(carEntities);
            db.SaveChanges();

            return Ok(carEntities);
        }

        // DELETE: api/CarEntities/5
        [ResponseType(typeof(CarEntities))]
        public IHttpActionResult DeleteCarEntities(int id)
        {
            CarEntities carEntities = db.CarEntities.Find(id);
            if (carEntities == null)
            {
                return NotFound();
            }

            db.CarEntities.Remove(carEntities);
            db.SaveChanges();

            return Ok(carEntities);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //private bool CarEntitiesExists(int id)
        //{
        //    return db.CarEntities.Count(e => e.Id == id) > 0;
        //}
    }
}
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
using AutoMapper;
using Project_MVC.DAL;
using Project_MVC.Dtos;
using Project_MVC.Models;

namespace Project_MVC.Controllers.api
{
    public class TrainersController : ApiController
    {
        readonly ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            return Ok(db.Trainers.ToList().Select(Mapper.Map<Trainer, TrainerDto>));
        }

        private IHttpActionResult Ok(object p)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IHttpActionResult GetProduct(int id)
        {
            var product = db.Trainers.SingleOrDefault(p => p.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Trainer, TrainerDto>(product));
        }

        [HttpPost]
        public IHttpActionResult CreateProduct(TrainerDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var product = Mapper.Map<TrainerDto, Trainer>(productDto);
            db.Trainers.Add(product);
            db.SaveChanges();
            productDto.ID = product.ID;
            return Created(new Uri(Request.RequestUri + "/" + product.ID), productDto);
        }

        [HttpPut]
        public void UpdateProduct(int id, TrainerDto productDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var productInDb = db.Trainers.SingleOrDefault(p => p.ID == id);
            if (productInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(productDto, productInDb);

            db.SaveChanges();

        }

        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            var productInDb = db.Trainers.SingleOrDefault(p => p.ID == id);
            if (productInDb == null)
                return NotFound();
            db.Trainers.Remove(productInDb);
            db.SaveChanges();

            return Ok(Mapper.Map<Trainer, TrainerDto>(productInDb));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
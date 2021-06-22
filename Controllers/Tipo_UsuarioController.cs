using apiPractica.Context;
using apiPractica.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tipo_UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;
        public Tipo_UsuarioController(AppDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_context.tipo_usuario.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            try
            {
                var tipo_usuario = _context.tipo_usuario.FirstOrDefault(item => item.id == id);
                return Ok(tipo_usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] Tipo_Usuario tipo_usuario)
        {
            try
            {
                _context.tipo_usuario.Add(tipo_usuario);
                _context.SaveChanges();
                return CreatedAtRoute("GetById", new { tipo_usuario.id }, tipo_usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Tipo_Usuario tipo_usuario)
        {
            try
            {
                if (tipo_usuario.id == id)
                {
                    _context.Entry(tipo_usuario).State = EntityState.Modified;
                    _context.SaveChanges();
                    return CreatedAtRoute("GetById", new { id = tipo_usuario.id }, tipo_usuario);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var tipo_usuario = _context.tipo_usuario.FirstOrDefault(item => item.id == id);
                if (tipo_usuario != null)
                {
                    _context.tipo_usuario.Remove(tipo_usuario);
                    _context.SaveChanges();
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiPractica.Context;
using apiPractica.Models;
using Microsoft.EntityFrameworkCore;

namespace apiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tipo_UsuarioController : ControllerBase
    {
        private readonly AppDbContext context;
        public Tipo_UsuarioController(AppDbContext _context)
        {
            this.context = _context;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(context.tipo_usuario.ToList());
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
                var tipousuario = context.tipo_usuario.FirstOrDefault(item => item.idtipo_usuario == id);
                return Ok(tipousuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] Tipo_Usuario tipousuario)
        {
            try
            {
                context.tipo_usuario.Add(tipousuario);
                context.SaveChanges();
                return CreatedAtRoute("GetById", new { tipousuario.idtipo_usuario }, tipousuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Tipo_Usuario tipousuario)
        {
            try
            {
                if (tipousuario.idtipo_usuario == id)
                {
                    context.Entry(tipousuario).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetById", new { id = tipousuario.idtipo_usuario }, tipousuario);
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
                var tipousuario = context.tipo_usuario.FirstOrDefault(item => item.idtipo_usuario == id);
                if (tipousuario != null)
                {
                    context.tipo_usuario.Remove(tipousuario);
                    context.SaveChanges();
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

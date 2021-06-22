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
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsuarioController(AppDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_context.usuario.Include(e => e.tipo_usuario).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var usuario = _context.usuario.Include(e => e.tipo_usuario).FirstOrDefault(e => e.id == id);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] Usuario usuario)
        {
            try
            {
                //_context.usuario.Add(usuario);
                var tipo_usuarios = _context.tipo_usuario.FirstOrDefault(t => t.id == usuario.tipo_usuarioid);
                _context.Add(new Usuario { nombre = usuario.nombre,tipo_usuario= tipo_usuarios, tipo_usuarioid = usuario.tipo_usuarioid, tipo_documento = usuario.tipo_documento, num_documento = usuario.num_documento, direccion = usuario.direccion, email = usuario.email, password = usuario.password, estado = usuario.estado });
                _context.SaveChanges();
                return CreatedAtRoute("GetById", new { usuario.id }, usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Usuario usuario)
        {
            try
            {
                if (usuario.id == id)
                {
                    var tipo_usuarios = _context.tipo_usuario.FirstOrDefault(t => t.id == usuario.tipo_usuarioid);
                    var usuarios = _context.usuario.FirstOrDefault(e => e.id == usuario.id);
                    usuarios.nombre = usuario.nombre;
                    usuarios.tipo_documento = usuario.tipo_documento;
                    usuarios.num_documento = usuario.num_documento;
                    usuarios.direccion = usuario.direccion;
                    usuarios.email = usuario.email;
                    usuarios.password = usuario.password;
                    usuarios.estado = usuario.estado;
                    usuarios.tipo_usuario = tipo_usuarios;
//                    _context.Entry(usuario).State = EntityState.Modified;
                    _context.SaveChanges();
                    return CreatedAtRoute("GetById", new { id = usuario.id }, usuario);
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
                var usuario = _context.usuario.FirstOrDefault(item => item.id == id);
                if (usuario != null)
                {
                    _context.usuario.Remove(usuario);
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

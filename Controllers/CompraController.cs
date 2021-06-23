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
    public class CompraController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CompraController(AppDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_context.compra.Include(e => e.usuario).Include(e => e.proveedor).ToList());
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
                var venta = _context.compra.Include(e => e.usuario).Include(e => e.proveedor).FirstOrDefault(e => e.id == id);
                return Ok(venta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] Compra compra)
        {
            try
            {
                //_context.usuario.Add(usuario);
                var usuarios = _context.usuario.FirstOrDefault(t => t.id == compra.usuarioid);
                var proveedores = _context.persona.FirstOrDefault(t => t.id == compra.proveedorid);
                _context.Add(new Compra
                {
                    proveedor = proveedores,
                    usuario = usuarios,
                    tipo_comprobante = compra.tipo_comprobante,
                    serie_comprobante = compra.serie_comprobante,
                    num_comprobante = compra.num_comprobante,
                    fecha_hora = DateTime.Now,
                    impuesto = compra.impuesto,
                    total = compra.total,
                    estado = compra.estado
                });
                _context.SaveChanges();
                return CreatedAtRoute("GetById", new { compra.id }, compra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Compra compra)
        {
            try
            {
                if (compra.id == id)
                {
                    var proveedores = _context.persona.FirstOrDefault(t => t.id == compra.proveedorid);
                    var usuarios = _context.usuario.FirstOrDefault(e => e.id == compra.usuarioid);
                    var compras = _context.compra.FirstOrDefault(e => e.id == compra.id);

                    compras.proveedor = proveedores;
                    compras.usuario = usuarios;
                    compras.tipo_comprobante = compra.tipo_comprobante;
                    compras.serie_comprobante = compra.serie_comprobante;
                    compras.num_comprobante = compra.num_comprobante;
                    compras.fecha_hora = compra.fecha_hora;
                    compras.impuesto = compra.impuesto;
                    compras.total = compra.total;
                    compras.estado = compra.estado;
                    //                    _context.Entry(usuario).State = EntityState.Modified;
                    _context.SaveChanges();
                    return CreatedAtRoute("GetById", new { id = compra.id }, compra);
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
                var compra = _context.compra.FirstOrDefault(item => item.id == id);
                if (compra != null)
                {
                    _context.compra.Remove(compra);
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

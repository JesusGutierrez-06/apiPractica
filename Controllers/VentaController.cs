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
    public class VentaController : ControllerBase
    {
        private readonly AppDbContext _context;
        public VentaController(AppDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_context.venta.Include(e => e.usuario).Include(e => e.cliente).ToList());
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
                var venta = _context.venta.Include(e => e.usuario).Include(e => e.cliente).FirstOrDefault(e => e.id == id);
                return Ok(venta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] Venta venta)
        {
            try
            {
                //_context.usuario.Add(usuario);
                var usuarios = _context.usuario.FirstOrDefault(t => t.id == venta.usuarioid);
                var clientes = _context.persona.FirstOrDefault(t => t.id == venta.clienteid);
                _context.Add(new Venta {
                    cliente = clientes,
                    usuario = usuarios,
                    tipo_comprobante = venta.tipo_comprobante,
                    serie_comprobante = venta.serie_comprobante,
                    num_comprobante = venta.num_comprobante,
                    fecha_hora = DateTime.Now,
                    impuesto = venta.impuesto,
                    total = venta.total,
                    estado = venta.estado }) ;
                _context.SaveChanges();
                return CreatedAtRoute("GetById", new { venta.id }, venta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Venta venta)
        {
            try
            {
                if (venta.id == id)
                {
                    var clientes = _context.persona.FirstOrDefault(t => t.id == venta.clienteid);
                    var usuarios = _context.usuario.FirstOrDefault(e => e.id == venta.usuarioid);
                    var ventas = _context.venta.FirstOrDefault(e => e.id == venta.id);

                    ventas.cliente = clientes;
                    ventas.usuario = usuarios;
                    ventas.tipo_comprobante = venta.tipo_comprobante;
                    ventas.serie_comprobante =venta.serie_comprobante;
                    ventas.num_comprobante = venta.num_comprobante;
                    ventas.fecha_hora = venta.fecha_hora;
                    ventas.impuesto = venta.impuesto;
                    ventas.total = venta.total;
                    ventas.estado = venta.estado;
                    //                    _context.Entry(usuario).State = EntityState.Modified;
                    _context.SaveChanges();
                    return CreatedAtRoute("GetById", new { id = venta.id }, venta);
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
                var venta = _context.venta.FirstOrDefault(item => item.id == id);
                if (venta != null)
                {
                    _context.venta.Remove(venta);
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

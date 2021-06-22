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
    public class Detalle_VentaController : ControllerBase
    {
        private readonly AppDbContext _context;
        public Detalle_VentaController(AppDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_context.detalle_venta.Include(e => e.venta).Include(e => e.articulo).ToList());
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
                var detalle_venta = _context.detalle_venta.Include(e => e.venta).Include(e => e.articulo).FirstOrDefault(e => e.id == id);
                return Ok(detalle_venta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] Detalle_Venta detalle_venta)
        {
            try
            {
                //_context.usuario.Add(usuario);
                var ventas = _context.venta.FirstOrDefault(t => t.id == detalle_venta.ventaid);
                var articulos = _context.articulo.FirstOrDefault(t => t.id == detalle_venta.articuloid);
                _context.Add(new Detalle_Venta
                {
                    venta = ventas,
                    articulo = articulos,
                    cantidad = detalle_venta.cantidad,
                    precio = detalle_venta.precio,
                    descuento = detalle_venta.descuento
                });
                _context.SaveChanges();
                return CreatedAtRoute("GetById", new { detalle_venta.id }, detalle_venta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Detalle_Venta detalle_venta)
        {
            try
            {
                if (detalle_venta.id == id)
                {
                    var ventas = _context.venta.FirstOrDefault(t => t.id == detalle_venta.ventaid);
                    var articulos = _context.articulo.FirstOrDefault(e => e.id == detalle_venta.articuloid);
                    var detalle_ventas = _context.detalle_venta.FirstOrDefault(e => e.id == detalle_venta.id);

                    detalle_ventas.venta = ventas;
                    detalle_ventas.articulo= articulos;
                    detalle_ventas.cantidad = detalle_venta.cantidad;
                    detalle_ventas.precio = detalle_venta.precio;
                    detalle_ventas.descuento = detalle_venta.descuento;
                    //                    _context.Entry(usuario).State = EntityState.Modified;
                    _context.SaveChanges();
                    return CreatedAtRoute("GetById", new { id = detalle_venta.id }, detalle_venta);
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
                var detalle_venta = _context.venta.FirstOrDefault(item => item.id == id);
                if (detalle_venta != null)
                {
                    _context.venta.Remove(detalle_venta);
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

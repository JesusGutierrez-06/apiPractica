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
    public class Detalle_CompraController : ControllerBase
    {
        private readonly AppDbContext _context;
        public Detalle_CompraController(AppDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_context.detalle_compra.Include(e => e.compra).Include(e => e.articulo).ToList());
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
                var detalle_compra = _context.detalle_compra.Include(e => e.compra).Include(e => e.articulo).FirstOrDefault(e => e.id == id);
                return Ok(detalle_compra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] Detalle_Compra detalle_compra)
        {
            try
            {
                //_context.usuario.Add(usuario);
                var compras = _context.compra.FirstOrDefault(t => t.id == detalle_compra.compraid);
                var articulos = _context.articulo.FirstOrDefault(t => t.id == detalle_compra.articuloid);
                _context.Add(new Detalle_Compra
                {
                    compra = compras,
                    articulo = articulos,
                    cantidad = detalle_compra.cantidad,
                    precio = detalle_compra.precio,
                });
                _context.SaveChanges();
                return CreatedAtRoute("GetById", new { detalle_compra.id }, detalle_compra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Detalle_Compra detalle_compra)
        {
            try
            {
                if (detalle_compra.id == id)
                {
                    var compras = _context.compra.FirstOrDefault(t => t.id == detalle_compra.compraid);
                    var articulos = _context.articulo.FirstOrDefault(e => e.id == detalle_compra.articuloid);
                    var detalle_compras = _context.detalle_compra.FirstOrDefault(e => e.id == detalle_compra.id);

                    detalle_compras.compra = compras;
                    detalle_compras.articulo = articulos;
                    detalle_compras.cantidad = detalle_compra.cantidad;
                    detalle_compras.precio = detalle_compra.precio;
                    //                    _context.Entry(usuario).State = EntityState.Modified;
                    _context.SaveChanges();
                    return CreatedAtRoute("GetById", new { id = detalle_compra.id }, detalle_compra);
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
                var detalle_compra = _context.detalle_compra.FirstOrDefault(item => item.id == id);
                if (detalle_compra != null)
                {
                    _context.detalle_compra.Remove(detalle_compra);
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

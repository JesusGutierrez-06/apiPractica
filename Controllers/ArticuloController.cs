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
    public class ArticuloController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ArticuloController(AppDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(_context.articulo.Include(e => e.categoria).ToList());
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
                var articulo = _context.articulo.Include(e => e.categoria).FirstOrDefault(e => e.id == id);
                return Ok(articulo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody] Articulo articulo)
        {
            try
            {
                //_context.articulo.Add(articulo);
                var categorias = _context.categoria.FirstOrDefault(t => t.id == articulo.categoriaid);
                _context.Add(new Articulo { nombre = articulo.nombre, categoria = categorias, categoriaid = articulo.categoriaid, codigo = articulo.codigo, precio_venta = articulo.precio_venta, stock = articulo.stock, descripcion = articulo.descripcion, estado = articulo.estado });
                _context.SaveChanges();
                return CreatedAtRoute("GetById", new { articulo.id }, articulo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Articulo articulo)
        {
            try
            {
                if (articulo.id == id)
                {
                    var categorias = _context.categoria.FirstOrDefault(t => t.id == articulo.categoriaid);
                    var articulos = _context.articulo.FirstOrDefault(e => e.id == articulo.id);
                    articulos.nombre = articulo.nombre;
                    articulos.codigo = articulo.codigo;
                    articulos.precio_venta = articulo.precio_venta;
                    articulos.stock = articulo.stock;
                    articulos.descripcion = articulo.descripcion;
                    articulos.estado = articulo.estado;
                    articulos.categoria = categorias;
                    //                    _context.Entry(articulo).State = EntityState.Modified;
                    _context.SaveChanges();
                    return CreatedAtRoute("GetById", new { id = articulo.id }, articulo);
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
                var articulo = _context.articulo.FirstOrDefault(item => item.id == id);
                if (articulo != null)
                {
                    _context.articulo.Remove(articulo);
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

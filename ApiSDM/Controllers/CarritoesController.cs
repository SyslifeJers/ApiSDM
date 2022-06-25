using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSDM.Models;
using ApiSDM.Models.ViewsModel;

namespace ApiSDM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoesController : ControllerBase
    {
        private readonly u204501959_SaborDeMexicoContext _context;

        public CarritoesController(u204501959_SaborDeMexicoContext context)
        {
            _context = context;
        }

        // GET: api/Carritoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrito>>> GetCarrito()
        {
            return await _context.Carrito.ToListAsync();
        }
        public const double EarthRadius = 6371;
        public static double GetDistance(GeoCoordinate point1, GeoCoordinate point2)
        {
            double distance = 0;
            double Lat = (point2.Latitude - point1.Latitude) * (Math.PI / 180);
            double Lon = (point2.Longitude - point1.Longitude) * (Math.PI / 180);
            double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(point1.Latitude * (Math.PI / 180)) * Math.Cos(point2.Latitude * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            distance = EarthRadius * c;
            return distance;
        }
        [HttpPost("CancelarOrden")]
        public async Task<ActionResult<ModelGOrden>> CancelarOCarrito(ModelGOrden model)
        {
            model.Respuesta = false ;
            var clien = await _context.Cliente.Where(f => f.Token.Equals(model.Token)).Include(d => d.Orden).ToListAsync();
            if (clien.Count == 0)
            {
                return Ok(model);
            }
            bool existe = false;
            foreach (var orden in clien[0].Orden)
            {
                if (orden.Id==model.idOrden)
                {
                    existe = true;
                }
            }
            existe = true;
            if (existe)
            {
                var ordens = await _context.Orden.FindAsync(model.idOrden);
                ordens.Estatus = -1;
                _context.Entry(ordens).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                model.Respuesta = true;
            }
            


            return Ok(model);
        }
            [HttpPost("Orden")]
        public async Task<ActionResult<ModelGOrden>> CrearOCarrito(ModelGOrden model)
        {
            var clien = await _context.Cliente.Where(f => f.Token.Equals(model.Token)).Include(d => d.Ubicacion).Include(d => d.Carrito).ToListAsync();
            var listprod = await _context.Producto.Where(f => f.Activo > 0).ToListAsync();
            var listprese = await _context.Presentacion.ToListAsync();

            if (clien[0].Carrito.Count == 0)
            {
                return Ok(model);
            }

            List<ModelCarrito> lisCarrito = new List<ModelCarrito>();
            List<DetalleOrden> detalleOrden = new List<DetalleOrden>();
            int id_resta = 0;
            int indexultimo = clien[0].Ubicacion.Count - 1;
            var ubica = clien[0].Ubicacion.ToList();

            Ruta ruta = new Ruta()
            {
                Lat = ubica[indexultimo].Lat,
                Lon = Convert.ToDecimal(ubica[indexultimo].Lon),
        
                Direccion = ubica[indexultimo].Direccion,
                Google = ubica[indexultimo].Nota,
                Cp = ubica[indexultimo].Cp

            };
            decimal total = 0;
            int cant = 0;
            IEnumerable<Repartidor> repart = await _context.Repartidor.Where(f => f.Activo == 1).ToArrayAsync();
            List<DistaciaModel> distancia = new List<DistaciaModel>();
            foreach (var distribucion in repart)
            {
                try
                {
                    double rango = GetDistance(new GeoCoordinate() { Latitude = Convert.ToDouble(distribucion.Lat), Longitude = Convert.ToDouble(distribucion.Lon) }, new GeoCoordinate() { Longitude = Convert.ToDouble(ubica[indexultimo].Lon), Latitude = Convert.ToDouble(ubica[indexultimo].Lat) });
                    distancia.Add(new DistaciaModel() { repartidor = distribucion, distancia = (rango) });
                }
                catch (Exception)
                {

                }
            }
            if (distancia.Count == 0)
            {
                model.Nota = "Nuestros servicios ya no estan disponibles en esta ubicación";
                return Ok(model);
            }
            else
            {
                List<DistaciaModel> nuevodistancia = new List<DistaciaModel>(distancia);
                foreach (var item in distancia)
                {
                    if (item.distancia <= Convert.ToDouble(item.repartidor.Rango+1))
                    {
                        item.rango = true;
                    }
                    else

                        nuevodistancia.Remove(item);
                }
                distancia = new List<DistaciaModel>(nuevodistancia.OrderBy(d=>d.distancia));
            }
            if (distancia.Count == 0)
            {
                model.Nota = "Nuestros servicios no estan en el rango de distribución";
                return Ok(model);
            }


            foreach (var item in clien[0].Carrito.ToList())
            {
                var produc = listprod.Where(d => d.Id == item.ProductoId).FirstOrDefault();
                cant += (int)item.Cantidad;
                var prese = listprese.Where(d=>d.Id == item.IdPresentacion).FirstOrDefault();
                decimal precio = 
                total += (decimal)(item.Cantidad * prese.Precio);
                DetalleOrden orden = new DetalleOrden()
                {
                    Cantidad = item.Cantidad,
                    Subtotal = item.Cantidad * prese.Precio,
                    ProductoId = item.ProductoId,
                    IdPresentacion = item.IdPresentacion,
                    Nota = item.Nota,

                };

                detalleOrden.Add(orden);

            }

            try
            {

                _context.Ruta.Add(ruta);
                int resuk = await _context.SaveChangesAsync();
                Orden ordenPlatillo = new Orden()
                {
                    Activo = 1,
                    Cantidad = cant,
                    Total = total,
                    Fecha = DateTime.Now,
                    ClienteId = clien[0].Id,
                    RutaId = ruta.Id,
                    CostoEnvio = model.CostoEnvio,
                    TipoPago = 1,
                    Estatus = 0,
                    RepartidorId = distancia[0].repartidor.Id


                };

                _context.Orden.Add(ordenPlatillo);
                await _context.SaveChangesAsync();

                foreach (var item in detalleOrden)
                {
                    item.OrdenId = ordenPlatillo.Id;

                }

                _context.DetalleOrden.AddRange(detalleOrden);
                await _context.SaveChangesAsync();

                model.Respuesta = true;
                model.idOrden = ordenPlatillo.Id;

                try
                {
                    _context.Carrito.RemoveRange(clien[0].Carrito);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    model.Respuesta = false;
                    return Ok(model);
                }

            }
            catch (Exception)
            {

            }

            return Ok(model);

        }


        [HttpPost("BorrarLista")]
        public async Task<ActionResult<ModelCarrito>> DeleteCarrito(ModelCarrito carrito)
        {
            var clien = await _context.Cliente.Where(f => f.Token.Equals(carrito.Token)).Include(d => d.Carrito).ToListAsync();

            _context.Carrito.RemoveRange(clien[0].Carrito);
            await _context.SaveChangesAsync();
            carrito.Nota = "C";
            return Ok(carrito);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> DevOrden(int id)
        {
            var orde = await _context.Orden.Where(d => d.Id == id).Include(d => d.Repartidor).Include(d => d.DetalleOrden).ToListAsync();

            var proc = await _context.Producto.ToListAsync();
            List<ModelDetalleOrden> model = new List<ModelDetalleOrden>();
            foreach (var item in orde[0].DetalleOrden)
            {
                Producto platillos = new Producto();
                var listpla = proc.Where(d => d.Id == item.ProductoId).ToList();
                platillos = listpla[0];
                decimal precio = (decimal)item.Subtotal / (int)item.Cantidad;
                var devorden = new ModelDetalleOrden()
                {
                    Cant = orde[0].Cantidad.ToString(),
                    Estatus = (int)orde[0].Estatus,
                    Fecha = orde[0].Fecha.ToString(),
                    Total = orde[0].Total.ToString(),
                    Plato = item.Cantidad + " " + platillos.Nombre + " " + precio,
                    Envio = orde[0].CostoEnvio.ToString(),  
                    
                };
                if (orde[0].Repartidor != null)
                {
                    devorden.Repartidor = orde[0].Repartidor.Nombre;

                }
                model.Add(devorden);
            }


            return Ok(model);
        }

        // GET: api/Carritoes/5
        [HttpPost("BorrarProduc")]
        public async Task<ActionResult<ModelCarrito>> GetCarrito(ModelCarrito model)
        {
            var carrito = await _context.Carrito.FindAsync(model.Id);

            if (carrito == null)
            {
                return NotFound();
            }

            _context.Carrito.Remove(carrito);
            await _context.SaveChangesAsync();

            model.Nota = "C";
            return model;
        }

        // PUT: api/Carritoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrito(int id, Carrito carrito)
        {
            if (id != carrito.Id)
            {
                return BadRequest();
            }

            _context.Entry(carrito).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarritoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Carritoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Carrito>> PostCarrito(ModelCarrito carrito)
        {
            var clien = await _context.Cliente.Include(d => d.Carrito).Where(f => f.Token.Equals(carrito.Token)).ToListAsync();

            if (clien.Count > 0)
            {

                    Carrito model = new Carrito()
                    {

                        Modificado = DateTime.Now,
                        Cantidad = carrito.Cantidad,
                        IdCliente = clien[0].Id,
                        ProductoId = carrito.IdProducto,
                        IdPresentacion = (int)carrito.IdPresentacion,
                        Nota = "",

                    };
                    _context.Carrito.Add(model);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetCarrito", new { id = model.Id }, model);



            }
            return null;

        }
        [HttpPost("Lista")]
        public async Task<IActionResult> ListaCarrito(ModelCarrito carrito)
        {
            Cliente clien = await _context.Cliente.Where(f => f.Token.Equals(carrito.Token)).FirstOrDefaultAsync();
            if (clien == null) { return null; }
            List<Carrito> carritos = await _context.Carrito.Where(d => d.IdCliente == clien.Id).ToListAsync();
            List<Producto> listprod = await _context.Producto.Include(d => d.Presentacion).Where(f => f.Activo == 1).ToListAsync();
            List<ModelCarrito> lisCarrito = new List<ModelCarrito>();

            foreach (Carrito item in carritos)
            {

                Producto produc = listprod.Where(d => d.Id == item.ProductoId).FirstOrDefault();
                Presentacion presentacion = produc.Presentacion.Where(x=>x.Id==item.IdPresentacion).FirstOrDefault();
                ModelCarrito model = new ModelCarrito()
                {

                };
                model.Cantidad = item.Cantidad;
                model.Id = item.Id;
                model.IdProducto = item.ProductoId;
                model.NameProduct = produc.Nombre;
                model.NamePresentacion = presentacion.Precentacion;
                     model.Costo = presentacion.Precio;
                    lisCarrito.Add(model);



            }

            return Ok(lisCarrito);

        }

        // DELETE: api/Carritoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Carrito>> DeleteCarrito(int id)
        {
            var carrito = await _context.Carrito.FindAsync(id);
            if (carrito == null)
            {
                return NotFound();
            }

            _context.Carrito.Remove(carrito);
            await _context.SaveChangesAsync();

            return carrito;
        }

        private bool CarritoExists(int id)
        {
            return _context.Carrito.Any(e => e.Id == id);
        }
    }
}

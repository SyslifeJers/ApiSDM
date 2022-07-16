
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSDM.Models;
using System.Text;
using System.Security.Cryptography;
using ApiSDM.Models.ViewsModel;
using System;
using static ApiSDM.Controllers.Herramientas;

namespace ApiSDM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly u204501959_SaborDeMexicoContext _context;

        public ClientesController(u204501959_SaborDeMexicoContext context)
        {
            _context = context;
        }

        [HttpPost("Reenviar")]
        public async Task<ActionResult<Cliente>> PostCodigoRenviar(Cliente cliente)
        {
            var clien = await _context.Cliente.FirstOrDefaultAsync(d=>d.Correo.Equals(cliente.Correo));
            try
            {
                Herramientas.Correo(clien.Correo, "Recuperación de contrase♫a", "Tu codigo de validacion es: " + clien.CodigoR);
                clien.Id = 1;
                return Ok(cliente);
            }
            catch (Exception)
            {
                cliente.Id = 0;
                cliente.Token = "Ups! nuestros servidores estan ocupados, intente más tarde";
                return Ok(cliente);

            }
        }
            [HttpPost("GCodigo")]
        public async Task<ActionResult<Cliente>> PostCodigoCliente(Cliente cliente)
        {
            var clien = await _context.Cliente.Where(f => f.Correo.Equals(cliente.Correo)).ToListAsync();

            if (clien.Count != 0)
            {
               
                var seed = Environment.TickCount;
                var random = new Random(seed);
                string cad = "";
                for (int i = 0; i < 3; i++)
                {
                    var value = random.Next(0, 9);
                    Console.WriteLine($"Iteración {i} - semilla {seed} - valor {value}");
                    cad += cad + value;
                }
                clien[0].CodigoR = cad;
                _context.Cliente.Update(clien[0]);
                await _context.SaveChangesAsync();
                cliente.Id = 1;

                try
                {
                    Herramientas.Correo(clien[0].Correo, "Recuperación de contrase♫a", "Tu codigo de validacion es: " + cad);
                }
                catch (Exception)
                {
                    cliente.Id = 0;
                    cliente.Token = "Ups! nuestros servidores estan ocupados, intente más tarde";
                    return Ok(cliente);

                }
                return Ok(cliente);
            }
            else
            {
                cliente.Id = 0;
                cliente.Token = "Ups! error este correo no fue registrado";
                return Ok(cliente);
            }

        }
        [HttpPost("CVerifi")]
        public async Task<ActionResult<Cliente>> PostVerifiCCliente(Cliente cliente)
        {
            var clien = await _context.Cliente.Where(f => f.Correo.Equals(cliente.Correo)).ToListAsync();

            if (clien.Count != 0)
            {
                if (clien[0].CodigoR.Equals(cliente.CodigoR))
                {
                    cliente.Id = 1;
                }
                else
                {
                    cliente.Id = 0;
                }


                return Ok(cliente);
            }
            else
            {
                cliente.Id = 0;
                cliente.Token = "Ups error este Correo no fue registrado";
                return Ok(cliente);
            }

        }
        [HttpGet("prueba")]
        public async Task<ActionResult> Prueba()
        {
            Notification.NotificationP("cTGJz-PdQhmlciro8ouqn8:APA91bEIZYldrAcR_J09dL9xqRBeYATGjbMP1Zy7DErKp0Tcwas0QURsCK68kCo1l4ig6Jn_-FuhXfOYKTdnnH9ivSseGCq5LlFXr3oHv7vtJ8s-lhobm4YuqMDaEZolGo3WNrf-YkBI");
         //   Notification.SendNotification("cTGJz-PdQhmlciro8ouqn8:APA91bEIZYldrAcR_J09dL9xqRBeYATGjbMP1Zy7DErKp0Tcwas0QURsCK68kCo1l4ig6Jn_-FuhXfOYKTdnnH9ivSseGCq5LlFXr3oHv7vtJ8s-lhobm4YuqMDaEZolGo3WNrf-YkBI", "Prueba de alerta", "asdsaf");
            return Ok(true);
        }
        [HttpPost("SaveNotifi")]
        public async Task<ActionResult<Cliente>> PostVerifiCNotificacion(Cliente cliente)
        {
            var clien = await _context.Cliente.Where(f => f.Token.Equals(cliente.Token)).ToListAsync();

            if (clien.Count != 0)
            {
                clien[0].CodigoN = cliente.CodigoN;
                _context.Cliente.Update(clien[0]);
                await _context.SaveChangesAsync();

                return Ok(cliente);
            }
            else
            {
                cliente.Id = 0;
                cliente.Token = "Ups error este Correo no fue registrado";
                return Ok(cliente);
            }

        }
        [HttpPost("CContra")]
        public async Task<ActionResult<Cliente>> PostContraCliente(Cliente cliente)
        {
            var clien = await _context.Cliente.Where(f => f.Correo.Equals(cliente.Correo)).ToListAsync();

            if (clien.Count != 0)
            {
                if (clien[0].CodigoR.Equals(cliente.CodigoR))
                {
                    clien[0].Contrasena = Encriptacion.EncodePassword(cliente.Contrasena);
                    _context.Cliente.Update(clien[0]);
                    await _context.SaveChangesAsync();
                    cliente.Id = 1;
                    cliente.Token = "Cambio correcto";
                    return Ok(cliente);
                }
                else
                {
                    cliente.Id = 0;
                    cliente.Token = "Problemas con tu codigo de confirmación";
                    return Ok(cliente);
                }
            }
            else
            {
                cliente.Id = 0;
                cliente.Token = "Ups error este correo no fue registrado";
                return Ok(cliente);
            }

        }

        [HttpPost("Datos")]
        public async Task<ActionResult<Cliente>> PostDatos(Cliente model)
        {
            try
            {
                var per = await _context.Cliente.Where(d => d.Token == model.Token).ToListAsync();
                if (per.Count == 0)
                {

                    return Ok(model);
                }
                else
                {
                    model = per[0];
                    return Ok(model);
                }
            }
            catch (Exception)
            {


                return Ok(null);
            }
        }
        [HttpPost("VerificaC")]
        public async Task<ActionResult<bool>> Postvalidar(Cliente model)
        {
            try
            {
                var per = await _context.Cliente.Where(d => d.Token == model.Token).ToListAsync();
                if (per.Count == 0)
                {

                    return Ok(false);
                }
                else
                {
                    return Ok(true);
                }
            }
            catch (Exception)
            {


                return Ok(null);
            }
        }
        [HttpPost("Registro")]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            var clien = await _context.Cliente.Where(f => f.Telefono.Equals(cliente.Telefono) || f.Correo.Equals(cliente.Correo)).ToListAsync();

            if (clien.Count == 0)
            {
                if (true)
                {
                    cliente.Contrasena = Encriptacion.EncodePassword(cliente.Contrasena);
                    cliente.CodigoN = "na";
                    cliente.Activo = 1;
                    cliente.Modificado = DateTime.Now;
                    _context.Cliente.Add(cliente);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
                }
            }
            else
            {
                cliente.Activo = 0;
                cliente.Token = "Ups error este Numero o Correo ya fue registrado";
                return Ok(cliente);
            }

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
        [HttpPost("Ubicacion")]
        public async Task<ActionResult<ModelUbicacion>> PostUbicacionA(ModelUbicacion model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Cp))
                {
                    model.Token = "Por favor ingrese su CP";
                    return Ok(model);
                }

                Cliente client = await _context.Cliente.Where(f => f.Token == model.Token).FirstOrDefaultAsync();

                if (client != null)
                {
                    IEnumerable<Repartidor> repart = await _context.Repartidor.Where(f => f.Activo == 1).ToArrayAsync();
                    List<DistaciaModel> distancia = new List<DistaciaModel>();
                    foreach (var distribucion in repart)
                    {
                        try
                        {
                            double rango = GetDistance(new GeoCoordinate() { Latitude = Convert.ToDouble(distribucion.Lat), Longitude = Convert.ToDouble(distribucion.Lon) }, new GeoCoordinate() { Longitude = Convert.ToDouble(model.Lon), Latitude = Convert.ToDouble(model.Lat) });
                            distancia.Add(new DistaciaModel() { repartidor = distribucion,distancia = (rango) });
                        }
                        catch (Exception)
                        {

                        }
                    }
                    if (distancia.Count==0)
                    {
                        model.Token = "Nuestros servicios reanudaran pronto";
                        return Ok(model);
                    }
                    else
                    {
                        List<DistaciaModel> nuevodistancia = new List<DistaciaModel>(distancia);
                        foreach (var item in distancia)
                        {
                            if (item.distancia <= Convert.ToDouble(item.repartidor.Rango + 1))
                            {
                                item.rango = true;
                            }
                            else

                                nuevodistancia.Remove(item);
                        }
                        distancia = new List<DistaciaModel>(nuevodistancia);
                    }
                    if (distancia.Count == 0)
                    {
                        model.Token = "Nuestros servicios no estan en el rango de distribución";
                        return Ok(model);
                    }

                    Ubicacion ubicaicion = new Ubicacion()
                    {
                        Direccion = model.Direccion,
                        ClienteId = client.Id,
                        Lat = model.Lat,
                        Lon = model.Lon,
                        Nota = model.Name,
                        Cp = model.Cp
                    };
                    _context.Ubicacion.Add(ubicaicion);
                    await _context.SaveChangesAsync();



                }
                model.Token = "Ok";
                return Ok(model);
            }
            catch (Exception)
            {
                model.Token = "Error de nube";
                return Ok(model);
            }
        }
        [HttpPost("ListaUbicacion")]
        public async Task<ActionResult<List<ModelUbicacion>>> PostListUbicacionA(ModelUbicacion model)
        {
            try
            {
                return Ok(await _context.Ubicacion.Where(d => d.Cliente.Token == model.Token).ToListAsync());
            }
            catch (Exception)
            {
                return Ok(null);
            }
        }
        [HttpPost("ListaOrdenes")]
        public async Task<ActionResult<List<Orden>>> PostListOrdenes(ModelUbicacion model)
        {
            try
            {
                return Ok(await _context.Orden.Where(d => d.Cliente.Token == model.Token).ToListAsync());
            }
            catch (Exception)
            {
                return Ok(null);
            }
        }
        [HttpPost("Login")]
        public async Task<ActionResult<Cliente>> PostLogin(Cliente model)
        {
            try
            {
                List<Cliente> listuser = new List<Cliente>();
                listuser = await _context.Cliente.ToListAsync();
                Cliente user = null;
                foreach (Cliente usuario in listuser)
                {
                    if (usuario.Correo == model.Correo)
                    {
                        user = usuario;
                    }
                }
                if (user == null)
                {
                    model.Activo = 0;
                    model.Nombre = "Numero no registrado";
                    return Ok(model);
                }
                if (user.Contrasena != Encriptacion.EncodePassword(model.Contrasena))
                {
                    model.Activo = 0;
                    model.Nombre = "Contraseña incorrecta";

                    return Ok(model);
                }
                else
                {
                    byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                    byte[] key = Guid.NewGuid().ToByteArray();
                    string token = Convert.ToBase64String(time.Concat(key).ToArray());
                    user.Token = token;
                    model.Activo = 1;
                    model.Token = user.Token;
                    _context.Entry(user).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(user);
                }
            }
            catch (Exception)
            {

                model.Activo = 0;
                model.Nombre = "Ups Problemas de nube";

                return Ok(model);
            }
        }

        // GET: api/Clientes
        [HttpGet("Cate")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            return await _context.Categoria.ToListAsync();
        } 
        [HttpGet("Produc/{id}")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos(int id)
        {
            return await _context.Producto.Where(d=>d.Categoria == id).ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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



        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cliente>> DeleteCliente(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return cliente;
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }

        private class Encriptacion
        {
            public static string EncodePassword(string originalPassword)
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();

                byte[] inputBytes = (new UnicodeEncoding()).GetBytes(originalPassword);
                byte[] hash = sha1.ComputeHash(inputBytes);

                return Convert.ToBase64String(hash);
            }
        }
    }
}

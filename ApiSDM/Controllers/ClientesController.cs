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
        [HttpPost("Ubicacion")]
        public async Task<ActionResult<ModelUbicacion>> PostUbicacionA(ModelUbicacion model)
        {
            try
            {
                Cliente client = await _context.Cliente.Where(f => f.Token == model.Token).FirstOrDefaultAsync();
                if (client != null)
                {

                    Ubicacion ubicaicion = new Ubicacion()
                    {
                        Direccion = model.Direccion,
                        ClienteId = client.Id,
                        Lat = model.Lat,
                        Lon = model.Lon,
                        Nota = model.Name,
                    };
                    _context.Ubicacion.Add(ubicaicion);
                    await _context.SaveChangesAsync();



                }
                return Ok(model);
            }
            catch (Exception)
            {
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AerodromController : ControllerBase
    {
        public AerodromContext Context { get; set; }

        public AerodromController(AerodromContext context)
        {
            Context=context;
        }

        [Route("UpisiAerodromUTabelu")]
        [HttpPost]
        public async Task<ActionResult> UpisiAerodromUTabelu([FromBody] Aerodrom aerodrom)
        {

            if(string.IsNullOrWhiteSpace(aerodrom.NazivAerodroma) || aerodrom.NazivAerodroma.Length>20)
           {
               return BadRequest("Naziv aerodroma nije korektan!");
           }

            if(string.IsNullOrWhiteSpace(aerodrom.Lokacija) || aerodrom.Lokacija.Length>20)
           {
               return BadRequest("Lokacija aerodroma nije korektna!");
           }

            try{
               Context.Aerodromi.Add(aerodrom);
               await Context.SaveChangesAsync();
               return Ok("Aerodrom je dodat u bazu!");
           }
            catch(Exception exception)
           {
               return BadRequest(exception.Message);
           }
       }

       [Route("VratiAerodrome")]
       [HttpGet]
       public async Task<ActionResult> VratiAerodrome()
        {
           try
           {     
               return Ok(await Context.Aerodromi.Select(p=>
               new
               {
                   ID=p.ID,
                   NazivAerodroma=p.NazivAerodroma,
                   Lokacija=p.Lokacija

               }).ToListAsync());
           }
           catch(Exception exception)
           {
               return BadRequest(exception.Message);
           }

        }


       /* [Route("ObrisiAerodrom")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiAerodrom(int id)
        {
            if(id<=0)
            {
               return BadRequest("Pogresan ID!");
            }
            try
            {
               var aerodrom = await Context.Aerodromi.FindAsync(id);
               Context.Aerodromi.Remove(aerodrom);
               await Context.SaveChangesAsync();
               return Ok("Uspesno obrisan aerodrom");
            }
            catch(Exception exception)
            {
               return BadRequest(exception.Message);
            }
        }*/





    }
}

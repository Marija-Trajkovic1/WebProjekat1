using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
namespace Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DestinacijaController : ControllerBase
    {
        public AerodromContext Context { get; set; }

        public DestinacijaController(AerodromContext context)
        {
            Context=context;
        }

        [Route("VratiDestinacije/{IdAerodroma}")]
        [HttpGet]
        public async Task<ActionResult> VratiDestinacije(int IdAerodroma)
        {
            try{
                var aerodrom=await Context.Aerodromi.Where(p=>p.ID==IdAerodroma).FirstAsync();
                if(aerodrom==null)
                    throw new Exception("Ne postoji takav aerodrom");
                var destinacije = await Context.Destinacije.Where(p=>p.DestinacijaAerodrom.ID== IdAerodroma).ToListAsync();
                return Ok(
                    destinacije.Select(p=>
                    new{
                        ID=p.ID,
                        NazivDestinacije=p.NazivDestinacije, 
                        Tip=p.Tip
                    }).ToList()
                );

                   
            } catch(Exception exception)
            {
               return BadRequest(exception.Message);
            }
        }

        [Route("UpisiDestinacijuUTabelu")]
        [HttpPost]
        public async Task<ActionResult> UpisiDestinacijuUTabelu([FromBody] Destinacija destinacija)
        {

           if(string.IsNullOrWhiteSpace(destinacija.NazivDestinacije) || destinacija.NazivDestinacije.Length>30)
           {
               return BadRequest("Naziv destinacije nije korektan!");
           }

           if(string.IsNullOrWhiteSpace(destinacija.Tip) || destinacija.Tip.Length>30)
           {
               return BadRequest("Tip destinacije nije korektan!");
           }

           try{
               Context.Destinacije.Add(destinacija);
               await Context.SaveChangesAsync();
               return Ok("Destinacija je dodata!");
           }
           catch(Exception exception)
           {
               return BadRequest(exception.Message);
           }
       }

       /*[Route("ObrisiDestinaciju")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiDestinaciju(int id)
        {
            if(id<=0)
            {
               return BadRequest("Pogresan ID!");
            }
            try
            {
               var destinacija = await Context.Destinacije.FindAsync(id);
               Context.Destinacije.Remove(destinacija);
               await Context.SaveChangesAsync();
               return Ok("Uspesno obrisana destinacija");
            }
            catch(Exception exception)
            {
               return BadRequest(exception.Message);
            }
        }*/

    }
}
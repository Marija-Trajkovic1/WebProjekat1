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
    public class SedisteController : ControllerBase
    {
        public AerodromContext Context { get; set; }

        public SedisteController(AerodromContext context)
        {
            Context=context;
        }

        [Route("VratiRezervisanaSedista/{IdPolaska}")]
        [HttpGet]
        public async Task<ActionResult> VratiSediste(int IdPolaska)
        {
            try{
                var polazak=await Context.Letovi.Where(p=>p.ID==IdPolaska).FirstAsync();
                if(polazak==null)
                    throw new Exception("Ne postoji takav polazak");
                var sedista = await Context.Sedista.Where(p=>p.SedisteLet.ID== IdPolaska).ToListAsync();
                return Ok(
                    sedista.Select(p=>
                    new{
                        ID=p.ID,
                        RedniBrojSedista=p.RedniBrojSedista, 
                        RezervisanoSediste=p.RezervisanoSediste,
                        TipSedista=p.TipSedista
                    }).ToList()
                );

                   
            } catch(Exception exception)
            {
               return BadRequest(exception.Message);
            }
        }

        [Route("UpisiPutnikaUSediste/{IdPutnika}/{IdSedista}")]
        [HttpPut]
        public async Task<ActionResult> UpisiPutnikaUSediste(int IdPutnika, int IdSedista){
            {
                try{
                    var sediste = await Context.Sedista.Where(p=>p.ID==IdSedista).FirstOrDefaultAsync();
                    if(sediste==null)
                        return BadRequest("Ne postoji sediste");
                    var putnik = await Context.Putnici.Where(p=>p.ID==IdPutnika).FirstOrDefaultAsync();
                    if (putnik==null)
                        return BadRequest("Ne postoji putnik");

                    sediste.RezervisanoSediste=true;
                    sediste.SedistePutnik=putnik;
                    //putnik.Sedista.Add(sediste);
                    await Context.SaveChangesAsync();
                    
                    return Ok("Rezervacija uspesna");
                }catch(Exception e){
                    return BadRequest(e.Message);
                }
            }
        }
        

    }
}
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
    public class PutnikController : ControllerBase
    {
        public AerodromContext Context { get; set; }

        public PutnikController(AerodromContext context)
        {
            Context=context;
        }

        [Route("VratiPutnika/{brPasosa}")]
        [HttpGet]
        public async Task<ActionResult> VratiPutnika(int brPasosa)
        {
            try{
                var putnik=await Context.Putnici
                .Where(p=>p.BrojPasosa==brPasosa)
                .Select(p=>new {ID=p.ID, BrojPasosa=p.BrojPasosa, Ime=p.Ime, Prezime=p.Prezime, TezinaPrtljagaUKg=p.TezinaPrtljagaUKg}).FirstAsync();
                if(putnik==null)
                    throw new Exception("Ne postoje podaci za putnika");
                return Ok(putnik);
            } catch(Exception exception)
            {
               return BadRequest(exception.Message);
            }

        }

        [Route("DodajNovogPutnika/{Ime}/{Prezime}/{BrojPasosa}/{TezinaPrtljagaUKg}")]
        [HttpPost]

        public async Task<ActionResult> DodajNovogPPutnika(string Ime, string Prezime, int BrojPasosa, int TezinaPrtljagaUKg)
        {
            if(string.IsNullOrWhiteSpace(Ime) || Ime.Length>20)
                return BadRequest("Ime nije ispravno!");
            if(string.IsNullOrWhiteSpace(Prezime) || Prezime.Length>20)
                return BadRequest("Ime nije ispravno!");
            if(BrojPasosa<100000000 || BrojPasosa>999999999)
                return BadRequest("Neispravan pasos!"); 
            if (TezinaPrtljagaUKg<0 || TezinaPrtljagaUKg>100)   
                return BadRequest("Nedozvoljena tezina");
            
           try{
               Putnik putnik=new Putnik();
               putnik.Ime=Ime;
               putnik.Prezime=Prezime;
               putnik.BrojPasosa=BrojPasosa;
               putnik.TezinaPrtljagaUKg=TezinaPrtljagaUKg;

               Context.Putnici.Add(putnik);
               await Context.SaveChangesAsync();

                return Ok("Dodat putnik");

           }catch(Exception e){
               return BadRequest(e.Message);
           }
        }
        [Route("ObrisiPutnika/{IdPutnika}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiPutnika(int IdPutnika){
            {
                try{
                    var putnik = await Context.Putnici.Where(p=>p.ID==IdPutnika).FirstOrDefaultAsync();
                    if (putnik==null)
                        return BadRequest("Ne postoji putnik");
                    var sedista = await Context.Sedista.Where(p=>p.SedistePutnik.ID==IdPutnika).ToListAsync();
                    foreach(var sediste in sedista){
                        sediste.RezervisanoSediste=false;//da postavim na slobodno
                        sediste.SedistePutnik=null;
                    }
                    
                    Context.Putnici.Remove(putnik);
                    await Context.SaveChangesAsync();
                    
                    return Ok("Uspesno obrisan putnik");
                }catch(Exception e){
                    return BadRequest(e.Message);
                }
            }
        }
    
    }
}
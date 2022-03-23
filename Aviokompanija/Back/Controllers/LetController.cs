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
    public class LetController : ControllerBase
    {
        public AerodromContext Context { get; set; }

        public LetController(AerodromContext context)
        {
            Context=context;
        }

        [Route("UpisiLetUTabelu")]
        [HttpPost]
        public async Task<ActionResult> UpisiLetUTabelu([FromBody] Let let)
        {
            if(let.UkupanBrojSedista<1 || let.UkupanBrojSedista>250)
            {
                return BadRequest("Neispravan broj sedista");
            }

            if(let.BrojZauzetih>let.UkupanBrojSedista)
            {
                return BadRequest("Neispravan broj sedista");
            }

            try{
               Context.Letovi.Add(let);
               await Context.SaveChangesAsync();
               return Ok("Let je dodat!");
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Route("VratiLetove/{idDestinacije}")]
        [HttpGet]
        public async Task<ActionResult> VratiLetove(int idDestinacije)
        {
            try{
                var destinacija=await Context.Destinacije.Where(p=>p.ID==idDestinacije).FirstAsync();
                if(destinacija==null)
                    throw new Exception("Ne postoji takva destinacija");
                var letovi=await Context.Letovi.Where(p=>p.LetoviDestinacije.ID==idDestinacije).ToListAsync();
                return Ok(
                    letovi.Select(p=>
                    new{
                        ID=p.ID,
                        VremePoletanja=p.VremePoletanja,
                        VremeSletanja=p.VremeSletanja,
                        UkupanBrojSedista=p.UkupanBrojSedista,
                        BrojZauzetih=p.BrojZauzetih
                    }).ToList()
                );
                   
            } catch(Exception exception)
            {
               return BadRequest(exception.Message);
            }
        }

        [Route("VratiIzabraniLet/{idLeta}")]
        [HttpGet]
        public async Task<ActionResult> VratiIzabraniLet(int idLeta)
        {
            try{
                var polazak=await Context.Letovi
                .Where(p=>p.ID==idLeta)
                .Select(p=>new {ID=p.ID, VremePoletanja=p.VremePoletanja, VremeSletanja=p.VremeSletanja, UkupanBrojSedista=p.UkupanBrojSedista, BrojZauzetih=p.BrojZauzetih}).FirstAsync();
                if(polazak==null)
                    throw new Exception("Ne postoji takav let");
                return Ok(polazak);
            } catch(Exception exception)
            {
               return BadRequest(exception.Message);
            }

        }


    }
}
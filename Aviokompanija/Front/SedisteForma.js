import { Let } from "./Let.js";
import { LetForma } from "./LetForma.js";
import { Sediste } from "./Sediste.js"

export class SedisteForma{
    constructor(idselektovaniPolazak){
        this.idselektovaniPolazak=idselektovaniPolazak;
        this.listaSedista=[];
        this.polazak=null;
        this.putnik=null;
        this.brojSedistaUAvionu;
        this.idselektovanogSedista=0;
        this.idPutnika=0;
        this.ukupnoSedista;
    }

    crtajPrikazSedista(host){
    
        //console.log(this.idselektovaniPolazak); radi ok
        let zauzetaMesta=document.createElement("label");
        //fetch da mi za dati id vrati broj rezervisanih sedista i ukupan broj sedista
        fetch("https://localhost:5001/Let/VratiIzabraniLet/" + this.idselektovaniPolazak,
        {
            method: "GET"
        })
        .then(p => {
            if (p.status != 200) {
                window.alert("Nije moguce prikazati let!");
            }
            else {
                //console.log(p);
                p.json().then(polazakizJson => {
                    this.polazak = new Let(polazakizJson.id, polazakizJson.vremePoletanja, polazakizJson.vremeSletanja, polazakizJson.ukupanBrojSedista, polazakizJson.brojZauzetih)
                    //console.log(this.polazak);
                   //zauzetaMesta.innerHTML="Broj zauzetih mesta: "+this.polazak.BrojZauzetih +" od ukupno: "+this.polazak.UkupanBrojSedista;
                    this.brojSedistaUAvionu=this.polazak.UkupanBrojSedista;
                    this.ukupnoSedista=this.polazak.BrojZauzetih;
                });
            }
        });

        
        
        //host.appendChild(zauzetaMesta);
        let putnickaKabinaDiv=document.createElement("div");
        putnickaKabinaDiv.className="PutnickaKabinaDiv";
        host.appendChild(putnickaKabinaDiv);

//da mi vrati zauzeta sedista na letu 
        fetch("https://localhost:5001/Sediste/VratiRezervisanaSedista/"+ this.idselektovaniPolazak,
        {
            method:"GET"
        })
        .then(p=>{
            if(p.status!=200)
            {
                window.alert("Nije moguce ucitati sediste");
            }
            else
            {
                p.json().then(sedista=>{
                    sedista.forEach(sediste=>{
                        var seat=new Sediste(sediste.id, sediste.redniBrojSedista, sediste.rezervisanoSediste, sediste.tipSedista);
                       // console.log(seat);
                        this.listaSedista.push(seat);
                                              
                    })
                    this.nacrtajKabinu(putnickaKabinaDiv); 
                }) 
            }
        })
        
        let putnikDiv=document.createElement("div");
        putnikDiv.className="PutnikDiv";
        host.appendChild(putnikDiv);
        this.crtajPutnika(putnikDiv);

    }

    nacrtajKabinu(host){
    
        let izaberiteSvojeSedistelbl=document.createElement("label");
        izaberiteSvojeSedistelbl.innerHTML="Izaberite slobodno sediste i pretrazite putnika po pasosu da bi ste rezervisali!";
        host.appendChild(izaberiteSvojeSedistelbl);
        let formaSedista=document.createElement("div");
        formaSedista.className = "formaSedista";
        host.appendChild(formaSedista);

        //crtam sedista
        
        this.listaSedista.forEach(sediste =>{
           console.log(sediste);
            let btnSediste = document.createElement("button");
            btnSediste.className = "sedisteDugme";
            btnSediste.value=sediste.ID;
            if(sediste.RezervisanoSediste)
            {
                btnSediste.innerHTML = "rezervisano";
                btnSediste.className="Rezervisano";
               btnSediste.onclick=(e)=>{window.alert("Nije moguce selektovanje rezervisanog sedista!");};
                //console.log(this.idselektovanogSedista)
                //window.alert("Nije moguce selektovanje rezervisanog sedista!");
            }else {
                btnSediste.innerHTML = "slobodno";
                btnSediste.className = "Slobodno";
                btnSediste.onclick=(e)=>{
                    this.idselektovanogSedista=btnSediste.value;
                    //console.log(this.idselektovanogSedista);
                };
                
            }
           //console.log(btnSediste.value);
          formaSedista.appendChild(btnSediste);
        
  
        });
        
    }
   
    crtajPutnika(host){
        let imelbl=document.createElement("label");
        imelbl.innerHTML="Ime: ";
        host.appendChild(imelbl);
            
        let imeInput=document.createElement("input");
        imeInput.type="text";
        imeInput.className="ImeUnos";
        host.appendChild(imeInput);
         
        let prezimelbl=document.createElement("label");
        prezimelbl.innerHTML="Prezime: ";
        host.appendChild(prezimelbl);
        let prezimeInput=document.createElement("input");
        prezimeInput.type="text";
        prezimeInput.className="PrezimeUnos";
        host.appendChild(prezimeInput);
            
        let brojPasosaLbl=document.createElement("label");
        brojPasosaLbl.innerHTML="Broj vašeg pasoša: ";
        host.appendChild(brojPasosaLbl);
            
        let brojPasosaInput=document.createElement("input");
        brojPasosaInput.type="number";
        brojPasosaInput.className="brojPasosaUnos";
        host.appendChild(brojPasosaInput);
            
        let tezinaPrtljagaLbl=document.createElement("label");
        tezinaPrtljagaLbl.innerHTML="Tezina vašeg prtljaga: ";
        host.appendChild(tezinaPrtljagaLbl);
            
        let tezinaPrtljagaInput=document.createElement("input");
        tezinaPrtljagaInput.type="number";
        tezinaPrtljagaInput.className="tezinaPrtljagaUnos";
        host.appendChild(tezinaPrtljagaInput);
            
        let dodajBtn=document.createElement("button");
        dodajBtn.innerHTML="Dodaj putnika";
        host.appendChild(dodajBtn);
        dodajBtn.onclick=(e)=>{
            let ime = imeInput.value;
            let prezime = prezimeInput.value;
            let brojPasosa = brojPasosaInput.value;
            let tezinaPrtljaga = tezinaPrtljagaInput.value;
            if(ime && prezime && brojPasosa && tezinaPrtljaga){
                imeInput.value = "";
                prezimeInput.value = "";
                brojPasosaInput.value = "";
                tezinaPrtljagaInput.value = "";
                this.dodajPutnika(ime, prezime, brojPasosa, tezinaPrtljaga);
            }
            else
                window.alert("Nisu uneti validni podaci!");
        }
        let lblPasosPretrazi = document.createElement("label");
        lblPasosPretrazi.className= "labela";
        lblPasosPretrazi.innerHTML= "Unesi broj pasosa za pretragu";
        host.appendChild(lblPasosPretrazi);
        let pasosPretraziInout = document.createElement("input");
        pasosPretraziInout.type = "number";
        pasosPretraziInout.className = "pasosUnos";
        host.appendChild(pasosPretraziInout);

        let pretraziBtn=document.createElement("button");
        pretraziBtn.innerHTML="Pretrazi putnika";
        host.appendChild(pretraziBtn);

        let putnikInfo = document.createElement("label");
        putnikInfo.className= "labela";
        putnikInfo.innerHTML= "";
        host.appendChild(putnikInfo);
        pretraziBtn.onclick=(e)=>{
            if(!pasosPretraziInout.value){
                window.alert("Napisi broj pasosa!");
                return;
            }
            fetch("https://localhost:5001/Putnik/VratiPutnika/" + pasosPretraziInout.value ).then(p=>{
                if(p.status == 200){
                p.json().then(putnik =>{
                    this.idPutnika = putnik.id;
                    imeInput.value = putnik.ime;
                    prezimeInput.value = putnik.prezime;
                    brojPasosaInput.value = putnik.brojPasosa;
                    tezinaPrtljagaInput.value = putnik.tezinaPrtljagaUKg;
                });
                }else{
                    window.alert("Putnik nije pronadjen!");
                }
            });
        }

        

        let obrisiBtn=document.createElement("button");
        obrisiBtn.innerHTML="Obrisi putnika";
        host.appendChild(obrisiBtn);
        obrisiBtn.onclick=(e)=>{
            if(!this.idPutnika){
                window.alert("Prvo pretrazi putnika!");
                return;
            }
            fetch("https://localhost:5001/Putnik/ObrisiPutnika/" + this.idPutnika, {method : "DELETE"}).then(p=>{
                if(!(p.status == 200)){
                    window.alert("Greska pri bisanju!");
                }else{
                    window.alert("Uspesno obrisan!");
                    imeInput.value = "";
                    prezimeInput.value = "";
                    brojPasosaInput.value = "";
                    tezinaPrtljagaInput.value = "";
                    pasosPretraziInout.value = "";
                    this.idPutnika = 0;
                    let select = document.querySelector(".ListaIzborLeta");
                    select.onchange();
                }
            });
        }
        let rezervisiBtn = document.createElement("button");
        rezervisiBtn.className = "rezervisiDugme";
        rezervisiBtn.innerHTML="Rezervisi";
        host.appendChild(rezervisiBtn);
        rezervisiBtn.onclick = e =>{

            fetch(`https://localhost:5001/Sediste/UpisiPutnikaUSediste/${this.idPutnika}/${this.idselektovanogSedista}`, {method : "PUT"})
            .then(p=>{
                if(p.status == 200){
                    window.alert("Uspesna rezervacija!");
                    let div = document.querySelector(".formaSedista");
                    let dugmici = div.querySelectorAll(".Slobodno");
                    dugmici.forEach(deca =>{
                        console.log(deca);
                        if(deca.value == this.idselektovanogSedista){
                            deca.className = "Rezervisano";
                            deca.innerHTML = "rezervisano";
                        }
                    });
                }else{
                    window.alert("Nije uspesna rezervacija");
                }
            });
        }
    }

    dodajPutnika(ime, prezime, brojPasosa, tezinaPrtljaga){
        fetch(`https://localhost:5001/Putnik/DodajNovogPutnika/${ime}/${prezime}/${brojPasosa}/${tezinaPrtljaga}`, {method : "POST"})
            .then(p =>{
                if(!(p.status == 200)){
                    window.alert("Greska pri dodavanju putnika!");
                }else{
                    window.alert("Putnik uspesno dodat!");
                }
            });
    }
}
/*
    Obrisi(){
        //da vratim id putnika kog zelim da obrisem
        fetch("https://localHost:5001/Putnik/VratiPutnika"+this.brPasosa,
        {
            method:"GET"
        })
        .then(p=>{
        if(p.status!=200)
        {
            window.alert("Nije moguce ucitati putnika!");
        } 
        else{
            p.json().then(putnikizJson => {
                this. = new Let(polazakizJson.id, polazakizJson.vremePoletanja, polazakizJson.vremeSletanja, polazakizJson.ukupanBrojSedista, polazakizJson.brojZauzetih)
                
                this.brojSedistaUAvionu=this.polazak.UkupanBrojSedista;
                this.ukupnoSedista=this.polazak.BrojZauzetih;
            });
        }


               
        
        if (confirm("Stvarno zelis da obrises svoje podatke?")) {

            fetch("https://localhost:5001/Putnik/ObrisiPutnika/" + AktivnostID, { method: "DELETE" }).then(p => {
                if (!p.ok) {
                    window.alert("Nije moguce obrisati aktivnost!");
                }
                this.pribaviAktivnosti();
            });
        }
        
    }*/

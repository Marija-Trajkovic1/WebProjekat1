import { Aerodrom } from "./Aerodrom.js";
import { Destinacija } from "./Destinacija.js"
import { LetForma } from "./LetForma.js";

export class AerodromForma{
    constructor(idAerodroma){
        this.idAerodroma=idAerodroma;
       // this.container=null;
    }

    crtaj(host){

        let idselektovanaDestinacija;

        let izbordestinacijelbl=document.createElement("label");
        izbordestinacijelbl.innerHTML="Izaberite zeljenu destinaciju";
        host.appendChild(izbordestinacijelbl);

        let listaIzborDestinacija=document.createElement("select");
        listaIzborDestinacija.className="ListaIzborDestinacija";
        host.appendChild(listaIzborDestinacija);

        fetch("https://localhost:5001/Destinacija/VratiDestinacije/" + this.idAerodroma,
        {
            method:"GET"
       })
        .then(p => {
        if (p.status!=200) 
        {
            window.alert("Nije moguce prikazati destinacije!");
        } 
        else
        {
            p.json().then(destinacije => {
                    destinacije.forEach(destinacija => {
                        
                        let destinacijaOpcija=document.createElement("option");
                        destinacijaOpcija.innerHTML=destinacija.nazivDestinacije;
                        destinacijaOpcija.value=destinacija.id;
                        listaIzborDestinacija.appendChild(destinacijaOpcija);
                        //console.log(destinacijaOpcija.value);
                    
                    });
                    idselektovanaDestinacija=listaIzborDestinacija.options[listaIzborDestinacija.selectedIndex].value;    
                    //console.log(idselektovanaDestinacija); //radi
                    prikaziVremenaLeta();
                    });
               
            }
            
        });

        listaIzborDestinacija.onclick = (ev) => {
           idselektovanaDestinacija= listaIzborDestinacija.options[listaIzborDestinacija.selectedIndex].value;
            obrisiDestinacije();
            prikaziVremenaLeta();
        }

        let letDiv=document.createElement("div");
        letDiv.className="LetDiv";
        host.appendChild(letDiv);

        function prikaziVremenaLeta()
        {
            let crtajLetove=new LetForma(idselektovanaDestinacija);
            crtajLetove.crtajLet(letDiv);
        }

        function obrisiDestinacije()
        {
            while(letDiv.firstChild)
                letDiv.removeChild(letDiv.firstChild);
        }
    }
}
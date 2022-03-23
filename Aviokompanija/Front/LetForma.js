import { Aerodrom } from "./Aerodrom.js";
import { Destinacija } from "./Destinacija.js"
import { Let } from "./Let.js"
import { AerodromForma } from "./AerodromForma.js";
import { SedisteForma } from "./SedisteForma.js";

export class LetForma{
    constructor(idDestinacije){
        this.idDestinacije=idDestinacije;
        this.idselektovanoVreme = 0;
        this.listaIzborLeta;
    }

    crtajLet(host){
       
        
        let izborletalbl=document.createElement("label");
        izborletalbl.innerHTML="Izaberite zeljeni termin leta";
        host.appendChild(izborletalbl);

        let listaIzborLeta=document.createElement("select");
        listaIzborLeta.className="ListaIzborLeta";
        host.appendChild(listaIzborLeta);

        fetch("https://localhost:5001/Let/VratiLetove/" + this.idDestinacije,
        {
            method:"GET"
       })
        .then(p => {
        if (p.status!=200) 
        {
            window.alert("Nije moguce prikazati letove!");
        } 
        else
        {
            p.json().then(letovi => {
                letovi.forEach(vremeleta => {
                        //console.log(vremeleta);
                        let letPoletanjeOpcija=document.createElement("option");
                        letPoletanjeOpcija.innerHTML=vremeleta.vremePoletanja;
                        letPoletanjeOpcija.value=vremeleta.id;
                        listaIzborLeta.appendChild(letPoletanjeOpcija);                      
                        //console.log(letPoletanjeOpcija.value);
                        
                    
                    });
                    //console.log(listaIzborLeta.options[listaIzborLeta.selectedIndex].value);
                    this.idselektovanoVreme=listaIzborLeta.options[listaIzborLeta.selectedIndex].value;          
                    //console.log(this.idselektovanoVreme);
                    prikaziSedista();
                });
            }
        });

        listaIzborLeta.onchange = (ev) => {
            this.idselektovanoVreme=listaIzborLeta.options[listaIzborLeta.selectedIndex].value;
            obrisiSedista();
            prikaziSedista();
        }

        let sedistaDiv=document.createElement("div");
        sedistaDiv.className="SedistaDiv";
        host.appendChild(sedistaDiv);
    
        const prikaziSedista = () => 
        {
            console.log(this.idselektovanoVreme);
            let crtajSedista=new SedisteForma(this.idselektovanoVreme);
            crtajSedista.crtajPrikazSedista(sedistaDiv);
        }
    
        const obrisiSedista = ()=>
        {
            while(sedistaDiv.firstChild)
                sedistaDiv.removeChild(sedistaDiv.firstChild);
        }
    }
   
}
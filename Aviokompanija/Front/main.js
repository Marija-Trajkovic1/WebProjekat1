import { Aerodrom } from "./Aerodrom.js";
import { AerodromForma } from "./AerodromForma.js"
import {Destinacija} from "./Destinacija.js"

let idselektovanAerodrom;

let izborAerodromaDiv=document.createElement("div");
izborAerodromaDiv.className="PrikazIzboraAerodroma";
document.body.appendChild(izborAerodromaDiv);

let ListaIzborAerodromaLbl=document.createElement("label");
ListaIzborAerodromaLbl.innerHTML="Izaberite aerodrom iz liste";
izborAerodromaDiv.appendChild(ListaIzborAerodromaLbl);

let listaIzborAerodroma=document.createElement("select");
listaIzborAerodroma.className="ListaIzborAerodroma";
izborAerodromaDiv.appendChild(listaIzborAerodroma);

fetch("https://localhost:5001/Aerodrom/VratiAerodrome")
.then(p => {
    if (!p.ok) 
    {
        window.alert("Nije moguce prikazati aerodrome!");
    } 
    else
    {
        p.json().then(aerodromi => {
            aerodromi.forEach(aerodrom => {
                let aerodromOpcija = document.createElement("option");
                aerodromOpcija.innerHTML = aerodrom.nazivAerodroma+", "+aerodrom.lokacija;
                aerodromOpcija.value = aerodrom.id;
                //console.log(aerodrom.id); ok
                listaIzborAerodroma.appendChild(aerodromOpcija);
            });
            idselektovanAerodrom = listaIzborAerodroma.options[listaIzborAerodroma.selectedIndex].value;
           // console.log(idselektovanAerodrom); ok
            prikaziAerodrom();
        })
    }
});

listaIzborAerodroma.onclick = (ev) => {
    idselektovanAerodrom = listaIzborAerodroma.options[listaIzborAerodroma.selectedIndex].value;
    obrisiAerodrom();
    prikaziAerodrom();
}

let aerodromDiv=document.createElement("div");
aerodromDiv.className="AerodromDiv";
document.body.appendChild(aerodromDiv);

function prikaziAerodrom()
{
    //obrisiAerodrom();
    let crtajAerodromce=new AerodromForma(idselektovanAerodrom);
    crtajAerodromce.crtaj(aerodromDiv);
}

function obrisiAerodrom()
{
    while(aerodromDiv.firstChild)
        aerodromDiv.removeChild(aerodromDiv.firstChild);
}
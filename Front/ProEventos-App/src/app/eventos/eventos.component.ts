import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {Evento} from "../types/Evento";
@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  public eventos: Array<Evento> = [];
  visibilidadeImg = true;
  private _filtroLista: string = "";

  public get filtroLista(){
    return this._filtroLista;
  }

  public set filtroLista(value:string){
    this._filtroLista = value.trim();
    if(this._filtroLista){
      this.filtrarEventos(value);
    } else{
      this.getEventos();
    }
  }

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getEventos();
    //this.eventos.forEach(e => e.dataEvento = new Date(e.dataEvento).toLocaleDateString("pt-BR"))
  }

  private getEventos(): void{
    this.http.get("https://localhost:5001/api/eventos").subscribe(
      {
        next: res => this.eventos = res as Array<Evento>,
        error: err => console.error(err)
      }
    );
  }

  private filtrarEventos(value: string): void{
    const filtrarPor = value.toUpperCase()
    this.eventos = this.eventos.filter(e => e.tema.toUpperCase().indexOf(filtrarPor) !== -1 || 
    e.local.toUpperCase().indexOf(filtrarPor) !== -1);
  }

  formatDate(date: Date): string 
  {
    let datestring: string = date.toString();
    const charIndex = datestring.indexOf("T");
    const dateSemTempo = datestring.substring(0, charIndex).split("-");
    datestring = `${dateSemTempo[2]}/${dateSemTempo[1]}/${dateSemTempo[0]}`
    return datestring;
  }

  setVisibilidadeImg(){
    this.visibilidadeImg = !this.visibilidadeImg;
  }
}

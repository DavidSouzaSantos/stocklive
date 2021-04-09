import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {

  produtos:any = [
    {image:"img1.jpg", id:"1", name:"Caneta Bic", description: "Caneta de escritorio da cor azul.", status:"Ativo"},
    {image:"img2.jpg", id:"2", name:"Lapis", description: "Lapis de escritorio da cor preta.", status:"Ativo"},
    {image:"img3.jpg", id:"3", name:"Régua", description: "Régu azul de 30cm.", status:"Ativo"},
    {image:"img4.jpg", id:"4", name:"Caderno", description: "Caderno azul 1 matéria.", status:"Inativo"}
  ];
  produtosFiltrados:any = [];

  mostrarImagem = true;

  _filtroLista = '';

  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    this._filtroLista = value;
    this.produtosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.produtos;    
  }

  constructor() { }

  ngOnInit() {
    this.produtosFiltrados = this.produtos;
  }

 
 filtrarEventos(filtrarPor: string) { 
   filtrarPor = filtrarPor.toLocaleLowerCase() 
   return this.produtos.filter(produto => { 
     return produto.name.toLocaleLowerCase().includes(filtrarPor) 
   }) 
 }

 novoProduto(template:any){

 }

 alternarImagem(){}

 editarProduto(){}
 excluirProduto(){};

}

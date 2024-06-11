import { Component } from '@angular/core';
import $ from 'jquery';
import * as bootstrap from 'bootstrap';
import { OnInit } from '@angular/core';
import { BookService } from '../book.service';
@Component({
  selector: 'app-home2',
  templateUrl: './home2.component.html',
  styleUrl: './home2.component.css'
})
export class Home2Component implements OnInit {
  constructor(public service:BookService){}
  ngOnInit() {
    this.service.getAllBooks();
    
    this.service.book={
      id: 0,
      title: "",
      author:"",
      numberOfPages:0,
      publishedAt:"",
      image:"",
      filePdf:"",
      discription:"",
      categoryId:1,
  
      
   };
  }}

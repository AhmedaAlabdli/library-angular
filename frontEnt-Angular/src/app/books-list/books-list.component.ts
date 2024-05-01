import { Component, OnInit } from '@angular/core';
import { BookService } from '../book.service';

@Component({
  selector: 'app-books-list',
  templateUrl: './books-list.component.html',
  styleUrl: './books-list.component.css'
})
export class BooksListComponent implements OnInit{

  constructor(public service:BookService){}
  ngOnInit() {
    this.service.getAllBooks();

  }

  // private readonly newProperty = this.service.book;

  fillData(item:any): void{
    this.service.book.id=item.id;
    this.service.book.title=item.title;
    this.service.book.author=item.author;
    this.service.book.numberOfPages=item.numberOfPages;
    this.service.book.publishedAt=item.publishedAt;
  }

  
  delete(id:any){
    this.service.deleteBook(id).subscribe(res=>{
      this.service.getAllBooks();
    })

  }

}
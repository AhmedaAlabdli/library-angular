import { Component, OnInit } from '@angular/core';
import { BookService } from '../book.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-book-item',
  templateUrl: './book-item.component.html',
  styleUrl: './book-item.component.css'
})
export class BookItemComponent implements OnInit{

  constructor(public service:BookService){}
  uploadImage(event: any)
    {
      debugger;
    }
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

}
// form: any;
// submit(arg0: any) {
// throw new Error('Method not implemented.');
// }

submit(){

  if(this.service.book?.id==0)
    {
      this.service.postBook().subscribe(res=>{
        this.service.getAllBooks()
      },
      err=>{
        console.log(err)
      }
    )
    }
    else
    {
      this.service.putBook().subscribe(res=>{
        this.service.getAllBooks()
      },
      err=>{
        console.log(err)
      }
    )
    }
    }

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
    


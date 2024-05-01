import { NgModule ,NO_ERRORS_SCHEMA} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BooksListComponent } from './books-list/books-list.component';
import { BookItemComponent } from './book-item/book-item.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { NgToastModule } from 'ng-angular-popup';
// import { ToastrModule } from 'ngx-toastr';
import { AuthGuard } from './guards/auth.guard';
import { CommonModule } from '@angular/common';
//import { ToastrModule } from 'ngx-toastr';
//import { BooksPageComponent } from './books-page/books-page.component';





@NgModule({
  declarations: [
    AppComponent,
    BooksListComponent,
    BookItemComponent,
    LoginComponent,
    SignupComponent,
    
   // BooksPageComponent
    
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgToastModule,
    BrowserAnimationsModule,
    //ToastrModule.forRoot(),
    CommonModule,
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent],
  
  
  schemas:[NO_ERRORS_SCHEMA]
})
export class AppModule { }

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { BookItemComponent } from './book-item/book-item.component';
import { AuthGuard } from './guards/auth.guard';
import { HomeComponent } from './home/home.component';
//import { BooksPageComponent } from './books-page/books-page.component';
import { Home2Component } from './home2/home2.component';

const routes: Routes = [

  {path:'login',redirectTo:'login',pathMatch:'full'},
  {path:'',component:LoginComponent},
  {path:'signup',component:SignupComponent},
  {path:'home',component:HomeComponent},
  {path:'dashboard',component:BookItemComponent}, //canActivate:[AuthGuard]
  {path:'home2',component:Home2Component}



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

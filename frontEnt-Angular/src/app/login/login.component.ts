import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit{

  loginForm!:FormGroup;
  constructor( 
    private fb:FormBuilder,
    private auth:AuthService,
    private router:Router,
    // private toast:NgToastService,
    //private toastr:ToastrService,
  ){}


  ngOnInit(): void {
    this.loginForm=this.fb.group({
      UserName:['',Validators.required],
      Password:['',Validators.required]
    })
  }

  onLogin(){
    if(this.loginForm.valid)
      {
        //console.log(this.loginForm.value)
        this.auth.login(this.loginForm.value).subscribe({
          next:(res=>{
            alert(res.message);
            // this.toast.success({detail:"تسجيل الدخول",summary:res.massege,duration:5000});
            //this.toastr.success('تسجيل الدخول','تسجيل الدخول بنجاح')
            this.loginForm.reset();
            this.auth.storeToken(res.token);
            this.router.navigate(['dashboard'])
          }),
          error:(err)=>{
            alert(err.error.massege)
            // this.toast.success('تسجيل الدخول','تسجيل الدخول بنجاح')

            // this.toast.error({detail:"تسجيل الدخول",summary:"هناك خطاء في تسجيل الدخول",duration:5000});
          }
        })
      }
    else
    this.validateAllFormFileds(this.loginForm);
    // alert("صفحة تسجيل الدخول غير متوفره")
  }

  private validateAllFormFileds(formGroup:FormGroup){
    Object.keys(formGroup.controls).forEach(field=>{
      const control=formGroup.get(field);
      if(control instanceof FormControl){
        control.markAsDirty({onlySelf:true});
      }
      else if(control instanceof FormGroup){
        this.validateAllFormFileds(control)
      }
    })
  }

}

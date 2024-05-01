import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent implements OnInit {

  signUpForm!:FormGroup;
  router: any;
  constructor(private fb:FormBuilder,private auth: AuthService ){}
  ngOnInit(): void {
    this.signUpForm=this.fb.group({
      fullName:['',Validators.required],
      phoneNumber:['',Validators.required],
      email:['',Validators.required],
      userName:['',Validators.required],
      password:['',Validators.required],


    })

  }
  onSignup(){
    if(this.signUpForm.valid){
      // perform logic for signup
      this.auth.signUp(this.signUpForm.value).subscribe({
        
        next:((res)=>{
          alert(res.message);
          this.signUpForm.reset();
          this.router.navigate(['/login'])
          console.log(this.signUpForm.value)
        })
        ,error:(err=>{
          alert(err.error.message)
          console.log(this.signUpForm.value)
        })
        
      })
      
    }
    else
    {
      //logic for throwing error
      this.validateAllFormFileds(this.signUpForm)
      alert("يجب تعبية جميع البيانات")
    }
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

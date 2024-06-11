// import { Injectable } from '@angular/core';
// import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
// import { AuthService } from '../auth.service';

// @Injectable({
//   providedIn:'root'
// })
// export class AuthGuard implements CanActivate {
//   constructor(private Auth:AuthService,private router: Router) {}

//   canActivate():boolean {
//      const token = localStorage.getItem('token');
//     // if (token) {
//     //   // المستخدم مصرح له بالوصول
//     //   alert('المستخدم مصرح له بالوصول')
//     //   return true;
//     // } else {
//     //   // توجيه المستخدم إلى صفحة تسجيل الدخول
//     //   this.router.navigate(['login']);
//     //   alert('يجب عليك تسجيل الدخول اولاً !')
//     //   return false;
//     // // return !!token;
//     if(this.Auth.isLoggedIn()){
//       return true;
//     }
//     else
//     {
//       return false;
//     }
//    }
//   }

// في ملف auth.guard.ts

import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../auth.service'; // افترض أن لديك خدمة للمصادقة

@Injectable({
  providedIn:'root'
})

export class AuthGuard implements CanActivate {
  constructor(private auth: AuthService, private router: Router) {}

  canActivate(): boolean {
    if (this.auth.isLoggedIn()) {
      // المستخدم مسجل دخولًا، يمكنه الوصول إلى لوحة التحكم
      // alert(' المستخدم مسجل دخولًا، يمكنه الوصول إلى لوحة التحكم')
      return true;
    } else {
      // المستخدم غير مسجل دخولًا، قم بتوجيهه إلى صفحة تسجيل الدخول
      alert('يجب عليك تسجيل الدخول اولاً !')
      this.router.navigate(['login']);
      return false;
    }
  }
}


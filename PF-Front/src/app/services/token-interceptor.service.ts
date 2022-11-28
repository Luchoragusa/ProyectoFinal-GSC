import { HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService implements HttpInterceptor {

  constructor() { }

  intercept(req: HttpRequest<any>, next: HttpHandler) {

    let token = localStorage.getItem('token');
    let jwttoken = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    })  
  return next.handle(jwttoken);
  }
}

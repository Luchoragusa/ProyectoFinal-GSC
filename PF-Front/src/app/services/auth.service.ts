import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import jwtDecode from 'jwt-decode';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private _http: HttpClient, private _router: Router) { }
  
  login(person: any){
    return this._http.post(environment.url + 'account/login', person);
  }

  // Comprueba si el token esta almacenado
  loggedIn() {
    return !!localStorage.getItem('token') && this.expiredToken() > 0;
  }

  // Elimina el token del localstorage
  logout() {
    localStorage.clear();
    this._router.navigate(['/']);
  }

  // Obtiene el rol del usuario
  checkIsAdmin() {
    const payload: any = this.getDecodedToken();
    if (payload.role == "Admin") {
      return true;
    }else{
      return false;
    }
  }

  // Evalua que el tiempo de sesion del token no haya expirado
  expiredToken(): number {
    const payload: any = this.getDecodedToken();
    const actualTime = Date.now() / 1000;
    const remainingTime = (payload.exp) - actualTime;
    return remainingTime;
  }

    // Obtener el payload del token
    getDecodedToken() {
      return jwtDecode(this.getToken());
    }
    
    // Obtiene el token
    getToken() {
      return localStorage.getItem('token') || '';
    }
}

import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../services/user.service';
import * as moment from 'moment';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private userService: UserService, private router: Router) { }

  canActivate() {
    if (this.isLoggedIn()) {
      console.log(`I'm Logged in`);
      return true;
    }
    return this.isLoggedIn();
  }

  decode(jwt: string) {
    const [headerB64, payloadB64] = jwt.split('.');
    const headerStr = atob(headerB64);
    const payloadStr = atob(payloadB64);
    return {
      header: JSON.parse(headerStr),
      payload: JSON.parse(payloadStr)
    };
  }

  public isLoggedIn() {
    return moment().isBefore(this.getExpiration());
  }

  isLoggedOut() {
    return !this.isLoggedIn();
  }

  getExpiration() {
    const expiration = localStorage.getItem('expireDate');
    const expiresAt = JSON.parse(expiration);
    return moment(expiresAt * 1000);
  }
}

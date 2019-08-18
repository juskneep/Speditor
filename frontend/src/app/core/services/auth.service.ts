import { Injectable } from '@angular/core';
import * as jwt_decode from 'jwt-decode';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthenticatorModel } from '../models/authenticator-model';
import { Authenticate, Deauthenticate } from '../store/authentication/authentication.actions';
import { Store, select } from '@ngrx/store';
import { AppState } from '../store/app.state';
import { LoginModel } from 'src/app/components/user/login/models/login-model';



@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private username: string;
  private isUserAdmin: boolean;
  private isUserAuthenticated: boolean;

  baseUrl = 'https://localhost:44345/api/users';

  constructor(private http: HttpClient,
    private toastr: ToastrService,
    private store: Store<AppState>,
    private router: Router) {
    /* this.store.pipe(select(state => state.authentication))
       .subscribe(authentication => {
         this.isUserAdmin = authentication.isAdmin;
         this.isUserAuthenticated = authentication.isAuthenticated;
         this.username = authentication.username;
       })
 */
    if (localStorage.getItem('authtoken')) {
      const authtoken = localStorage.getItem('authtoken')
      try {
        const decoded = jwt_decode(authtoken);
        if (!this.isTokenExpired(decoded)) {
          this.isUserAuthenticated = true;
          let isAdmin = false;
          if (decoded.role === 'Administrator') {
            isAdmin = true;
          }

          const authData = new AuthenticatorModel(authtoken, decoded.unique_name, isAdmin, true);
          this.store.dispatch(new Authenticate(authData));
        } else {
          this.toastr.error('Invalid token', 'Warning!');
        }
      } catch (err) {
        this.toastr.error('Invalid token', 'Warning!');
      }
    }
  }

  /*register(body: RegisterModel) {
    return this.http.post(`${this.baseUrl}/register`, body)
  }*/

  login(form) {
    return this.http.post(`${this.baseUrl}/login`, form);
  }

  /* facebookLogin(body: FacebookLoginModel) {
     return this.http.post(facebookLoginUrl, body)
   }*/

  logout() {
    localStorage.clear();
    //this.store.dispatch(new Deauthenticate());
    this.toastr.success('Logout successful!')
    this.router.navigate(['/']);
  }

  isAuthenticated() {
    return this.isUserAuthenticated
  }

  isAdmin() {
    return this.isUserAdmin
  }

  getUsername() {
    return this.username
  }

  private isTokenExpired(token): boolean {
    if (token.exp === undefined) {
      return false
    }

    const date = new Date(0)
    date.setUTCSeconds(token.exp)

    return !(date.valueOf() > new Date().valueOf())
  }
}

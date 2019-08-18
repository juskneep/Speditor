import { Injectable } from '@angular/core';
import { NgModule } from '@angular/compiler/src/core';
import * as jwt_decode from 'jwt-decode';
import { HttpClient } from '@angular/common/http';
import { NgForm, Validators, FormBuilder, FormGroup, NgModel, FormControl } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { mapTo, tap, catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { AppState } from '../store/app.state';
import { ToastrService } from 'ngx-toastr';
import { AuthenticatorModel } from '../models/authenticator-model';
import { Authenticate, Deauthenticate } from '../store/authentication/authentication.actions';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private username: string;
  private isUserAdmin: boolean;
  private isUserAuthenticated: boolean;

  baseUrl = 'https://localhost:44345/api/users';

  formModel = this.formBuilder.group({
    UserName: ['', Validators.required],
    FirstName: ['', Validators.required],
    LastName: ['', Validators.required],
    Email: ['', Validators.required],
    Company: ['', Validators.required],
    Passwords: this.formBuilder.group({
      Password: ['', Validators.required],
      ConfirmPassword: ['', Validators.required]
    })
  });

  private loggedUser: string;

  constructor(private http: HttpClient,
    private toastr: ToastrService,
    private store: Store<AppState>,
    private router: Router,
    private formBuilder: FormBuilder) {
  /*  this.store.pipe(select(state => state.authentication))
      .subscribe(authentication => {
        this.isUserAdmin = authentication.isAdmin;
        this.isUserAuthenticated = authentication.isAuthenticated;
        this.username = authentication.username;
      })*/
/*
    if (localStorage.getItem('authtoken')) {
      const authtoken = localStorage.getItem('authtoken')
      try {
        const decoded = jwt_decode(authtoken);
        if (!this.isTokenExpired(decoded)) {
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
    }*/
  }

  register() {
    const body = {
      UserName: this.formModel.value.UserName,
      FirstName: this.formModel.value.FirstName,
      LastName: this.formModel.value.LastName,
      Email: this.formModel.value.Email,
      Company: this.formModel.value.Company,
      Password: this.formModel.value.Passwords.Password,
      ConfirmPassword: this.formModel.value.Passwords.ConfirmPassword
    };
    return this.http.post(`${this.baseUrl}/register`, body);
  }

  login(form) {
    return this.http.post(`${this.baseUrl}/login`, form);
  }

  /*private setSession(authResult) {
    const expiresAt = moment().add(authResult.expiresIn, 'second');

    localStorage.setItem('id_token', authResult.idToken);
    localStorage.setItem('expires_at', JSON.stringify(expiresAt.valueOf()) );
    } */

    logout() {
      localStorage.clear()
      this.store.dispatch(new Deauthenticate())
      this.toastr.success('Logout successful!')
      this.router.navigate(['/'])
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

  isLoggedIn() {
    return !!this.getSession();
  }

  getSession() {
    return localStorage.getItem('session');
  }

  private doLogoutUser() {
    this.loggedUser = null;
  }
}

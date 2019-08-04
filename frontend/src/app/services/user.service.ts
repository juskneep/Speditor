import { Injectable } from '@angular/core';
import { NgModule } from '@angular/compiler/src/core';
import { HttpClient } from '@angular/common/http';
import { NgForm, Validators, FormBuilder, FormGroup, NgModel, FormControl } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { mapTo, tap, catchError } from 'rxjs/operators';
import { Token } from '../guard/Token';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {

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

  baseUrl = 'https://localhost:44345/api/users';


  private readonly JWT_TOKEN = 'JWT_TOKEN';
  private loggedUser: string;

  constructor(private http: HttpClient, private router: Router, private formBuilder: FormBuilder) { }

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
    return this.http.post<any>(`${this.baseUrl}/register`, body);
  }

  login(form): Observable<boolean> {
    return this.http.post<any>(`${this.baseUrl}/login`, form).pipe(
      tap(token => this.doLoginUser(form.Email, token.token))
    );
  }

  /*private setSession(authResult) {
    const expiresAt = moment().add(authResult.expiresIn, 'second');

    localStorage.setItem('id_token', authResult.idToken);
    localStorage.setItem('expires_at', JSON.stringify(expiresAt.valueOf()) );
    } */

  doLoginUser(email: string, token: string): void {
    console.log(token);
    this.loggedUser = email;
    this.storeJwtToken(token);
  }

  logout() {
    return this.http.post<any>(`${this.baseUrl}/logout`, this.getJwtToken()).pipe(
      tap(() => this.doLogoutUser()),
      mapTo(true),
      catchError(error => {
        alert(error.error);
        return of(false);
      }));
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

  isLoggedIn() {
    return !!this.getJwtToken();
  }

  getJwtToken() {
    return localStorage.getItem(this.JWT_TOKEN);
  }

  private doLogoutUser() {
    this.loggedUser = null;
    this.removeTokens();
  }

  private storeJwtToken(jwt: string) {
    localStorage.setItem(this.JWT_TOKEN, jwt);
    const decodedToken = this.decode(jwt);
    localStorage.setItem('userId', decodedToken.payload.userid);
    localStorage.setItem('expireDate', decodedToken.payload.exp);
  }

  private removeTokens() {
    localStorage.removeItem(this.JWT_TOKEN);
  }
}

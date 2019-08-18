import { Component, OnInit } from '@angular/core';
import { Validators, NgForm, FormGroup } from '@angular/forms';
import { UserService } from 'src/app/core/services/user.service';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formModel = {
    Email: '',
    Password: ''
  }

  constructor(
    private userService: UserService,
    private authService: AuthService
  ) { }

  ngOnInit() {
  }

  onSubmit(form: NgForm) {
    this.authService.login(form.value).subscribe(
      (res: any) => {
        console.log(res); 
        localStorage.setItem('authtoken', res.token);
        console.log(localStorage.getItem('authtoken'));
      },
      (err) => {
        console.log(err);
      }
    );
  }

}

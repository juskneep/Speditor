import { Component, OnInit } from '@angular/core';
import { Validators, NgForm, FormGroup } from '@angular/forms';
import { UserService } from 'src/app/core/services/user.service';
import { AuthService } from 'src/app/core/services/auth.service';
import { Toast, ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formModel = {
    Email: '',
    Password: ''
  };

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  onSubmit(form: NgForm) {
    this.authService.login(form.value).subscribe(
      (res: any) => {
        localStorage.setItem('authtoken', res.token);
        this.toastr.success('Login Successful');
        this.router.navigate(['/']);
      },
      (err) => {
        console.log(err);
      }
    );
  }

}

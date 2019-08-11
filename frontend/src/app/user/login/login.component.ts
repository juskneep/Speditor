import { Component, OnInit } from '@angular/core';
import { Validators, NgForm, FormGroup } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';

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

  constructor(private userService: UserService) { }

  ngOnInit() {
  }

  onSubmit(form: NgForm) {
    this.userService.login(form.value).subscribe(
      (res: any) => {
        console.log(`Your session: ${res}`);

      },
      (err) => {
        console.log(err);
      }
    );
  }

}

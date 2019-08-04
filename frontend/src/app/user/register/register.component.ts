import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(
    private formBuilder: FormBuilder,
    public userService: UserService,
    private router: Router
  ) { }

  ngOnInit() {
    this.userService.formModel.reset();
  }

  onSubmit() {
    this.userService.register().subscribe(
      (res: any) => {
        if (res.suceeded) {
          this.userService.formModel.reset();
          this.router.navigate(['/login']);
        } else {
          res.errors.forEach(element => {
            switch (element.code) {
              case 'DuplicateUserName':
                //
                break;
              default:
                //registration failed
                break;
            }
          });
        }
      },
      err => {
        console.log(`${err}`);
      }
    );
  }
}

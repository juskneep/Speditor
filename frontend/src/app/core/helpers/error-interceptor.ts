import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
//import { ToastrService } from 'ngx-toastr';

import { AuthService } from '../services/auth.service';

export class ErrorInterceptor implements HttpInterceptor {
    constructor(
        private authService: AuthService,
       // private toastr: ToastrService
       ) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next
          .handle(req)
          .pipe(catchError((err: HttpErrorResponse) => {
            switch (err.status) {
              case 400:
              case 404:
                //this.spinner.hide()
    
                if (err.error.errors) {
                  const message = Object.keys(err.error.errors)
                    .map(e => err.error.errors[e])
                    .join('\n')
                //  this.toastr.error(message, 'Warning!')
                } else {
                //  this.toastr.error(err.error.message, 'Warning!')
                }
                break;
              case 401:
                //this.spinner.hide()
              //  this.toastr.error(err.error.title, 'Warning!')
            }
            return throwError(err);
          }));
      }

}

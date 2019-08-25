import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

import { AuthService } from '../services/auth.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    private authToken: string = localStorage.getItem('authtoken');

    constructor(private authService: AuthService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // add authorization header with jwt token if available
        if (request.url.endsWith('/api/users/login') || request.url.endsWith('/api/users/register') && request.method === 'GET') {
            request = request.clone({
                setHeaders: {
                    'Content-Type': 'application/json'
                }
            });
        } else {
            request = request.clone({
                setHeaders: {
                    'Authorization': `Bearer ${this.authToken}`,
                    'Content-Type': 'application/json'
                }
            });
        }

        return next.handle(request);
    }
}
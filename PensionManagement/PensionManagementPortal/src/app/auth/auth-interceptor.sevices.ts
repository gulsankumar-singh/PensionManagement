import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse,
  HttpParams,
} from '@angular/common/http';
import { catchError, exhaustMap, Observable, take, throwError } from 'rxjs';
import { AuthService } from './services/auth.service';

@Injectable()
export class AuthInterceptorService implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return this.authService.user.pipe(
      take(1),
      exhaustMap((user) => {
        if (!user) {
          return next.handle(req);
        }
        const modifiedReq = req.clone({
          setHeaders: { Authorization: `Bearer ${user.token}` },
        });
        return next.handle(modifiedReq);
      })
    );
  }
  // intercept(
  //   req: HttpRequest<any>,
  //   next: HttpHandler
  // ): Observable<HttpEvent<any>> {
  //   const token = this.authService.getToken();
  //   if (token) {
  //     // If we have a token, we set it to the header
  //     req = req.clone({
  //       setHeaders: { Authorization: `Bearer ${token}` },
  //     });
  //   }

  //   return next.handle(req).pipe(
  //     catchError((err) => {
  //       if (err instanceof HttpErrorResponse) {
  //         if (err.status === 401) {
  //           // redirect user to the logout page
  //           console.log('');
  //         }
  //       }
  //       return throwError(() => new Error(err));
  //     })
  //   );
  // }
}

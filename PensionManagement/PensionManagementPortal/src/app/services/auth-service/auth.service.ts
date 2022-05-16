import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, catchError, tap, throwError } from 'rxjs';
import { ApiPaths } from 'src/app/shared/enums/api-paths';
import { AuthResponseData } from 'src/app/shared/interfaces/interface';
import { User } from 'src/app/shared/models/user';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class AuthService {
  baseUrl: string = environment.authServiceBaseUrl;
  private tokenExpirationTimer: any;
  user = new BehaviorSubject<User>(null);

  constructor(private http: HttpClient, private router: Router) {}

  login(userName: string, password: string) {
    return this.http
      .post<AuthResponseData>(`${this.baseUrl}/${ApiPaths.Authenticate}`, {
        userName: userName,
        password: password,
      })
      .pipe(
        tap((resData) => {
          this.handleAuthentication(
            userName,
            resData.response.token,
            resData.response.expiration
          );
        })
      );
  }

  logout() {
    this.user.next(null);
    localStorage.removeItem('currentUser');

    if (this.tokenExpirationTimer) {
      clearTimeout(this.tokenExpirationTimer);
    }
    this.tokenExpirationTimer = null;
  }

  getAuthToken() {
    let storageData = localStorage.getItem('currentUser');
    if (storageData) {
      const currentUser: {
        userName: string;
        _token: string;
        _tokenExpirationDate: string;
      } = JSON.parse(storageData);
      if (currentUser) {
        return currentUser._token;
      }
    }
    return null;
  }

  isUserLogged() {
    let storageData = localStorage.getItem('currentUser');
    if (storageData) {
      return true;
    }
    return false;
  }

  autoLogin() {
    const currentUser: {
      userName: string;
      _token: string;
      _tokenExpirationDate: string;
    } = JSON.parse(localStorage.getItem('currentUser'));

    if (!currentUser) {
      return;
    }

    const loadedUser = new User(
      currentUser.userName,
      currentUser._token,
      new Date(currentUser._tokenExpirationDate)
    );

    if (loadedUser.token) {
      this.user.next(loadedUser);
    } else {
      this.router.navigate(['/session-expired']);
    }
  }

  private handleAuthentication(
    userName: string,
    token: string,
    expiration: string
  ) {
    const expirationDate = new Date(expiration);
    const user = new User(userName, token, expirationDate);
    this.user.next(user);
    localStorage.setItem('currentUser', JSON.stringify(user));
  }

  // private handleError(errorRes: HttpErrorResponse) {
  //   let errorMessage = 'An unknown error occured!';

  //   if (errorRes instanceof HttpErrorResponse) {
  //     if (errorRes.error && errorRes.error.message) {
  //       errorMessage = errorRes.error.message;
  //     }
  //   }
  //   return throwError(() => new Error(errorMessage));
  // }
}
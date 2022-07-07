import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Register } from '../register/interface/register';
import { Observable, Subject, tap } from 'rxjs';
import { Login } from '../login/interface/login';
import { LoginResult } from '../login/interface/loginResult';
import { RegistrationResult } from '../register/interface/registrationResult';

@Injectable()
export class HttpService {
  constructor(private http: HttpClient) {}

  //endpoints
  public url = 'http://localhost:5000/api/';
  public registerUrl = 'Accounts/RegisterUser';
  public loginUrl = 'accounts/loginUser';

  private tokenKey: string = 'token';
  private _authStatus = new Subject<boolean>();
  public authStatus = this._authStatus.asObservable();

  public registerUser(
    userToRegister: Register
  ): Observable<RegistrationResult> {
    return this.http.post<RegistrationResult>(
      this.url + this.registerUrl,
      userToRegister
    );
  }

  public loginUser(userToLogin: Login): Observable<LoginResult> {
    return this.http
      .post<LoginResult>(this.url + this.loginUrl, userToLogin)
      .pipe(
        tap((loginResult) => {
          if (loginResult.token) {
            localStorage.setItem(this.tokenKey, loginResult.token);
            this.setAuthStatus(true);
          }
        })
      );
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  isAuthenticated(): boolean {
    return this.getToken() !== null;
  }

  init(): void {
    if (this.isAuthenticated()) {
      this.setAuthStatus(true);
    }
  }

  private setAuthStatus(isAuthenticated: boolean): void {
    this._authStatus.next(isAuthenticated);
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    this.setAuthStatus(false);
  }
}

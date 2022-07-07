import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Validators } from '@angular/forms';
import { HttpService } from '../services/http';
import { Login } from './interface/login';
import { LoginResult } from './interface/loginResult';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  constructor(private http: HttpService, private router: Router) {}

  loginSubmit: Login = {} as Login;
  loginResult?: LoginResult;

  ngOnInit(): void {}

  public loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
    ]),
  });

  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }

  public onSubmit(loginForm: FormGroup) {
    this.loginSubmit = loginForm.value;
    this.http.loginUser(this.loginSubmit).subscribe((res) => {
      this.loginResult = res;
      this.router.navigate(['userRegistrationPage']);
      console.log(this.loginResult);
    });
    loginForm.reset();
  }
}

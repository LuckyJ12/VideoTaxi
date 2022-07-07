import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-registered-page',
  templateUrl: './user-registered-page.component.html',
  styleUrls: ['./user-registered-page.component.scss'],
})
export class UserRegisteredPageComponent implements OnInit {
  constructor(private router: Router) {}

  ngOnInit(): void {}

  public logout() {
    localStorage.clear();
    this.router.navigate(['login']);
  }
}

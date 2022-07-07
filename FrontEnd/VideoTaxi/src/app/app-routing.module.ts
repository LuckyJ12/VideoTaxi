import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/authGuard';
import { LoggedInGuard } from './guards/loggedInGuard';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { UserRegisteredPageComponent } from './user-registered-page/user-registered-page.component';

const routes: Routes = [
  {
    path: 'register',
    component: RegisterComponent,
    canActivate: [LoggedInGuard],
  },
  { path: 'login', component: LoginComponent, canActivate: [LoggedInGuard] },
  {
    path: 'userRegistrationPage',
    component: UserRegisteredPageComponent,
    canActivate: [AuthGuard],
  },
  { path: '**', component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

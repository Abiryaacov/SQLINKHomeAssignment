import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login';
import { authGuard } from './guards/auth-guard';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { 
    path: 'info', 
    loadComponent: () => import('./components/info/info').then(m => m.InfoComponent),
    canActivate: [authGuard]
  }
];
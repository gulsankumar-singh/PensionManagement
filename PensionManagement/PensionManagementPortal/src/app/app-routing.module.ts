import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CalculatePensionComponent } from './components/calculate-pension/calculate-pension.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { PageNotFoundComponent } from './shared/components/page-not-found/page-not-found.component';
import { SessionExpiredComponent } from './shared/components/session-expired/session-expired.component';
import { AuthGuardService } from './shared/guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'calculatepension',
    component: CalculatePensionComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'not-found',
    component: PageNotFoundComponent,
  },
  {
    path: 'session-expired',
    component: SessionExpiredComponent,
  },
  {
    path: '**',
    redirectTo: '/not-found',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

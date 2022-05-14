import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardService } from './auth/auth.guard';
import { LoginComponent } from './auth/components/login.component';
import { CalculatePensionComponent } from './calculate-pension/calculate-pension.component';
import { HomeComponent } from './home/home.component';
import { PensionerListComponent } from './pensioner-list/pensioner-list.component';

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
    path: 'pensionerlist',
    component: PensionerListComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'calculatepension',
    component: CalculatePensionComponent,
    canActivate: [AuthGuardService],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

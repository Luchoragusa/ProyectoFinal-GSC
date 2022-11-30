import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

// Import the components that will be used in the routes
import { HomeComponent } from './modules/home/home.component';
import { LoginComponent } from './modules/login/login.component';
import { AuthGuard } from './modules/shared/auth/auth.guard';
import { NotfoundComponent } from './modules/shared/notfound/notfound.component';


const routes: Routes = [
  {
      path: '',
      redirectTo: 'login',
      pathMatch: 'full'
  },
  {
      path: 'login',
      component: LoginComponent,
  },
  {
      path: 'home',
      component: HomeComponent,
      canActivate: [AuthGuard]
  },
  {  path: '**', 
    pathMatch: 'full', 
    component: NotfoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }


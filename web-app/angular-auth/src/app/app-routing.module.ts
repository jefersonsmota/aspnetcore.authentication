import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SigninComponent } from 'src/app/pages/signin/signin.component';
import { SignupComponent } from './pages/signup/signup.component';


const routes: Routes = [
  {
    path: '', component: SigninComponent, pathMatch: 'full'
  },
  {
    path: 'signup', component: SignupComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

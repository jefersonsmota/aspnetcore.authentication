import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { ApiService } from '../api.service';
import { SignIn } from '../../models/signIn';
import { AppConstants } from "src/app/core/app.constants";
import { HttpHeaders } from '@angular/common/http';
import { SignUp } from '../../models/signUp';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    private url: string = AppConstants.API_RESOURCE;

    constructor(private apiService: ApiService) {
    }

    login(singIn: SignIn) {
        this.apiService.headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });

        return this.apiService.http.post<any>(this.url + 'signin', JSON.stringify(singIn), { headers: this.apiService.headers }).pipe(
            map(user => {
                if (user && user.access_token) {
                    localStorage.setItem('currentUserCredential', JSON.stringify(user));
                }

                return user;
            }));
    }

    register(signUp: SignUp) {
        this.apiService.headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });

        return this.apiService.http.post<any>(this.url + 'signup', JSON.stringify(signUp), { headers: this.apiService.headers }).pipe(
            map(user => {
                if (user && user.access_token) {
                    localStorage.setItem('currentUserCredential', JSON.stringify(user));
                }

                return user;
            }));
    }

}

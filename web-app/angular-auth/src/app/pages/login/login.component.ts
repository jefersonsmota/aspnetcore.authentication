import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { SignIn } from 'src/app/shared/models/signIn';
import { AuthService } from 'src/app/shared/services/auth/auth.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    singInForm: FormGroup;

    constructor(private authService: AuthService) { }

    ngOnInit(): void {
        this.createForm(new SignIn());
    }

    onSubmit() {
        var singIn = new SignIn();
        singIn.email = this.singInForm.value.email;
        singIn.password = this.singInForm.value.password;

        this.authService.login(singIn).pipe(first()).subscribe(data => {
            console.log(data);
        }, err => {
            console.log(err);
        });
    }

    private createForm(singIn: SignIn) {
        this.singInForm = new FormGroup({
            email: new FormControl(singIn.email, Validators.compose([Validators.required])),
            password: new FormControl(singIn.password, Validators.compose([Validators.required, Validators.minLength(8)]))
        });
    }

}

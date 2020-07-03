import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { SignUp } from 'src/app/shared/models/signUp';
import { Phone } from 'src/app/shared/models/phone';
import { AuthService } from 'src/app/shared/services/auth/auth.service';
import { first } from 'rxjs/operators';

@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

    signUpForm: FormGroup;
    phoneForms: FormArray;
    message: string;
    isShowMessage: boolean = false;
    notifications: {key, message}[];
    isSuccess: boolean;

    constructor(private authService: AuthService) { }

    ngOnInit(): void {
        this.phoneForms = new FormArray([this.createPhoneForm(new Phone(), 0)], [Validators.required]);
        this.createForm(new SignUp());
    }

    addPhone():void {
        if (this.phoneForms.length < 3 && this.phoneForms.enabled) {
            this.phoneForms.push(this.createPhoneForm(new Phone(), this.phoneForms.length));
        }
    }

    removePhone(index: number):void {
        if(index >= 1) {
            this.phoneForms.removeAt(index);
        }
    }

    onSubmit() {
        if (this.signUpForm.invalid || this.phoneForms.invalid) {
            return;
        }

        let signUp = this.createRegister();

        this.disableForms();

        this.authService.register(signUp).pipe(first()).subscribe(resp => {
            this.isSuccess = resp.success;
            this.message = resp.message;
            this.isShowMessage = true;
            this.enableForms(); 
        }, err => {
            this.isSuccess = err.error.success;
            this.message = err.error.message;
            this.notifications = err.error.notifications;
            this.isShowMessage = true;

            console.log(err);
            this.enableForms(); 
        });
    }

    private createRegister():SignUp {
        let signUp: SignUp = this.signUpForm.getRawValue();
        let phones: Array<Phone> = new Array<Phone>();

        let i = 0;

        for (let phoneForm of this.phoneForms.controls) {
            if (phoneForm.valid) {
                let phone: Phone = new Phone();
                phone.area_code = parseInt(phoneForm.get('area_code_' + i).value);
                phone.country_code = phoneForm.get('country_code_' + i).value;
                phone.number = parseInt(phoneForm.get('number_' + i).value);

                phones.push(phone);
            }
            i++;
        }

        signUp.phones = phones;

        return signUp;
    }

    private createForm(signUp: SignUp) {
        this.signUpForm = new FormGroup({
            firstName: new FormControl(signUp.firstName, [Validators.required]),
            lastName: new FormControl(signUp.lastName, [Validators.required]),
            email: new FormControl(signUp.email, [Validators.required, Validators.email]),
            password: new FormControl(signUp.password, [Validators.required, Validators.minLength(8)]),
            //phones: new FormArray(this.phoneForms, { validators: this.validatePhoneForms} )//new FormControl('', [this.validatePhoneForms])
        });
    }

    private createPhoneForm(phone: Phone, index: number) {
        const phoneForm = new FormGroup({
            ['number_'+ index]: new FormControl(phone.number, [Validators.required, Validators.minLength(9)]),
            ['area_code_'+ index]: new FormControl(phone.area_code, [Validators.required]),
            ['country_code_'+ index]: new FormControl(phone.country_code, [Validators.required])
        });

        return phoneForm;
    }

    disableForms() {
        this.signUpForm.disable();
        this.phoneForms.disable();
    }

    enableForms() {
        this.signUpForm.enable();
        this.phoneForms.enable();
    }

}

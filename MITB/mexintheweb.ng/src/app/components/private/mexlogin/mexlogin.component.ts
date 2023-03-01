import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { LoginModel } from 'src/app/models/login.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-mexlogin',
  templateUrl: './mexlogin.component.html',
  styleUrls: ['./mexlogin.component.css']
})
export class MexloginComponent implements OnInit {
  public loginResult: string = 'empty';
  emailFormControl = new FormControl('', [Validators.required, Validators.email]);
  matcher = new MyErrorStateMatcher();
  public loginData: LoginModel = new LoginModel('','');

  constructor(public auth: AuthService) {

  }
  ngOnInit(): void {
  }

  public login(username: string, password: string)
  {
    console.log('username: ' + username + ', pw: ' + password);
    this.auth.login(username, password);
    if(this.auth.isUserLoggedIn() === true)
      this.loginResult = 'succeeded';
    else
      this.loginResult = 'failed';
  }
}

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
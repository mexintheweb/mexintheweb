import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { LoginResponseModel } from '../models/login-response.model';
import { LoginModel } from '../models/login.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl: string = '';
  private httpHeaders: HttpHeaders = new HttpHeaders();

  private loginUrl: string = 'api/mexlogin';
  private jsonWebToken: string = '';
  private loginUserName: string = '';
  private isLoggedIn: boolean = false;
  
  public isLoading: boolean = false;
  public userID: number = 0;

  constructor(public http: HttpClient, /*@Inject('BASE_URL') baseUrl: string,*/ public router: Router) { 
    /*this.baseUrl = baseUrl;*/
    this.httpHeaders = new HttpHeaders({'Content-Type': 'application/json'})
  }

  public login(username: string, password: string): void
  {
    this.isLoading = true;
    this.isLoggedIn = false;
    const httpOptions = {
      headers: this.httpHeaders
    }

    let loginModel = new LoginModel(username, password);
    let jsonModel = JSON.stringify(loginModel);
    this.http.post<LoginResponseModel>(/*this.baseUrl + */this.loginUrl, jsonModel, httpOptions).subscribe( res =>
    {
      if(res)
      {
        let token = res.webToken;
        let loginUser = res.username;
        this.userID = res.userID;
  
        if(token && token.length > 1 && loginUser && loginUser.length > 1)
        {
          this.jsonWebToken = token;
          this.loginUserName = loginUser;
          this.isLoggedIn = true;

          this.httpHeaders = this.httpHeaders.set('JsonWebToken', token); //.set('JsonWebToken', token);
          this.httpHeaders = this.httpHeaders.set('LoginUserName', loginUser); //this.httpHeaders.set('LoginUserName', loginUser);

          //this.router.navigate(['/private/menu']);
          this.router.navigate(['/']);
        }

        this.isLoading = false;
      }
      this.isLoading = false;
    },
    error =>
    {
      this.isLoading = false;
    })
  }

  public getWebtoken(): string
  {
    return this.jsonWebToken;
  }

  public getLoginUserName(): string
  {
    return this.loginUserName;
  }

  public isUserLoggedIn(): boolean
  {
    return this.isLoggedIn;
  }

  public logout()
  {
    this.jsonWebToken = '';
    this.loginUserName = '';
    this.isLoggedIn = false;
  }

  public getHeaders(): HttpHeaders
  {
    console.log('httpHeaders: ' + this.httpHeaders.getAll('test'))
    return this.httpHeaders;
  }
}

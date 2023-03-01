import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-mexlogin',
  templateUrl: './mexlogin.component.html',
  styleUrls: ['./mexlogin.component.css']
})
export class MexloginComponent implements OnInit {
  public loginResult: string = 'empty';
  
  constructor(public auth: AuthService) {

  }
  ngOnInit(): void {
  }

  public login(username: string, password: string)
  {
    this.auth.login(username, password);
    if(this.auth.isUserLoggedIn() === true)
      this.loginResult = 'succeeded';
    else
      this.loginResult = 'failed';
  }
}

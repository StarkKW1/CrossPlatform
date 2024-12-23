import {Component, inject} from '@angular/core';
import {MatButton} from '@angular/material/button';
import {FormsModule} from '@angular/forms';
import {MatCard, MatCardContent} from '@angular/material/card';
import {MatFormField} from '@angular/material/form-field';
import {MatInput} from '@angular/material/input';
import {AuthService} from '../../services/AuthService';
import {CommonModule} from '@angular/common';
import {CUSTOM_ELEMENTS_SCHEMA} from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login-page',
  imports: [
    MatButton,
    FormsModule,
    MatCard,
    MatCardContent,
    MatFormField,
    MatInput,
    CommonModule,
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.css'
})
export class LoginPageComponent {
  public msg: string = "";
  router = inject(Router);

  constructor(private _auth:AuthService ) { }

  ngOnInit(): void {
    this.ResetMsg();
  }

  public ResetMsg():void{
    this.msg = "Log in to continue";
  }
  public Login(info: { login: string, password: string }) {
    this._auth.login(JSON.parse(JSON.stringify(info))).subscribe(
      status=>
      {
        if (status==200)
        {
          this.msg = "Success";
          this.router.navigate(['']);
        }
        else if (status==401)
          this.msg = "Wrong login/password";
        else
          this.msg = `Something went wrong (${status})`;
      });
  }
}

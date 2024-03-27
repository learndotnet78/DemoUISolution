import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../../environments/environment';
import { Loginmodel } from './loginmodel.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginModel : Loginmodel = {
    username : "",
    password : ""
  };

  invalidLogin: boolean = false;
  url :string = environment.apiBaseUrl + 'api/Auth/login';
  constructor(private router : Router, private http : HttpClient, private toastr :ToastrService ){}

  login(form : NgForm){
    this.loginModel = {
      username : form.value.username,
      password : form.value.password
    }
    console.log(this.loginModel);
    this.http.post(this.url,this.loginModel)
    .subscribe({
      next : res => {
        const token = (<any>res).token;
        localStorage.setItem('jwt',token);
        this.invalidLogin = false;
        this.toastr.success("Login successful","Login Form");
        this.router.navigate(["home"]);
      },
      error : err => {
        console.log(err)
        this.invalidLogin = true;
        this.toastr.success("Login failed","Login Form");}
    })
  }

}

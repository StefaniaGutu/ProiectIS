import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from "../../services/auth.service";
import { HttpResponse } from "@angular/common/http";
import { Credentials } from "../../models/credentials";
import { HOME } from "@angular/cdk/keycodes";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public errorMessage: string | boolean = false;

  public myForm!: FormGroup;

  constructor(
    private router: Router,
    private fromBuilder: FormBuilder,
    private authService: AuthService) { }

  ngOnInit(): void {
    this.myForm = this.fromBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  doLogin() {
    this.errorMessage = false;
    if (this.myForm.controls['email'].value && this.myForm.controls['password'].value) {
      this.authService.login({
        email: this.myForm.controls['email'].value,
        password: this.myForm.controls['password'].value
      }).subscribe((loginToken: HttpResponse<string>) => {
        if (typeof loginToken.body === "string") {
          window.localStorage.setItem('token', loginToken.body);
          this.router.navigate(['/home']);
        }
      }, error => {
        this.errorMessage = error.error;
      })
    }
  }

}

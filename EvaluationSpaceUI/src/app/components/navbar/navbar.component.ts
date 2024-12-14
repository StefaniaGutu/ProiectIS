import { Component, OnInit } from '@angular/core';
import { getToken } from "../../util/auth.util";
import { Router } from "@angular/router";
import { AuthService } from "../../services/auth.service";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor(private route: Router, private authService: AuthService) {
  }

  ngOnInit(): void {
  }

  checkLogged(): boolean {
    return this.authService.isLogged(getToken());
  }

  isStudent(): boolean | null {
    let token: string | null = getToken();
    if (token != null) {
      let jwtData = token.split('.')[1]
      let decodedJwtJsonData = window.atob(jwtData)
      let decodedJwtData = JSON.parse(decodedJwtJsonData).role
      return decodedJwtData === 'Student';
    }
    return null;
  }

  logout() {
    this.authService.logout();
  }
}

import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { isStudent } from '../util/auth.util';

@Injectable({
  providedIn: 'root'
})
export class AuthStudentGuard implements CanActivate {

  constructor(private router: Router) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (isStudent()) {
      return true;
    }
    else {
      this.router.navigate(['/home']);
      return false;
    }
  }

}

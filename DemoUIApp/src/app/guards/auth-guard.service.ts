import { Injectable,inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService   {

  constructor(private router : Router) { }

  
}

export const canActivateGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const token = localStorage.getItem("jwt");
  if (token != null) {
    return true;
  } else {
    inject(Router).navigate(['/login']); // Use inject(Router) to get the Router service
    return false;
  }
};

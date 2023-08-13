import { HttpClient } from '@angular/common/http';
import { Injectable, ViewChild } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Register } from 'src/app/models/register';
import { CustomControllerService } from './custom-controller-service';
import { UtilitiesService } from './utilitites-service';
import { HttpHeaders } from '@angular/common/http';

const defaultPath = '/';
const defaultUser = {
  email: 'sandra@example.com'
};

@Injectable()
export class AuthService {
  private _user = defaultUser;
  get loggedIn(): boolean {
    if (localStorage.getItem('Email') != null){
      return true;
    }else{
      return false;
    }
  }

  registerUser: Register = new Register();
  registerResponse: Boolean = false;

  private _lastAuthenticatedPath: string = defaultPath;
  set lastAuthenticatedPath(value: string) {
    this._lastAuthenticatedPath = value;
  }

  constructor(private router: Router,
    protected http: HttpClient,
    private utilities: UtilitiesService,
    private customControllerService: CustomControllerService) { }

   logIn(email: string, password: string) {
      var data = "username=" + email + "&password=" + password + "&grant_type=password";
      var reqHeader = new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded'});
      return this.http.post(this.utilities.baseAPIUrl + 'token', data, { headers: reqHeader });
  

    // try {
    //   // Send request
    //   console.log(email, password);
    //   this._user = { ...defaultUser, email };
    //   this.router.navigate([this._lastAuthenticatedPath]);

    //   return {
    //     isOk: true,
    //     data: this._user
    //   };
    // }
    // catch {
    //   return {
    //     isOk: false,
    //     message: "Authentication failed"
    //   };
    // }
  }

  async getUser() {
    try {
      // Send request

      this._user.email = localStorage.getItem('Email');

      return {
        isOk: true,
        data: this._user
      };
    }
    catch {
      return {
        isOk: false
      };
    }
  }

  async createAccount(email, password) {
    try {
        this.registerUser.Email= email;
        this.registerUser.ConfirmPassword = email;
        this.registerUser.Password = password;
      
        this.customControllerService.utilitiesPost("Account/Register", this.registerUser).subscribe(
          data => {
            if(data.Succeeded == true){
                this.registerResponse = true;
              }else{
                this.registerResponse = false;
              }
          },
          error => {
            this.registerResponse = false;
          }
       )

      this.router.navigate(['/create-account']);
      return {
        isOk:  this.registerResponse
      };
    }
    catch {
      return {
        isOk: false,
        message: "Failed to create account"
      };
    }
  }

  async changePassword(email: string, recoveryCode: string) {
    try {
      // Send request
      console.log(email, recoveryCode);

      return {
        isOk: true
      };
    }
    catch {
      return {
        isOk: false,
        message: "Failed to change password"
      }
    };
  }

  async resetPassword(email: string) {
    try {
      // Send request
      return {
        isOk: true
      };
    }
    catch {
      return {
        isOk: false,
        message: "Failed to reset password"
      };
    }
  }

  async logOut() {
    localStorage.removeItem('Email');
    localStorage.removeItem('userName');
    this.router.navigate(['/login-form']);
  }
}

@Injectable()
export class AuthGuardService implements CanActivate {
  constructor(private router: Router, private authService: AuthService) { }

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const isLoggedIn = this.authService.loggedIn;
    const isAuthForm = [
      'login-form',
      'reset-password',
      'create-account',
      'change-password/:recoveryCode',
      'reset-password-with-code',
    ].includes(route.routeConfig.path);

    if (isLoggedIn && isAuthForm) {
      this.authService.lastAuthenticatedPath = defaultPath;
      this.router.navigate([defaultPath]);
      return false;
    }

    if (!isLoggedIn && !isAuthForm) {
      this.router.navigate(['/login-form']);
    }

    if (isLoggedIn) {
      this.authService.lastAuthenticatedPath = route.routeConfig.path;
    }

    return isLoggedIn || isAuthForm;
  }
}

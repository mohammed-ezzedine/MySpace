import {Inject, Injectable, PLATFORM_ID} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {FormGroup} from "@angular/forms";
import jwt_decode from "jwt-decode";
import {DateService} from "./date.service";
import { isPlatformBrowser } from '@angular/common';


@Injectable({
  providedIn: "root"
})
export class AuthService {

  private static TOKEN = "MYSPACE_ADMIN_TOKEN"
  private static readonly ENDPOINT = environment.authUrl + '/identity'
  private static readonly HTTP_OPTIONS = {
    headers: new HttpHeaders({
      "Content-Type": "application/json",
    }),
  }


  constructor(private http: HttpClient,
              @Inject(PLATFORM_ID) private platformId: Object) {
  }


  login(form: FormGroup) {
    let url = AuthService.ENDPOINT + "/login";
    let body = JSON.stringify(form.value);
    return this.http.post<any>(url, body, AuthService.HTTP_OPTIONS)
  }

  writeToken(token: string): void {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.setItem(AuthService.TOKEN, token);
    }
  }

  getToken(): string | null {
    let token = this.readToken();

    if (AuthService.isTokenValid(token)) {
      return token;
    } else {
      this.readToken()
    }

    return null;
  }

  isAuthenticated(): boolean {
    let token = this.readToken();
    return AuthService.isTokenValid(token)
  }

  logout() {
    this.removeToken();
  }

  private removeToken() {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem(AuthService.TOKEN);
    }
  }

  private static isTokenValid(token: string | null) : boolean {
    return token != null && AuthService.getTokenExpiryDate(token).valueOf() > Date.now().valueOf();
  }

  private readToken(): string | null {
    return isPlatformBrowser(this.platformId)
      ? localStorage.getItem(AuthService.TOKEN)
      : null;
  }

  private static getTokenExpiryDate(token: string): Date {
    let decodedToken : any = jwt_decode(token);
    return DateService.getDate(decodedToken.exp);
  }
}

import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {catchError} from "rxjs/operators";
import {throwError} from "rxjs";
import {FormGroup} from "@angular/forms";
import jwt_decode from "jwt-decode";
import {DateService} from "./date.service";
import {Router} from "@angular/router";

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


  constructor(private http: HttpClient) {
  }


  login(form: FormGroup) {
    let url = AuthService.ENDPOINT + "/login";
    let body = JSON.stringify(form.value);
    return this.http.post<any>(url, body, AuthService.HTTP_OPTIONS)
  }

  writeToken(token: string): void {
    localStorage.setItem(AuthService.TOKEN, token);
  }

  getToken(): string | null {
    let token = AuthService.readToken();

    if (AuthService.isTokenValid(token)) {
      return token;
    } else {
      AuthService.readToken()
    }

    return null;
  }

  isAuthenticated(): boolean {
    let token = AuthService.readToken();
    return AuthService.isTokenValid(token)
  }

  logout() {
    AuthService.removeToken();
  }

  private static removeToken() {
    localStorage.removeItem(AuthService.TOKEN);
  }

  private static isTokenValid(token: string | null) : boolean {
    return token != null && AuthService.getTokenExpiryDate(token).valueOf() > Date.now().valueOf();
  }

  private static readToken(): string | null {
    return localStorage.getItem(AuthService.TOKEN);
  }

  private static getTokenExpiryDate(token: string): Date {
    let decodedToken : any = jwt_decode(token);
    return DateService.getDate(decodedToken.exp);
  }
}

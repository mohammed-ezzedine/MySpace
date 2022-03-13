import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {catchError} from "rxjs/operators";
import {throwError} from "rxjs";
import {FormGroup} from "@angular/forms";
import jwt_decode from "jwt-decode";

@Injectable({
  providedIn: "root"
})
export class AuthService {

  private static LOGIN_ENDPOINT = '/identity/login';

  constructor(private http: HttpClient) {
  }


  login(form: FormGroup) {
    let url = environment.apiUrl + AuthService.LOGIN_ENDPOINT;

    let body = JSON.stringify(form.value);

    let options = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*"
      })
    }

    this.http.post<string>(url, body, options).subscribe({
      next: (token) => {
        let decodedToken = jwt_decode(token);
        console.log(decodedToken)
      },
      error: (err) => {
        console.error(err)
      },
      complete: () => {
      }
    })

  //   var myHeaders = new Headers();
  //   myHeaders.append("Content-Type", "application/json");
  //   myHeaders.append("Access-Control-Allow-Origin", "*");
  //
  //   var raw = JSON.stringify({
  //     "username": "admin",
  //     "password": "Admin123!!"
  //   });
  //
  //   var requestOptions : RequestInit = {
  //     method: 'POST',
  //     headers: myHeaders,
  //     body: raw,
  //     redirect: 'follow'
  //   };
  //
  //   fetch("http://localhost:8000/api/identity/login", requestOptions)
  //     .then(response => response.text())
  //     .then(result => console.log(result))
  //     .catch(error => console.log('error', error));
  }
}

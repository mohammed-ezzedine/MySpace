import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {ProjectModel} from "../models/project.model";
import {environment} from "../../environments/environment";
import {Injectable} from "@angular/core";
import {AuthService} from "./auth.service";

@Injectable({
  providedIn: "root"
})
export class ProjectService {
  constructor(private http: HttpClient,
              private authService: AuthService) {
  }

  private static readonly ENDPOINT = environment.apiUrl + "/project";

  getProjects() : Observable<ProjectModel[]> {
    return this.http.get<ProjectModel[]>(ProjectService.ENDPOINT);
  }

  getProject(id: string) : Observable<ProjectModel> {
    return this.http.get<ProjectModel>(ProjectService.ENDPOINT + '/' + id);
  }

  addProject(form: any) : Observable<ProjectModel> {
    return this.http.post<ProjectModel>(ProjectService.ENDPOINT, JSON.stringify(form), this.getHttpOptions());
  }

  updateProject(id: string, form: any) : Observable<ProjectModel> {
    return this.http.put<ProjectModel>(ProjectService.ENDPOINT + "/" + id, JSON.stringify(form), this.getHttpOptions());
  }

  deleteProject(id: string) : Observable<any> {
    return this.http.delete<any>(ProjectService.ENDPOINT + "/" + id, this.getHttpOptions());
  }

  private getHttpOptions() {
    let token = this.authService.getToken();
    if (token == null) {
      throw new Error("Unauthorized.")
    }

    return {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Authorization": "Bearer " + token
      })
    }
  }
}

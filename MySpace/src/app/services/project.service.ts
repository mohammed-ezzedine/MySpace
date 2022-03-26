import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {ProjectModel} from "../models/project.model";
import {environment} from "../../environments/environment";
import {Injectable} from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class ProjectService {
  constructor(private http: HttpClient) {
  }

  private static readonly ENDPOINT = environment.apiUrl + "/project";
  private static readonly HTTP_OPTIONS = {
    headers: new HttpHeaders({
      "Content-Type": "application/json"
    })
  }
  getProjects() : Observable<ProjectModel[]> {
    return this.http.get<ProjectModel[]>(ProjectService.ENDPOINT);
  }

  getProject(id: string) : Observable<ProjectModel> {
    return this.http.get<ProjectModel>(ProjectService.ENDPOINT + '/' + id);
  }

  addProject(form: any) : Observable<ProjectModel> {
    return this.http.post<ProjectModel>(ProjectService.ENDPOINT, JSON.stringify(form), ProjectService.HTTP_OPTIONS);
  }

  updateProject(id: string, form: any) : Observable<ProjectModel> {
    return this.http.put<ProjectModel>(ProjectService.ENDPOINT + "/" + id, JSON.stringify(form), ProjectService.HTTP_OPTIONS);
  }

  deleteProject(id: string) : Observable<any> {
    return this.http.delete<any>(ProjectService.ENDPOINT + "/" + id);
  }
}

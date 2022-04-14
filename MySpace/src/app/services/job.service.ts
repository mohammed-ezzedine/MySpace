import {JobModel} from "../models/job.model";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";
import {FormGroup} from "@angular/forms";
import {AuthService} from "./auth.service";

@Injectable({
  providedIn: 'root',
})
export class JobService {
  constructor(private http: HttpClient,
              private authService: AuthService) {
  }

  private static readonly ENDPOINT = environment.apiUrl + "/job"

  getJobs() : Observable<JobModel[]> {
    return this.http.get<JobModel[]>(JobService.ENDPOINT);
  }

  getJob(id: string) : Observable<JobModel> {
    return this.http.get<JobModel>(JobService.ENDPOINT + "/" + id);
  }

  getCurrentJob() : Observable<JobModel> {
    return this.http.get<JobModel>(JobService.ENDPOINT + "/current");
  }

  addJob(form: FormGroup) : Observable<JobModel> {
    return this.http.post<JobModel>(JobService.ENDPOINT, JSON.stringify(form.value), this.getHttpOptions());
  }

  updateJob(id: string, form: FormGroup) : Observable<JobModel> {
    return this.http.put<JobModel>(JobService.ENDPOINT + "/" + id, JSON.stringify(form.value), this.getHttpOptions());
  }

  deleteJob(id: string) : Observable<any> {
    return this.http.delete(JobService.ENDPOINT + "/" + id, this.getHttpOptions());
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

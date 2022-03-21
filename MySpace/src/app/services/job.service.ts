import {JobModel} from "../models/job.model";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";
import {FormGroup} from "@angular/forms";

@Injectable({
  providedIn: 'root',
})
export class JobService {
  constructor(private http: HttpClient) {
  }

  private static readonly ENDPOINT = environment.apiUrl + "/job"

  private static HTTP_OPTIONS = {
    headers: new HttpHeaders({
      "Content-Type": "application/json"
    })
  }

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
    return this.http.post<JobModel>(JobService.ENDPOINT, JSON.stringify(form.value), JobService.HTTP_OPTIONS);
  }

  updateJob(id: string, form: FormGroup) : Observable<JobModel> {
    return this.http.put<JobModel>(JobService.ENDPOINT + "/" + id, JSON.stringify(form.value), JobService.HTTP_OPTIONS);
  }

  deleteJob(id: string) : Observable<any> {
    return this.http.delete(JobService.ENDPOINT + "/" + id);
  }
}

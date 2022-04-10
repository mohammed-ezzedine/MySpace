import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {JobModel} from "../../models/job.model";
import {JobService} from "../../services/job.service";
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-job-thumbnail',
  templateUrl: './job-thumbnail.component.html',
  styleUrls: ['./job-thumbnail.component.scss']
})
export class JobThumbnailComponent implements OnInit {

  constructor(private jobService: JobService,
              private authService: AuthService) { }

  @Input("job")
  job!: JobModel;

  @Output()
  jobDeletion = new EventEmitter<any>();

  ngOnInit(): void {
  }

  getDateString(date: any) : string {
    return new Date(date).toDateString();
  }

  deleteJob() {
    this.jobService.deleteJob(this.job.id).subscribe({
      next: _ => this.jobDeletion.emit({ id: this.job.id, success: true}),
      error: err => this.jobDeletion.emit({ id: this.job.id, success: false, error: err}),
    })
  }

  isAuthenticated() {
    return this.authService.isAuthenticated()
  }
}

import { Component, OnInit } from '@angular/core';
import {JobModel} from "../../models/job.model";
import {JobService} from "../../services/job.service";
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.scss']
})
export class JobsComponent implements OnInit {

  constructor(private jobService: JobService,
              private authService: AuthService) { }

  successMessage: string | null = null;
  errorMessage: string | null = null;

  jobs: JobModel[] | undefined;

  ngOnInit(): void {
    this.getJobs();
  }

  jobDeletion($event: any) {
    if ($event.success) {
      this.successMessage = "Job deleted successfully!"
      this.jobs = this.jobs?.filter(j => j.id != $event.id);
    } else {
      this.errorMessage = `Failed to delete job of ID ${$event.id}: ${$event.error}`;
    }
  }

  isAuthenticated() {
    return this.authService.isAuthenticated()
  }

  private getJobs() {
    this.jobService.getJobs().subscribe({
      next: value => this.jobs = value,
      error: err => this.errorMessage = err.message
    })
  }
}

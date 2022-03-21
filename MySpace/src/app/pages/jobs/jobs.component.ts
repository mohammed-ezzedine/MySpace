import { Component, OnInit } from '@angular/core';
import {JobModel} from "../../models/job.model";
import {JobService} from "../../services/job.service";

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.scss']
})
export class JobsComponent implements OnInit {

  constructor(private jobService: JobService) { }

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

  private getJobs() {
    this.jobService.getJobs().subscribe({
      next: value => this.jobs = value,
      error: err => this.errorMessage = err.message
    })
  }
}

import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {JobModel} from "../../../models/job.model";
import {ActivatedRoute} from "@angular/router";
import {JobService} from "../../../services/job.service";

@Component({
  selector: 'app-edit-job',
  templateUrl: './edit-job.component.html',
  styleUrls: ['./edit-job.component.scss']
})
export class EditJobComponent implements OnInit {

  constructor(private fb: FormBuilder,
              private route: ActivatedRoute,
              private jobService: JobService) { }

  id: string | undefined;
  job: JobModel | undefined;

  errorMessage: string | undefined;
  successMessage: string | undefined;

  jobForm: FormGroup | undefined;

  ngOnInit(): void {
    this.getJob();
  }

  private getJob() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.jobService.getJob(this.id!).subscribe({
        next: value => {
          this.job = value;
          this.initializeForm();
        },
        error: err => this.errorMessage = err.message
      })
    })
  }

  private initializeForm() {
    this.jobForm = this.fb.group({
      employer: [this.job!.employer, [Validators.required]],
      employerUrl: [this.job!.employerUrl, [Validators.required]],
      employerImageUrl: [this.job!.employerImageUrl, [Validators.required]],
      position: [this.job!.position, [Validators.required]],
      startDate: [this.job!.startDate, [Validators.required]],
      endDate: [this.job!.endDate, []],
      active: [this.job!.active, []]
    })
  }

  submitForm() {
    if (this.jobForm?.valid) {
      this.jobService.updateJob(this.id!, this.jobForm).subscribe({
        next: _ => this.successMessage = "Job Updated Successfully",
        error: err => this.errorMessage = err.message
      })
    } else {
      Object.values(this.jobForm!.controls).forEach(control => {
        if (control.invalid) {
          console.log(control)
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

}

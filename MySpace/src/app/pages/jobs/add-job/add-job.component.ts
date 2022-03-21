import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {JobService} from "../../../services/job.service";

@Component({
  selector: 'app-add-job',
  templateUrl: './add-job.component.html',
  styleUrls: ['./add-job.component.scss']
})
export class AddJobComponent implements OnInit {

  constructor(private fb: FormBuilder,
              private jobService: JobService) { }

  successMessage: string | null = null;
  errorMessage: string | null = null;
  jobForm!: FormGroup;

  ngOnInit(): void {
    this.initializeForm();
  }

  submitForm() {
    if (this.jobForm.valid) {
      this.jobService.addJob(this.jobForm).subscribe({
        next: value => this.successMessage = "Job Added Successfully",
        error: err => this.errorMessage = err.message
      })
    } else {
      Object.values(this.jobForm.controls).forEach(control => {
        if (control.invalid) {
          console.log(control)
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }

  private initializeForm() {
    this.jobForm = this.fb.group({
      employer: [null, [Validators.required]],
      employerUrl: [null, [Validators.required]],
      employerImageUrl: [null, [Validators.required]],
      position: [null, [Validators.required]],
      startDate: [null, [Validators.required]],
      endDate: [null, []],
      active: [false, [Validators.required]],
    })
  }
}

import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ProjectService} from "../../../services/project.service";
import {ArticleUtils} from "../../../utils/article.utils";

@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.scss']
})
export class AddProjectComponent implements OnInit {

  constructor(private fb: FormBuilder,
              private projectService: ProjectService) { }

  projectForm!: FormGroup;
  contentControls: Array<{ id: number; controlInstance: string }> = [];

  successMessage: string | undefined;
  errorMessage: string | undefined;

  ngOnInit(): void {
    this.initializeForm();
  }

  private initializeForm() {
    this.projectForm = this.fb.group({
      title: [null, [Validators.required]],
      description: [null, [Validators.required]],
      createdDate: [null, [Validators.required]],
      url: [null, [Validators.pattern("https?:\\/\\/(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b([-a-zA-Z0-9()@:%_\\+.~#*?&//=]*)")]],
    })
  }

  submitForm(): void {
    if (this.projectForm.valid) {
      let form = {
        title: this.projectForm.controls['title'].value,
        description: this.projectForm.controls['description'].value,
        url: this.projectForm.controls['url'].value,
        createdDate: this.projectForm.controls['createdDate'].value,
        content: ArticleUtils.getArticleContent(this.contentControls, this.projectForm),
      };
      this.projectService.addProject(form).subscribe({
        next: _ => this.successMessage = "Project added successfully.",
        error: err => this.errorMessage = "Failed to add project: " + err.message
      })
    } else {
      Object.values(this.projectForm.controls).forEach(control => {
        if (control.invalid) {
          console.log(control)
          control.markAsDirty();
          control.updateValueAndValidity({onlySelf: true});
        }
      });
    }
  }
}

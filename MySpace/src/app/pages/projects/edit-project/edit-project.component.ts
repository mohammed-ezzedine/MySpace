import { Component, OnInit } from '@angular/core';
import {ProjectModel} from "../../../models/project.model";
import {ActivatedRoute} from "@angular/router";
import {ProjectService} from "../../../services/project.service";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ArticleUtils} from "../../../utils/article.utils";

@Component({
  selector: 'app-edit-project',
  templateUrl: './edit-project.component.html',
  styleUrls: ['./edit-project.component.scss']
})
export class EditProjectComponent implements OnInit {

  constructor(private fb: FormBuilder,
              private route: ActivatedRoute,
              private projectService: ProjectService) { }

  id: string | undefined;
  project: ProjectModel | undefined;
  successMessage: string | undefined;
  errorMessage: string | null = null;

  projectForm!: FormGroup;
  contentControls: Array<{ id: number; controlInstance: string }> = [];

  ngOnInit(): void {
    this.initializeProjectFromRoute();
  }

  private initializeProjectFromRoute() {
    this.route.params.subscribe({
      next: params => {
        this.id = params['id'];
        this.getProject();
      }
    })
  }

  private getProject() {
    this.projectService.getProject(this.id!).subscribe({
      next: value => {
        this.project = value;
        this.initializeForm();
        this.initializeContentControls();
      },
      error: err => this.errorMessage = err.message
    })
  }

  private initializeForm() {
    this.projectForm = this.fb.group({
      title: [this.project?.title, [Validators.required]],
      description: [this.project?.description, [Validators.required]],
      createdDate: [this.project?.createdDate, [Validators.required]],
      url: [this.project?.url, [Validators.pattern("https?:\\/\\/(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b([-a-zA-Z0-9()@:%_\\+.~#*?&//=]*)")]],
    })
  }

  private initializeContentControls() {
    if (!this.project) {
      return;
    }

    ArticleUtils.initializeContentControls(this.projectForm!, this.contentControls, this.project.content);
  }

  submitForm(): void {
    if (this.projectForm.valid) {
      let form = {
        title: this.projectForm.controls['title'].value,
        description: this.projectForm.controls['description'].value,
        url: this.projectForm.controls['url'].value,
        content: ArticleUtils.getArticleContent(this.contentControls, this.projectForm),
      };
      this.projectService.updateProject(this.id!, form).subscribe({
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

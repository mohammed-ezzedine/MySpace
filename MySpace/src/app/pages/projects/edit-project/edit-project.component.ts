import { Component, OnInit } from '@angular/core';
import {ProjectModel} from "../../../models/project.model";
import {ActivatedRoute} from "@angular/router";
import {ProjectService} from "../../../services/project.service";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ArticleEstimatedReadingTimeCalculator} from "../../../utils/articleEstimatedReadingTimeCalculator";
import {PageContentSection} from "../../../components/page-editor/page-content-section";

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
  controls : PageContentSection[] = [];

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

    this.initializePageContent(this.project.content);
    // ArticleUtils.initializeContentControls(this.projectForm!, this.contentControls, this.project.content);
  }

  private initializePageContent(content: any[]) {
    for (let section of content) {
      if (section.type == 'paragraph') {
        this.controls.push({type: 'paragraph', content: new FormControl(section.content, Validators.required)})
      } else if (section.type == 'code') {
        this.controls.push({
          type: 'code',
          metadata: section.language,
          content: new FormControl(section.content, Validators.required)
        })
      } else {
        this.controls.push({type: 'image', content: new FormControl(section.imageUrl, Validators.required)})
      }
    }
  }

  submitForm(): void {
    if (this.projectForm.valid && this.areSectionsValid()) {
      let form = {
        title: this.projectForm.controls['title'].value,
        description: this.projectForm.controls['description'].value,
        url: this.projectForm.controls['url'].value,
        createdDate: this.projectForm.controls['createdDate'].value,
        // content: ArticleUtils.getArticleContent(this.contentControls, this.projectForm),
        content: this.getContentSections(),
      };
      this.projectService.updateProject(this.id!, form).subscribe({
        next: _ => this.successMessage = "Project updated successfully.",
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

  private areSectionsValid() {
    return this.controls.map(c => c.content.valid).reduce((p, c) => p && c);
  }

  private getContentSections() {
    return this.controls.map(c => {
      switch (c.type) {
        case 'code': return { type: 'code', language: c.metadata, content: c.content.value };
        case 'image': return { type: 'image', imageUrl: c.content.value };
        case 'paragraph': return { type: 'paragraph', content: c.content.value };
        default: return null;
      }
    })
  }

  updateControls(updatedControls: PageContentSection[]) {
    this.controls = updatedControls
  }
}

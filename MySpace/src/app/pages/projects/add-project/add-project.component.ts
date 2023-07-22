import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ProjectService} from "../../../services/project.service";
import {ArticleEstimatedReadingTimeCalculator} from "../../../utils/articleEstimatedReadingTimeCalculator";
import {PageContentSection} from "../../../components/page-editor/page-content-section";

@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.scss']
})
export class AddProjectComponent implements OnInit {

  constructor(private fb: FormBuilder,
              private projectService: ProjectService) { }

  projectForm!: FormGroup;
  controls: PageContentSection[] = [];

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
    if (this.projectForm.valid && this.areSectionsValid()) {
      let form = {
        title: this.projectForm.controls['title'].value,
        description: this.projectForm.controls['description'].value,
        url: this.projectForm.controls['url'].value,
        createdDate: this.projectForm.controls['createdDate'].value,
        content: this.getContentSections(),
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

  private areSectionsValid() {
    return this.controls.map(c => c.content.valid).reduce((p, c) => p && c);
  }

  updateControls(updatedContent: PageContentSection[]) {
    this.controls = updatedContent;
  }
}

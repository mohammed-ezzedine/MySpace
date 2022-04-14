import { Component, OnInit } from '@angular/core';
import {ProjectModel} from "../../models/project.model";
import {ProjectService} from "../../services/project.service";
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss']
})
export class ProjectsComponent implements OnInit {

  constructor(private projectService: ProjectService,
              private authService: AuthService) { }

  projects: ProjectModel[] | undefined;
  errorMessage: string | null = null;
  successMessage: string | null = null;

  ngOnInit(): void {
    this.getProjects();
  }

  private getProjects() {
    this.projectService.getProjects().subscribe({
      next: value => {
        this.projects = value;
      },
      error: err => this.errorMessage = err
    });
  }

  projectDeletion($event: any) {
    if ($event.success) {
      this.successMessage = "Project deleted successfully!"
      this.projects = this.projects?.filter(p => p.id != $event.id);
    } else {
      this.errorMessage = `Failed to delete project of ID ${$event.id}: ${$event.error}`;
    }
  }

  isAuthenticated() {
    return this.authService.isAuthenticated()
  }
}

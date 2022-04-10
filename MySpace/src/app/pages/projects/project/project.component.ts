import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {ProjectService} from "../../../services/project.service";
import {ProjectModel} from "../../../models/project.model";
import {DateUtils} from "../../../utils/date.utils";
import {AuthService} from "../../../services/auth.service";

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit {

  constructor(private route: ActivatedRoute,
              private router: Router,
              private projectService: ProjectService,
              private authService: AuthService) { }

  id: string | undefined;
  project: ProjectModel | undefined;
  errorMessage: string | null = null;

  ngOnInit(): void {
    this.initializeProjectFromRoute();
  }

  getDateString(date: Date) : string {
    return DateUtils.getDateString(date);
  }

  isAuthenticated() {
    return this.authService.isAuthenticated()
  }

  deleteProject() {
    this.projectService.deleteProject(this.id!).subscribe({
      next: _ => this.router.navigateByUrl("/projects"),
      error: err => this.errorMessage = err.message,
    });
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
      next: value => this.project = value,
      error: err => this.errorMessage = err.message
    })
  }
}

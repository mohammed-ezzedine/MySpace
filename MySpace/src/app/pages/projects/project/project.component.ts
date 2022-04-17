import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {ProjectService} from "../../../services/project.service";
import {ProjectModel} from "../../../models/project.model";
import {DateUtils} from "../../../utils/date.utils";
import {AuthService} from "../../../services/auth.service";
import {SeoService} from "../../../services/seo.service";
import {SeoShareDataModel} from "../../../models/seo-share-data.model";

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.scss']
})
export class ProjectComponent implements OnInit {

  constructor(private route: ActivatedRoute,
              private router: Router,
              private seoService: SeoService,
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
      next: project => {
        this.project = project;
        this.setSeoShareData();
      },
      error: err => {
        if (err.status == 404) {
          this.router.navigateByUrl('/not-found');
        } else {
          this.errorMessage = err.message
        }
      }
    })
  }

  private setSeoShareData() {
    this.seoService.setData(new SeoShareDataModel(this.project!.title, this.project!.description));
  }
}

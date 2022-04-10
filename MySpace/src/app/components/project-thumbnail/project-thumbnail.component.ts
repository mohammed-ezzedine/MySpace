import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ProjectModel} from "../../models/project.model";
import {DateUtils} from "../../utils/date.utils";
import {ProjectService} from "../../services/project.service";
import {AuthService} from "../../services/auth.service";

@Component({
  selector: 'app-project-thumbnail',
  templateUrl: './project-thumbnail.component.html',
  styleUrls: ['./project-thumbnail.component.scss']
})
export class ProjectThumbnailComponent implements OnInit {

  constructor(private projectService: ProjectService,
              private authService: AuthService) { }

  @Input("project")
  project!: ProjectModel;

  @Output()
  projectDeletion = new EventEmitter<any>();

  ngOnInit(): void {
  }


  getDateString(date: any) : string {
    return DateUtils.getDateString(date);
  }

  deleteProject() {
    this.projectService.deleteProject(this.project.id).subscribe({
      next: _ => this.projectDeletion.emit({ id: this.project.id, success: true}),
      error: err => this.projectDeletion.emit({ id: this.project.id, success: false, error: err}),
    });
  }

  isAuthenticated() {
    return this.authService.isAuthenticated()
  }
}

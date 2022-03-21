import { Component, OnInit } from '@angular/core';
import {ArticleService} from "../../services/article.service";
import {Article} from "../../models/article";
import {JobModel} from "../../models/job.model";
import {JobService} from "../../services/job.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private jobService: JobService) { }

  currentJob : JobModel | undefined | null;
  errorMessage : string | null = null;

  ngOnInit(): void {
    this.getCurrentJob();
  }

  getPosition() {
    return this.currentJob?.position + " at " + this.currentJob?.employer;
  }

  private getCurrentJob() {
    this.jobService.getCurrentJob().subscribe({
      next: value => this.currentJob = value,
      error: err => this.errorMessage = err
    });
  }
}

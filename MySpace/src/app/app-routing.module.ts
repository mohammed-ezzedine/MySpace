import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./pages/home/home.component";
import {ArticleComponent} from "./pages/article/article.component";
import {LoginComponent} from "./pages/auth/login/login.component";
import {AddArticleComponent} from "./pages/article/add-article/add-article.component";
import {EditArticleComponent} from "./pages/article/edit-article/edit-article.component";
import {ProjectsComponent} from "./pages/projects/projects.component";
import {JobsComponent} from "./pages/jobs/jobs.component";
import {AddJobComponent} from "./pages/jobs/add-job/add-job.component";
import {EditJobComponent} from "./pages/jobs/edit-job/edit-job.component";

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'article/:id', component: ArticleComponent },
  { path: 'admin/article/add-article', component: AddArticleComponent },
  { path: 'admin/article/edit-article/:id', component: EditArticleComponent },
  { path: 'jobs', component: JobsComponent },
  { path: 'admin/jobs/add-job', component: AddJobComponent },
  { path: 'admin/jobs/edit-job/:id', component: EditJobComponent },
  { path: 'projects', component: ProjectsComponent },
  { path: 'admin/login', component: LoginComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

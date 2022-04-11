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
import {ProjectComponent} from "./pages/projects/project/project.component";
import {AddProjectComponent} from "./pages/projects/add-project/add-project.component";
import {EditProjectComponent} from "./pages/projects/edit-project/edit-project.component";
import {AuthGuardService} from "./services/auth-guard.service";
import {NotFoundComponent} from "./pages/not-found/not-found.component";
import {UnauthorizedComponent} from "./pages/unauthorized/unauthorized.component";
import {SearchComponent} from "./pages/search/search.component";
import {SmallScreenGuardService} from "./services/small-screen-guard.service";

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'search', component: SearchComponent, canActivate: [ SmallScreenGuardService ] },
  { path: 'article/:id', component: ArticleComponent },
  { path: 'admin/article/add-article', component: AddArticleComponent, canActivate: [ AuthGuardService ] },
  { path: 'admin/article/edit-article/:id', component: EditArticleComponent, canActivate: [ AuthGuardService ], },
  { path: 'jobs', component: JobsComponent },
  { path: 'admin/jobs/add-job', component: AddJobComponent, canActivate: [ AuthGuardService ] },
  { path: 'admin/jobs/edit-job/:id', component: EditJobComponent, canActivate: [ AuthGuardService ] },
  { path: 'projects', component: ProjectsComponent },
  { path: 'projects/:id', component: ProjectComponent },
  { path: 'admin/projects/add-project', component: AddProjectComponent, canActivate: [ AuthGuardService ] },
  { path: 'admin/projects/edit-project/:id', component: EditProjectComponent, canActivate: [ AuthGuardService ] },
  { path: 'admin/login', component: LoginComponent },
  { path: 'unauthorized', component: UnauthorizedComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

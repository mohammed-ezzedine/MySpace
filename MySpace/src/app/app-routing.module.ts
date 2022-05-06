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
import {ArticleMetaGuardService} from "./services/article-meta-guard.service";

const default_data = {
  seo: {
    title: "Mohammed EZZEDINE's Space",
    description: "Documented with ♥ by Mohammed Ezzedine - Software Engineer",
    image: "/assets/avatar.png"
  }
}

const unauthorized_data = {
  seo: {
    title: '401 - Unauthorized'
  }
}

const notfound_data = {
  seo: {
    title: '404 - Not Found'
  }
}

const routes: Routes = [
  { path: 'home', component: HomeComponent, data: default_data },
  { path: 'search', component: SearchComponent, canActivate: [ SmallScreenGuardService ], data: default_data  },
  { path: 'article/:id', component: ArticleComponent, canActivate: [ ArticleMetaGuardService ] },
  { path: 'admin/article/add-article', component: AddArticleComponent, canActivate: [ AuthGuardService ] },
  { path: 'admin/article/edit-article/:id', component: EditArticleComponent, canActivate: [ AuthGuardService ] },
  { path: 'jobs', component: JobsComponent, data: default_data  },
  { path: 'admin/jobs/add-job', component: AddJobComponent, canActivate: [ AuthGuardService ] },
  { path: 'admin/jobs/edit-job/:id', component: EditJobComponent, canActivate: [ AuthGuardService ] },
  { path: 'projects', component: ProjectsComponent, data: default_data  },
  { path: 'projects/:id', component: ProjectComponent }, // TODO add guard service for meta data
  { path: 'admin/projects/add-project', component: AddProjectComponent, canActivate: [ AuthGuardService ] },
  { path: 'admin/projects/edit-project/:id', component: EditProjectComponent, canActivate: [ AuthGuardService ] },
  { path: 'admin/login', component: LoginComponent },
  { path: 'unauthorized', component: UnauthorizedComponent, data: unauthorized_data },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component: NotFoundComponent, data: notfound_data }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    initialNavigation: 'enabledBlocking'
})],
  exports: [RouterModule]
})
export class AppRoutingModule { }

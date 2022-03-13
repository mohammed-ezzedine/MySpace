import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./pages/home/home.component";
import {ArticleComponent} from "./pages/article/article.component";
import {LoginComponent} from "./pages/auth/login/login.component";
import {AddArticleComponent} from "./pages/article/add-article/add-article.component";

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'article/:id', component: ArticleComponent },
  { path: 'admin/login', component: LoginComponent },
  { path: 'admin/article/add-article', component: AddArticleComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

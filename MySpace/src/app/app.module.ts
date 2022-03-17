import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { SideBarComponent } from './components/side-bar/side-bar.component';
import { SideNavbarComponent } from './components/side-navbar/side-navbar.component';
import { MainWindowComponent } from './components/main-window/main-window.component';
import { HttpClientModule } from "@angular/common/http";
import { ArticlesComponent } from './components/articles/articles.component';
import { NZ_I18N } from 'ng-zorro-antd/i18n';
import { en_US } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzImageModule } from 'ng-zorro-antd/image';
import {ArticleThumbnailComponent} from "./components/article-thumbnail/article-thumbnail.component";
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { ArticleComponent } from './pages/article/article.component';
import { NzAlertModule } from 'ng-zorro-antd/alert';
import { LoginComponent } from './pages/auth/login/login.component';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { AddArticleComponent } from './pages/article/add-article/add-article.component';
import { ImageComponent } from './components/image/image.component';
import { CodeSnippetComponent } from './components/code-snippet/code-snippet.component';
import { NzMessageModule } from 'ng-zorro-antd/message';
import { NzSelectModule } from 'ng-zorro-antd/select';

import 'prismjs/components/prism-aspnet';
import 'prismjs/components/prism-bash';
import 'prismjs/components/prism-c';
import 'prismjs/components/prism-clike';
import 'prismjs/components/prism-cpp';
import 'prismjs/components/prism-csharp';
import 'prismjs/components/prism-css';
import 'prismjs/components/prism-css-extras';
import 'prismjs/components/prism-dart';
import 'prismjs/components/prism-docker';
import 'prismjs/components/prism-fsharp';
import 'prismjs/components/prism-git';
import 'prismjs/components/prism-go';
import 'prismjs/components/prism-go-module';
import 'prismjs/components/prism-graphql';
import 'prismjs/components/prism-http';
import 'prismjs/components/prism-ignore';
import 'prismjs/components/prism-java';
import 'prismjs/components/prism-javastacktrace';
import 'prismjs/components/prism-javascript';
import 'prismjs/components/prism-json';
import 'prismjs/components/prism-jsonp';
import 'prismjs/components/prism-less';
import 'prismjs/components/prism-markdown';
import 'prismjs/components/prism-mongodb';
import 'prismjs/components/prism-powershell';
import 'prismjs/components/prism-properties';
import 'prismjs/components/prism-protobuf';
import 'prismjs/components/prism-python';
import 'prismjs/components/prism-regex';
import 'prismjs/components/prism-sass';
import 'prismjs/components/prism-scss';
import 'prismjs/components/prism-sql';
import 'prismjs/components/prism-typescript';
import 'prismjs/components/prism-yaml';
import { AddCodeSnippetComponent } from './components/add-code-snippet/add-code-snippet.component';
import { AddImageComponent } from './components/add-image/add-image.component';
import { EditorComponent } from './components/editor/editor.component';
import { TagsComponent } from './components/tags/tags.component';
import { EditArticleComponent } from './pages/article/edit-article/edit-article.component';
import { SectionComponent } from './components/section/section.component';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';


registerLocaleData(en);

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SideBarComponent,
    SideNavbarComponent,
    MainWindowComponent,
    ArticleThumbnailComponent,
    ArticlesComponent,
    ArticleThumbnailComponent,
    ArticleComponent,
    LoginComponent,
    AddArticleComponent,
    ImageComponent,
    CodeSnippetComponent,
    AddCodeSnippetComponent,
    AddImageComponent,
    EditorComponent,
    TagsComponent,
    EditArticleComponent,
    SectionComponent,],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    NzTagModule,
    NzImageModule,
    NzSkeletonModule,
    NzDividerModule,
    NzInputModule,
    NzIconModule,
    NzTypographyModule,
    NzAlertModule,
    NzFormModule,
    NzButtonModule,
    NzMessageModule,
    NzSelectModule,
    NzPopconfirmModule,
    NzToolTipModule
  ],
  providers: [{ provide: NZ_I18N, useValue: en_US }],
  bootstrap: [AppComponent]
})
export class AppModule { }

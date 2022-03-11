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
import { FormsModule } from '@angular/forms';
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
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    NzTagModule,
    NzImageModule,
    NzSkeletonModule,
    NzDividerModule,
    NzInputModule,
    NzIconModule,
    NzTypographyModule,
    NzAlertModule
  ],
  providers: [{ provide: NZ_I18N, useValue: en_US }],
  bootstrap: [AppComponent]
})
export class AppModule { }

import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree} from "@angular/router";
import {map, Observable} from "rxjs";
import {ArticleService} from "./article.service";
import {SeoService} from "./seo.service";
import {SeoShareDataModel} from "../models/seo-share-data.model";
import {Injectable} from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class ArticleMetaGuardService implements CanActivate {

  constructor(private articleService: ArticleService,
              private seoService: SeoService) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let articleId = route.paramMap.get("id");
    if (!articleId) return false;

    return this.articleService.getArticle(articleId).pipe(
      map(
        article => {
          route.data = {
            seo: {
              title: article.title,
              description: article.description,
              image: article.imageUrl
            }
          }
          this.seoService.setData(new SeoShareDataModel(article.title, article.description, article.imageUrl));
          return true;
        }
      )
    )
  }

}

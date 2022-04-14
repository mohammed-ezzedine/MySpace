import {Injectable} from "@angular/core";
import {ActivatedRoute, NavigationEnd, Router} from "@angular/router";
import {SeoService} from "./seo.service";
import {filter, map} from "rxjs";

@Injectable({
  providedIn: "root"
})
export class RouteHelperService {

  constructor(private route: ActivatedRoute,
              private router: Router,
              private seoService: SeoService) {
    this.setupRouting();
  }

  private setupRouting() {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd),
      map(() => this.route),
      map(route => {
        while (route.firstChild) {
          route = route.firstChild;
        }
        return route;
      }),
      filter(route => route.outlet === 'primary')
    ).subscribe((route: ActivatedRoute) => {
      const seo = route.snapshot.data['seo'];
      this.seoService.setData(seo);
    });
  }
}

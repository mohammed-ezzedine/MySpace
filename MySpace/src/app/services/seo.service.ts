import {Meta, Title} from "@angular/platform-browser";
import {Injectable} from "@angular/core";
import {SeoShareDataModel} from "../models/seo-share-data.model";

@Injectable({
  providedIn: "root"
})
export class SeoService {
  constructor(private titleService: Title,
              private metaService: Meta) { }

  private static DEFAULT_TITLE = "Mohammed EZZEDINE's Space";
  private static DEFAULT_DESCRIPTION = "Documented with ♥ by Mohammed Ezzedine - Software Engineer";
  private static DEFAULT_IMAGE = "/assets/avatar.png";

  setData(data: SeoShareDataModel) {
    this.setTitle(data?.title);
    this.setDescription(data?.description);
    this.setImage(data?.image);
  }

  private setTitle(title: string = '') {
    this.titleService.setTitle(title);
    if (title && title.length) {
      this.metaService.updateTag({name: 'twitter:title', content: title});
      this.metaService.updateTag({name: 'twitter:image:alt', content: title});
      this.metaService.updateTag({property: 'og:image:alt', content: title});
      this.metaService.updateTag({property: 'og:title', content: title});
      this.metaService.updateTag({name: 'title', content: title});
    } else {
      this.metaService.updateTag({name: 'twitter:title', content: SeoService.DEFAULT_TITLE});
      this.metaService.updateTag({name: 'twitter:image:alt', content: SeoService.DEFAULT_TITLE});
      this.metaService.updateTag({property: 'og:image:alt', content: SeoService.DEFAULT_TITLE});
      this.metaService.updateTag({property: 'og:title', content: SeoService.DEFAULT_TITLE});
      this.metaService.updateTag({name: 'title', content: SeoService.DEFAULT_TITLE});
    }
  }

  private setDescription(description: string = '') {
    if (description && description.length) {
      this.metaService.updateTag({name: 'twitter:description', content: description});
      this.metaService.updateTag({property: 'og:description', content: description});
      this.metaService.updateTag({ name: "description", content: description });
    } else {
      this.metaService.updateTag({name: 'twitter:description', content: SeoService.DEFAULT_DESCRIPTION});
      this.metaService.updateTag({property: 'og:description', content: SeoService.DEFAULT_DESCRIPTION});
      this.metaService.updateTag({ name: "description", content: SeoService.DEFAULT_DESCRIPTION });
    }
  }

  private setImage(imageUrl: string = '') {
    if (imageUrl && imageUrl.length) {
      this.metaService.updateTag({name: 'twitter:image', content: imageUrl});
      this.metaService.updateTag({property: 'og:image', content: imageUrl});
      this.metaService.updateTag({ name: "image", content: imageUrl });
    } else {
      this.metaService.updateTag({name: 'twitter:image', content: SeoService.DEFAULT_IMAGE});
      this.metaService.updateTag({property: 'og:image', content: SeoService.DEFAULT_IMAGE});
      this.metaService.updateTag({ name: "image", content: SeoService.DEFAULT_IMAGE });
    }
  }
}

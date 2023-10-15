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
    this.updateDescription(data?.description);
    this.updateImage(data?.image);
  }

  private setTitle(title: string = '') {
    this.titleService.setTitle(title);
    if (title && title.length) {
      this.metaService.updateTag({name: 'twitter:title', content: title});
      this.metaService.updateTag({property: 'og:title', content: title});
      this.metaService.updateTag({name: 'title', content: title});
    } else {
      this.metaService.updateTag({name: 'twitter:title', content: SeoService.DEFAULT_TITLE});
      this.metaService.updateTag({property: 'og:title', content: SeoService.DEFAULT_TITLE});
      this.metaService.updateTag({name: 'title', content: SeoService.DEFAULT_TITLE});
    }
  }

  private updateDescription(description: string = '') {
    if (description && description.length) {
      this.setDescription(description);
    } else {
      this.setDescription(SeoService.DEFAULT_DESCRIPTION);
    }
  }

  private updateImage(imageUrl: string = '') {
    if (imageUrl && imageUrl.length) {
      this.setImage(imageUrl);
    } else {
      this.setImage(SeoService.DEFAULT_IMAGE);
    }
  }

  private setDescription(description: string) {
    this.metaService.addTag({ 'name': 'description', 'property':'og:description', 'content': description})
    this.metaService.addTag({ 'name': 'twitter:description', 'property':'twitter:description', 'content': description})
    this.metaService.addTag({ 'name': 'og:description', 'property':'og:description', 'content': description})
  }

  private setImage(imageUrl: string) {
    this.metaService.addTag({ 'name': 'image', 'property':'og:image', 'content': imageUrl})
    this.metaService.addTag({ 'name': 'twitter:image', 'property':'twitter:image', 'content': imageUrl})
    this.metaService.addTag({ 'name': 'og:image', 'property':'og:image', 'content': imageUrl})
  }
}

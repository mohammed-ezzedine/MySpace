import {readingTime} from "reading-time-estimator";
import {PageContentSection} from "../components/page-editor/page-content-section";

export class ArticleEstimatedReadingTimeCalculator {

  static calculate(contentControls: PageContentSection[]) : string {
    let data = ArticleEstimatedReadingTimeCalculator.getArticleContentString(contentControls);
    return readingTime(data, 180, 'en').text
  }

  static getArticleContentString(contentControls: PageContentSection[]) : string {
    let result = '';
    for (let section of contentControls) {
      if (section.type == 'code' || section.type == 'paragraph') {
        result += section.content.value;
      }
    }
    return this.strip(result)
  }

  private static strip(text: string) : string {
    return new DOMParser()?.parseFromString(text,"text/html")?.body?.textContent?? ''
  }
}

import {FormControl, FormGroup, Validators} from "@angular/forms";
import {readingTime} from "reading-time-estimator";
import {Content} from "@angular/compiler/src/render3/r3_ast";

export class ArticleUtils {
  static getArticleContent(contentControls: any, articleForm: FormGroup) {
    let result = [];
    for (let control of contentControls) {
      result.push(this.getControlItem(control, articleForm));
    }
    return result
  }

  static getArticleContentString(contentControls: any, articleForm: FormGroup) : string {
    let result = '';
    for (let control of contentControls) {
      let section = this.getControlItem(control, articleForm);
      if (section.type == 'code' || section.type == 'paragraph') {
        result += section.content;
      }
    }
    return this.strip(result)
  }

  static getArticleEstimatedReadingTime(contentControls: any, articleForm: FormGroup) : string {
    let data = ArticleUtils.getArticleContentString(contentControls, articleForm);
    return readingTime(data, 180, 'en').text
  }

  static getElementType(element: { id: number; controlInstance: string }) : string{
    return element.controlInstance.split('-')[0];
  }

  static addParagraph(articleForm: FormGroup, contentControls: { id: number; controlInstance: string }[], e?: MouseEvent) {
    if (e) {
      e.preventDefault();
    }
    const id = contentControls.length > 0 ? contentControls[contentControls.length - 1].id + 1 : 0;

    const control = {
      id,
      controlInstance: `paragraph-${id}`
    };

    const index = contentControls.push(control);
    articleForm.addControl(
      contentControls[index - 1].controlInstance,
      new FormControl(null, Validators.required)
    );
  }

  static addImage(articleForm: FormGroup, contentControls: { id: number; controlInstance: string }[], e?: MouseEvent) {
    if (e) {
      e.preventDefault();
    }
    const id = contentControls.length > 0 ? contentControls[contentControls.length - 1].id + 1 : 0;

    const control = {
      id,
      controlInstance: `image-${id}`
    };

    const index = contentControls.push(control);
    articleForm.addControl(
      contentControls[index - 1].controlInstance,
      new FormControl(null, Validators.required)
    );
  }

  static addCode(articleForm: FormGroup, contentControls: { id: number; controlInstance: string }[], e?: MouseEvent) {
    if (e) {
      e.preventDefault();
    }
    const id = contentControls.length > 0 ? contentControls[contentControls.length - 1].id + 1 : 0;

    const codeControl = {
      id: id,
      controlInstance: `code-${id}`
    };

    const languageControl = {
      id: id,
      controlInstance: `codelanguage-${id}`
    };

    contentControls.push(codeControl);

    articleForm.addControl(
      codeControl.controlInstance,
      new FormControl(null, Validators.required)
    );

    articleForm.addControl(
      languageControl.controlInstance,
      new FormControl(null, Validators.required)
    );
  }

  static deleteSection(articleForm: FormGroup, contentControls: { id: number; controlInstance: string }[], element: any) {
    let index = contentControls.findIndex(c => c.id == element.id);
    let numberOfElementsToPop = contentControls.length - index - 1;
    let controls = this.popControlsAfterTarget(numberOfElementsToPop, contentControls);
    this.popTargetControl(contentControls);
    this.pushPoppedControls(controls, contentControls);

    articleForm.removeControl(element.controlInstance)
  }

  static initializeContentControls(form: FormGroup, contentControls: { id: number; controlInstance: string }[], content: any[]) {
    for (let section of content) {
      const id = contentControls.length > 0 ? contentControls[contentControls.length - 1].id + 1 : 0;

      this.addExistingContentSection(form, contentControls, section, id);
    }
  }

  private static addExistingContentSection(form: FormGroup, contentControls: { id: number; controlInstance: string }[], section: any, id: number) {
    switch (section.type) {
      case 'code':
        this.addExistingCodeSection(form, contentControls, id, section);
        break;
      case 'paragraph':
        this.addExistingParagraphSection(form, contentControls, id, section);
        break;
      case 'image':
        this.addExistingImageSection(form, contentControls, id, section);
        break;
      default:
        break;
    }
  }

  private static addExistingImageSection(form: FormGroup, contentControls: { id: number; controlInstance: string }[], id: number, section: any) {
    const control = {
      id,
      controlInstance: `image-${id}`
    };

    const index = contentControls.push(control);
    form.addControl(
      contentControls[index - 1].controlInstance,
      new FormControl(section.imageUrl, Validators.required)
    );
  }

  private static addExistingParagraphSection(form: FormGroup, contentControls: { id: number; controlInstance: string }[], id: number, section: any) {
    const control = {
      id,
      controlInstance: `paragraph-${id}`
    };

    const index = contentControls.push(control);
    form.addControl(
      contentControls[index - 1].controlInstance,
      new FormControl(section.content, Validators.required)
    );
  }

  private static addExistingCodeSection(form: FormGroup, contentControls: { id: number; controlInstance: string }[], id: number, section: any) {
    const control = {
      id,
      controlInstance: `code-${id}`
    };

    const languageControl = {
      id: id,
      controlInstance: `codelanguage-${id}`
    };

    const index = contentControls.push(control);
    form.addControl(
      contentControls[index - 1].controlInstance,
      new FormControl(section.content, Validators.required)
    );

    form.addControl(
      languageControl.controlInstance,
      new FormControl(section.language, Validators.required)
    );
  }

  private static pushPoppedControls(controls: { id: number; controlInstance: string }[], contentControls: { id: number; controlInstance: string }[]) {
    while (controls.length != 0) {
      contentControls.push(controls.pop()!)
    }
  }

  private static popTargetControl(contentControls: { id: number; controlInstance: string }[]) {
    contentControls.pop();
  }

  private static popControlsAfterTarget(numberOfElementsToPop: number, contentControls: { id: number; controlInstance: string }[]) {
    let controls: { id: number; controlInstance: string }[] = [];
    for (let i = 0; i < numberOfElementsToPop; i++) {
      controls.push(contentControls.pop()!);
    }
    return controls;
  }

  private static strip(text: string) : string {
    return new DOMParser()?.parseFromString(text,"text/html")?.body?.textContent?? ''
  }

  private static getControlItem(control: { id: number; controlInstance: string }, articleForm: FormGroup) : any {
    switch (this.getElementType(control)) {
      case 'code': return this.getCodeItem(control, articleForm);
      case 'image': return this.getImageItem(control, articleForm);
      case 'paragraph': return this.getParagraphItem(control, articleForm);
      default: return null;
    }
  }

  private static getParagraphItem(control: { id: number; controlInstance: string }, articleForm: FormGroup) {
    let paragraph = articleForm.controls[`paragraph-${control.id}`].value;
    return {
      type: 'paragraph',
      content: paragraph,
    };
  }

  private static getImageItem(control: { id: number; controlInstance: string }, articleForm: FormGroup) {
    let imageUrl = articleForm.controls[`image-${control.id}`].value;
    return {
      type: 'image',
      imageUrl: imageUrl,
    };
  }

  private static getCodeItem(control: { id: number; controlInstance: string }, articleForm: FormGroup) {
    let codeLanguage = articleForm.controls[`codelanguage-${control.id}`].value;
    let code = articleForm.controls[`code-${control.id}`].value;

    return {
      type: 'code',
      language: codeLanguage,
      content: code
    };
  }
}

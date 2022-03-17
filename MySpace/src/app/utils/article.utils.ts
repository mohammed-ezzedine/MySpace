import {FormControl, FormGroup, Validators} from "@angular/forms";

export class ArticleUtils {
  static getArticleContent(contentControls: any, articleForm: FormGroup) {
    let result = [];
    for (let control of contentControls) {
      result.push(this.getControlItem(control, articleForm));
    }
    return result
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
    contentControls = contentControls.filter(c => c.id != element.id);
    articleForm.removeControl(element.controlInstance)
  }

  private static getControlItem(control: { id: number; controlInstance: string }, articleForm: FormGroup) {
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

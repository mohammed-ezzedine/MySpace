import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCodeSnippetComponent } from './add-code-snippet.component';

describe('AddCodeSnippetComponent', () => {
  let component: AddCodeSnippetComponent;
  let fixture: ComponentFixture<AddCodeSnippetComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddCodeSnippetComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddCodeSnippetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

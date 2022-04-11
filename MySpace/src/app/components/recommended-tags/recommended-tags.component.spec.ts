import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecommendedTagsComponent } from './recommended-tags.component';

describe('RecommendedTagsComponent', () => {
  let component: RecommendedTagsComponent;
  let fixture: ComponentFixture<RecommendedTagsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecommendedTagsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RecommendedTagsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

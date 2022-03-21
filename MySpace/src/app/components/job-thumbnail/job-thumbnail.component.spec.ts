import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobThumbnailComponent } from './job-thumbnail.component';

describe('JobThumbnailComponent', () => {
  let component: JobThumbnailComponent;
  let fixture: ComponentFixture<JobThumbnailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JobThumbnailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(JobThumbnailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

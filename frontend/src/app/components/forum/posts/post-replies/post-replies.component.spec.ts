import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PostRepliesComponent } from './post-replies.component';

describe('PostRepliesComponent', () => {
  let component: PostRepliesComponent;
  let fixture: ComponentFixture<PostRepliesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PostRepliesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PostRepliesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

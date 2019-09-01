import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PostRepliesCreateComponent } from './post-replies-create.component';

describe('PostRepliesCreateComponent', () => {
  let component: PostRepliesCreateComponent;
  let fixture: ComponentFixture<PostRepliesCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PostRepliesCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PostRepliesCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

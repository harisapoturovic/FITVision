import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ForumOdgovoriComponent } from './forum-odgovori.component';

describe('ForumOdgovoriComponent', () => {
  let component: ForumOdgovoriComponent;
  let fixture: ComponentFixture<ForumOdgovoriComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ForumOdgovoriComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ForumOdgovoriComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

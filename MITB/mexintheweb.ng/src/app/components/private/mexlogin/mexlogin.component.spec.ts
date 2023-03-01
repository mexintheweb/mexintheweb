import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MexloginComponent } from './mexlogin.component';

describe('MexloginComponent', () => {
  let component: MexloginComponent;
  let fixture: ComponentFixture<MexloginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MexloginComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MexloginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

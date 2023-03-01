import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { AboutComponent } from './components/about/about.component';
import { NavComponent } from './nav/nav.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';
import {MatCommonModule} from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import {MatMenuModule} from '@angular/material/menu';
import { ImpressumComponent } from './components/impressum/impressum.component';
import { MexloginComponent } from './components/private/mexlogin/mexlogin.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { APP_BASE_HREF } from '@angular/common';
import { ApiInterceptor } from './models/http-interceptor.model';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AboutComponent,
    NavComponent,
    ImpressumComponent,
    MexloginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatToolbarModule,
    MatIconModule,
    MatCommonModule,
    MatButtonModule,
    MatMenuModule
  ],
  providers: [
    /*{provide: APP_BASE_HREF, useValue: '/'},*/
    { provide: HTTP_INTERCEPTORS, useClass: ApiInterceptor, multi: true }
  ], //providers: [{provide: APP_BASE_HREF, useValue: '/my/app'}]
  bootstrap: [AppComponent]
})
export class AppModule { }

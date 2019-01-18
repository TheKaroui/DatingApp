import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { ValueComponent } from './value/value.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { NavComponent } from './nav/nav.component';

import { DataService } from './data.service';

@NgModule({
   declarations: [
      AppComponent,
      ValueComponent,
      NavComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      ServiceWorkerModule.register('/ngsw-worker.js', { enabled: environment.production }),
      FormsModule
   ],
   providers: [DataService],
   bootstrap: [AppComponent]
})
export class AppModule {}

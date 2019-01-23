import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { NavComponent } from './nav/nav.component';

import { DataService } from './_services/data.service';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ErrorIntercepptorProvider } from './_services/error.interceptor';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      ServiceWorkerModule.register('/ngsw-worker.js', { enabled: environment.production }),
      FormsModule
   ],
   providers: [DataService, AuthService, ErrorIntercepptorProvider],
   bootstrap: [AppComponent]
})
export class AppModule {}

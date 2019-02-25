import { MemberListResolver } from './_resolvers/member-list.resolver';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { AlertifyService } from './_services/alertify.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule, TabsModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxGalleryModule } from 'ngx-gallery';

import { AppComponent } from './app.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { NavComponent } from './nav/nav.component';
import { DataService } from './_services/data.service';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ErrorIntercepptorProvider } from './_services/error.interceptor';
import { MemberListComponent } from './members/member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { appRoutes } from 'src/_Routing/routes';
import { AuthGuard } from './../_Routing/_guards/auth.guard';
import { UserService } from './_services/user.service';
import { MemberCardComponent } from './members/member-card/member-card.component';


export function tokenGetter() {
   return localStorage.getItem('token');
 }

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      MemberListComponent,
      ListsComponent,
      MessagesComponent,
      MemberCardComponent,
      MemberDetailComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      ServiceWorkerModule.register('/ngsw-worker.js', { enabled: environment.production }),
      FormsModule,
      BsDropdownModule.forRoot(),
      TabsModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      NgxGalleryModule,
      JwtModule.forRoot({
         config: {
           tokenGetter: tokenGetter,
           whitelistedDomains: ['localhost:5000'],
           blacklistedRoutes: ['localhost:5000/api/auth']
         }
       })
   ],
   providers: [
      DataService,
      AuthService,
      ErrorIntercepptorProvider,
      AlertifyService,
      AuthGuard,
      UserService,
      MemberDetailResolver,
      MemberListResolver
   ],
   bootstrap: [AppComponent]
})
export class AppModule {}

import { AuthService } from './_services/auth.service';
import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { SwUpdate } from '@angular/service-worker';
import { DataService } from './_services/data.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  jwtHelper = new JwtHelperService();
  update = false;
  joke: any;

  constructor(updates: SwUpdate, private data: DataService, private authservice: AuthService) {
    updates.available.subscribe(event => {
      updates.activateUpdate().then(() => window.location.reload());
    });
  }

  ngOnInit() {
    const token = localStorage.getItem('token');
    if (token) {
      this.authservice.decodedToken = this.jwtHelper.decodeToken(token);
    }
    this.data.gimmeJokes().subscribe(res => {
      this.joke = res;
    });
  }
}

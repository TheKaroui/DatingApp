import { AlertifyService } from './../_services/alertify.service';
import { AuthService } from './../_services/auth.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() valuesFromParent: any;
  @Output() registerCancelled = new EventEmitter();
  model: any = {};

  constructor(private authservice: AuthService, private alertify: AlertifyService) {}

  ngOnInit() {}

  register() {
    this.authservice.register(this.model).subscribe(() => {
      this.alertify.success('registration successfull');
    },
    error => {
      this.alertify.error(error);
    });
  }
  cancel() {
    this.registerCancelled.emit(true); // we can emit every thing in this case its a simple false
    this.alertify.message('registration cancelled');
  }
}

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

  constructor(private authservice: AuthService) {}

  ngOnInit() {}

  register() {
    this.authservice.register(this.model).subscribe(() => {
      console.log('registration successful');
    },
    error => {
      console.log(error);
    });
  }
  cancel() {
    this.registerCancelled.emit(true); // we can emit every thing in this case its a simple false
    console.log('registration cancelled');
  }
}

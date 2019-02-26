import { Component, OnInit, EventEmitter } from '@angular/core';
import { Input } from '@angular/core';
import { Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // @Input() valuesFromHome: any;
  @Output() cancelRegister = new EventEmitter();

  model: any = {};
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  register() {
      console.log(this.model);
      this.authService.register(this.model).subscribe(() => {
          console.log('registration successfully');
        }, error => {
            console.log(error);
        });
  }

  cancel() {
    this.cancelRegister.emit(false);
    console.log('cancel');
  }



}

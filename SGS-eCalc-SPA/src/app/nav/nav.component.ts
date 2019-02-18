import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model:  any = {};
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }
  login() {
    this.authService.login(this.model).subscribe(next => {
      // login successfully
      console.log('Logged successfully');
    // tslint:disable-next-line:no-shadowed-variable
    }, error => {
      console.log('failed to login');
    });
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !! token;
  }
  logout(){
    localStorage.removeItem('token');
    console.log('logged out');
  }
}

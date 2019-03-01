import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { error } from '@angular/compiler/src/util';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';
import { HomeComponent } from '../home/home.component';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model:  any = {};
  constructor(public authService: AuthService, private alertifyService: AlertifyService, private router: Router) { }
  ngOnInit() {
  }
  login() {
    this.authService.login(this.model).subscribe(next => {
      // login successfully
      console.log('Logged successfully');
      this.alertifyService.success('Logged successfully');
    // tslint:disable-next-line:no-shadowed-variable
    }, error => {
      this.alertifyService.error(error);
    }, () => {
      this.router.navigate(['/members']);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }
  logout() {
    localStorage.removeItem('token');
    console.log('logged out');
    this.alertifyService.message('logged out');
    this.router.navigate(['/home']);
  }
}

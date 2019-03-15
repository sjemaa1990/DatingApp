import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../_services/user.service';
import { AlertifyService } from '../../../_services/alertify.service';
import { User } from '../../../_models/user';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {

  user: User;
  constructor(private userService: UserService, private alertify: AlertifyService,
     private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data['user'];
    });
  }

  // member/Id    + to convert the id from string to int because is is in url
  // loadUser() {
  //   this.userService.getUser(+this.route.snapshot.params['id']).subscribe ((user: User) => {
  //     this.user = user;
  //   }, error => {
  //       this.alertify.error(error);
  //     }
  //   );
  // }

}

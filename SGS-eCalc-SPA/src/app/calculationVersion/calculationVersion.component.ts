import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-calculationversion',
  templateUrl: './calculationVersion.component.html',
  styleUrls: ['./calculationVersion.component.css']
})
export class CalculationVersionComponent implements OnInit {

  values: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getValues();
  }

  getValues() {
    this.http.get('http://localhost:5000/api/values').subscribe(response => {
      this.values = response;
    },
      error => {
        console.log(error.message);
      });
  }
}

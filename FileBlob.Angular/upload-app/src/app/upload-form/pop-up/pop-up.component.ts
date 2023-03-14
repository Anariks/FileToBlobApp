import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-pop-up',
  templateUrl: './pop-up.component.html',
  styleUrls: ['./pop-up.component.scss']
})
export class PopUpComponent {

  header: string;
  message: string;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) {
    this.header = data.header;
    this.message = data.message;
  }
}

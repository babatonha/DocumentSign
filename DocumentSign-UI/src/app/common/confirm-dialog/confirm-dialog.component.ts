import { CommonModule } from '@angular/common';
import { Component, EventEmitter, NgModule, OnInit, Output } from '@angular/core';
import { DxButtonModule, DxCheckBoxModule, DxPopupModule, DxTabPanelModule, DxToolbarModule, DxTreeListModule } from 'devextreme-angular';


@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.scss']
})
export class ConfirmDialogComponent implements OnInit {

  isConfirmPopupVisible: Boolean=false;
  public message: String = "";
  public title:String="";

  @Output() private confirmClick: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

 public confirmClicked() {
    this.confirmClick.emit();
  }

  show(title:String, message: String) {
    this.message = message;
    this.title=title;
    this.isConfirmPopupVisible = true;
  }

  hide() {
    this.isConfirmPopupVisible = false;
  }

}


@NgModule({
  imports: [
    CommonModule,
    DxButtonModule,
    DxToolbarModule,
    DxPopupModule,
    DxTreeListModule,
    DxTabPanelModule,
    DxCheckBoxModule,
  ],
  declarations: [ ConfirmDialogComponent ],
  exports: [ ConfirmDialogComponent ]
})
export class ConfirmDialogModule{ }

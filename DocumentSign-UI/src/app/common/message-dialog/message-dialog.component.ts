import { CommonModule } from '@angular/common';
import { Component, NgModule, OnInit } from '@angular/core';
import { DxButtonModule, DxCheckBoxModule, DxPopupModule, DxTabPanelModule, DxToolbarModule, DxTreeListModule } from 'devextreme-angular';
import { DialogMessageTypes } from '../enums/dialog-message-types';

@Component({
  selector: 'app-message-dialog',
  templateUrl: './message-dialog.component.html',
  styleUrls: ['./message-dialog.component.css']
})
export class MessageDialogComponent implements OnInit {
  public isMessagePopupVisible: Boolean = false;
  public messageTitle: String = "System Message";
  public message: String = "";
  public showInformationIcon: Boolean = true;
  public showWarningIcon: Boolean = false;
  public messageType: DialogMessageTypes;

  constructor() { }

  ngOnInit() {
  }

  public closeMessageClick() {
    this.isMessagePopupVisible = false;
  }

  public show(message: String, messageType: DialogMessageTypes) {
    this.messageTitle = "System Message";
    this.message = message;
    this.showInformationIcon = true;
    this.showWarningIcon = false;
    if (messageType == DialogMessageTypes.SystemError) {
      this.showInformationIcon = false;
      this.showWarningIcon = true;
      this.messageTitle = "System Error";
    }
    this.isMessagePopupVisible = true;
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
  declarations: [ MessageDialogComponent ],
  exports: [ MessageDialogComponent ]
})
export class MessageDialogModule{ }

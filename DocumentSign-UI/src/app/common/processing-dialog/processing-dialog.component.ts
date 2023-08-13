import { CommonModule } from '@angular/common';
import { Component, NgModule, OnInit } from '@angular/core';
import { DxButtonModule, DxCheckBoxModule, DxDataGridModule, DxFormModule, DxLoadIndicatorModule, DxLoadPanelModule, DxPopupModule, DxTabPanelModule, DxToolbarModule, DxTreeListModule } from 'devextreme-angular';
import { NgxSpinnerModule, NgxSpinnerService } from 'ngx-spinner';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@Component({
  selector: 'app-processing-dialog',
  templateUrl: './processing-dialog.component.html',
  styleUrls: ['./processing-dialog.component.scss']
})
export class ProcessingDialogComponent implements OnInit {

  constructor(private spinnerService: NgxSpinnerService) { }

  ngOnInit() {
  }

  show(){
    this.spinnerService.show();
  }

  hide(){
    this.spinnerService.hide();
  }

}



@NgModule({
  declarations: [ ProcessingDialogComponent
  ],
  imports: [
    CommonModule,
    DxDataGridModule,
    DxPopupModule,
    DxToolbarModule,
    DxButtonModule,
    DxFormModule,
    //Ng4LoadingSpinnerModule.forRoot(),
    DxTreeListModule,
    DxTabPanelModule,
    DxCheckBoxModule,
    DxLoadPanelModule,
    NgxSpinnerModule,
    BrowserAnimationsModule
  ],
  exports: [
    ProcessingDialogComponent
  ]
})
export class ProcessingDialogModule { }

import { CommonModule } from '@angular/common';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, ElementRef, NgModule, OnInit, Output, ViewChild } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DxBoxModule, DxButtonModule, DxDataGridModule, DxDateBoxModule, DxDropDownBoxModule, DxFormModule, DxLoadIndicatorModule, DxLookupModule, DxNumberBoxModule, DxPopupModule, DxScrollViewModule, DxTabPanelModule, DxTextAreaModule, DxTextBoxModule, DxToolbarModule } from 'devextreme-angular';
import { DxiItemModule, DxoGridModule, DxoPagerModule } from 'devextreme-angular/ui/nested';
import { ConfirmDialogComponent, ConfirmDialogModule } from 'src/app/common/confirm-dialog/confirm-dialog.component';
import { DialogMessageTypes } from 'src/app/common/enums/dialog-message-types';
import { MessageDialogComponent, MessageDialogModule } from 'src/app/common/message-dialog/message-dialog.component';
import { ProcessingDialogComponent, ProcessingDialogModule } from 'src/app/common/processing-dialog/processing-dialog.component';
import { locale } from "devextreme/localization";
import config from "devextreme/core/config";
import { UtilitiesService } from 'src/app/shared/services/utilitites-service';
import { CustomControllerService } from 'src/app/shared/services/custom-controller-service';
import { ResponseStatus } from 'src/app/common/enums/response-statuses';
import { ResponseObject } from 'src/app/models/response-object';
import { vwDocumentsService } from 'src/app/services/vw-documents-service';
import { ProfileComponent } from '../../profile/profile.component';
import { WorkflowComponent } from '../../workflow/workflow.component';

@Component({
  selector: 'app-document-grid',
  templateUrl: './document-grid.component.html',
  styleUrls: ['./document-grid.component.scss']
})
export class DocumentGridComponent implements OnInit {

  selectedDocumentRecords;
  isNewDcoumentPopupVisible: Boolean = false;
  fileID;
  fileName;
  fileType;
  myInvoiceDocuments = [];
  documentDescription: string;
  documentInvoiceDate: Date;
  documentAmount: number;
  documentInvoiceNumber: string;
  documentCategory: string = "";
  invoiceDocument = [];
  singleInvoiceDocument: InvoiceDocumentRecord = new InvoiceDocumentRecord;
  isChooseDocumentDisabled: Boolean = true;
  lblInvoiceAttachment: string;
  documentRowIndex: number;
  selectedRecords;
  confirmAction: string = "";
  userName: string = localStorage.getItem("userName");
  invoiceNumbers: string = "";
  isEditInvoiceAmountPopupVisible: Boolean = false;
  documentEditInvoiceNumber: string;
  documentEditAmount: number;
  @ViewChild('invoiceDocumentsInput') invoiceInputVariable: ElementRef;
  userId: number = 1;

  documentDataSource = this.vwDocuments.oDataSource;

  @ViewChild(ConfirmDialogComponent) confirmDialog: ConfirmDialogComponent;
  @ViewChild(MessageDialogComponent) messageDialog: MessageDialogComponent;
  @ViewChild(ProcessingDialogComponent) processingDialog: ProcessingDialogComponent;

  constructor(private utilitiesService: UtilitiesService,
              protected httpClient: HttpClient,
              private vwDocuments: vwDocumentsService,
              private customControllerService: CustomControllerService) {
                locale('en-ZA');
                config({
                  defaultCurrency: "ZAR"
                });
               }

  ngOnInit(): void {
    
  }

  uploadClick(){
    if( this.invoiceDocument.length >0){
      this.confirmAction = "UploadDocument";
      this.confirmDialog.show("Submit Document", "Are you sure you want to upload and submit this document? ");
    }else{
      this.messageDialog.show("Please attach at least one document", DialogMessageTypes.Information);
    }

  }

  cancelCloseClick(){
    this.invoiceInputVariable.nativeElement.value ='';
    this.isNewDcoumentPopupVisible = false; 
  }

  refreshGrid(){
    this.vwDocuments.get().subscribe(
      data=>{
        this.documentDataSource = data.value;
      },error =>{
        this.messageDialog.show("A system error has occurerd while trying to get documents", DialogMessageTypes.SystemError);
      }
    );
  }

  getDocuments(e) {
    this.myInvoiceDocuments = [];
    if (e.target.files.length > 0) {

      for (var i = 0; i < e.target.files.length; i++) {
        let flag = 0;
       

        if (this.invoiceDocument == null) {
          this.singleInvoiceDocument = new InvoiceDocumentRecord();
          this.myInvoiceDocuments.push(e.target.files[i]);
          this.singleInvoiceDocument = this.myInvoiceDocuments[i];
          this.singleInvoiceDocument.FileName = this.myInvoiceDocuments[i].name;
          this.singleInvoiceDocument.FileType = this.myInvoiceDocuments[i].type;
          this.lblInvoiceAttachment =   this.singleInvoiceDocument.FileName;
          

          if (this.invoiceDocument == null) {
            this.invoiceDocument = [];
            this.invoiceDocument.push(this.singleInvoiceDocument);
          } else {
            this.invoiceDocument.push(this.singleInvoiceDocument);
          }
        } else {

          //validate duplicate documents
          for (var j = 0; j < this.invoiceDocument.length; j++) {
            if (e.target.files[i].size == this.invoiceDocument[j].size && e.target.files[i].name == this.invoiceDocument[j].FileName) {
              flag = 1;
            }
          }

          if (flag == 1) {
            this.messageDialog.show("You can not upload the same document twice", DialogMessageTypes.Information);
          } else {
            this.singleInvoiceDocument = new InvoiceDocumentRecord();
            this.myInvoiceDocuments.push(e.target.files[i]);
            this.singleInvoiceDocument = this.myInvoiceDocuments[i];
            this.singleInvoiceDocument.FileName = this.myInvoiceDocuments[i].name;
            this.singleInvoiceDocument.FileType = this.myInvoiceDocuments[i].type;
            this.lblInvoiceAttachment =  this.singleInvoiceDocument.FileName;
            

            if (this.invoiceDocument == null) {
              this.invoiceDocument = [];
              this.invoiceDocument.push(this.singleInvoiceDocument);
            } else {
              this.invoiceDocument.push(this.singleInvoiceDocument);
            }
        }
        }
      }
    }else{
      this.invoiceInputVariable.nativeElement.value ='';
    }
}

saveDocument(){
  if (this.invoiceDocument.length>0) {
    var counter = 0;
    const frmData = new FormData();
    for (var p = 0; p < this.invoiceDocument.length; p++) {
      const frmData = new FormData();
      this.processingDialog.show();
      frmData.append("fileUpload", this.invoiceDocument[p]);
      this.customControllerService.utilitiesPost("Documents/UploadDocument?userId=" + this.userId, frmData).subscribe(
          data => {
            let response: ResponseObject = data;
            if (response.Status == ResponseStatus.Success) {
              this.isNewDcoumentPopupVisible = false;
              this.invoiceInputVariable.nativeElement.value ='';
              this.refreshGrid();
              this.processingDialog.hide();
            } else {
              this.processingDialog.hide();
              this.messageDialog.show("A system error was encountered trying to submit your document.", DialogMessageTypes.SystemError);
            }
          }
        )   
    }

  } 
}

removeDocument(){
    if (this.selectedRecords[0].DocumentId == undefined) {
      this.invoiceDocument.splice(this.documentRowIndex, 1);
    } else {
      this.customControllerService.utilitiesGet("Documents/DeleteDocument?documentId=" + this.selectedRecords[0].DocumentId).subscribe(
        data => {
          let response: ResponseObject = data;

          if (response.Status == ResponseStatus.Success) {

            this.invoiceDocument.splice(this.documentRowIndex, 1);
            this.refreshGrid();

            this.messageDialog.show("Document deleted successfully", DialogMessageTypes.Information);
          }
          else if (response.Status == ResponseStatus.Failure) {
            this.messageDialog.show(response.StatusDescription, DialogMessageTypes.Information);
          }
          else {
            this.messageDialog.show("A system error was encountered trying to delete this record.", DialogMessageTypes.SystemError);
          }
        },
        error => {
          this.messageDialog.show("A system error was encountered trying to delete this record.", DialogMessageTypes.SystemError);
        }
      )
    }
}


  confirmClick(){
    this.confirmDialog.hide();

    if(this.confirmAction ==="UploadDocument"){
      this.saveDocument();
    }
    else if (this.confirmAction == "DeleteDocument") {
      this.removeDocument();    
    }
  }
 
  AddDocuments() {
    this.isNewDcoumentPopupVisible = true;
    //this.documentCatgoryLookup.value = 0;
    this.documentCategory = ""; 
    this.documentAmount = undefined;
    this.documentDescription = undefined;
    this.documentInvoiceNumber = undefined;
    this.documentInvoiceDate = undefined;
    this.lblInvoiceAttachment = "";
  }

  downloadDocument() {
    if (this.selectedRecords == undefined || this.selectedRecords[0] == undefined) {
      this.messageDialog.show("Select file to preview.", DialogMessageTypes.Information);
    }
    else {
      this.fileID = this.selectedRecords[0].DocumentId;

      if (this.fileID == undefined) {
        const url= window.URL.createObjectURL(new Blob([this.selectedRecords[0]], { type: "application/pdf" }));
        this.myPopup(url,"Requisition",1050,550);
      } else {
        this.vwDocuments.searchNonOdata("VwDocuments?$filter=DocumentId eq " + this.fileID).subscribe(
          data => {
            this.fileName = data.value[0].FileName;
            this.fileType = data.value[0].FileType;
          }
        )
        this.downloadFile(this.fileID.toString()).subscribe(
          data => {
            const url= window.URL.createObjectURL(new Blob([data], { type: "application/pdf" }));
          this.myPopup(url,"Document",1050,550);
          }
        )
      }
    }
  }

  downloadFile(fileID: string) {
    const REQUEST_URL = this.utilitiesService.baseAPIUrl + "api/Documents/DownloadDocument";
    const HTTP_PARAMS = new HttpParams().set("fileID", fileID);
    return this.httpClient.get(REQUEST_URL, {
      params: HTTP_PARAMS,
      responseType: 'arraybuffer'
    })
  }

  deleteDocument() {
    if (this.selectedRecords == undefined || this.selectedRecords[0] == undefined) {
      this.messageDialog.show("Select file to delete.", DialogMessageTypes.Information);
    }
    else {
      this.confirmAction = "DeleteDocument";
      this.confirmDialog.show("Delete Document", "Are you sure you want to delete document with name: " + this.selectedRecords[0].DocumentName + "?");
    }

  }

  myPopup(myURL, title, myWidth, myHeight) {
    var left = (screen.width - myWidth) / 2;
    var top = (screen.height - myHeight) / 4;
    var myWindow = window.open(myURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + myWidth + ', height=' + myHeight + ', top=' + top + ', left=' + left);
 }

  documentOnToolbarPreparing(e) {
    e.toolbarOptions.items.unshift(
      {
        location: 'before',
        locateInMenu: 'auto',
        widget: 'dxButton',
        options: {
          hint: 'Add',
          icon: 'plus',
          text: 'Upload Document',
          type: 'normal',
          onClick: this.AddDocuments.bind(this)
        }
      },
      {
        location: 'before',
        locateInMenu: 'auto',
        widget: 'dxButton',
        options: {
          hint: 'Preview',
          icon: 'search',
          text: 'Preview',
          type: 'normal',
          onClick: this.downloadDocument.bind(this)
        }
      },
      {
        location: 'before',
        locateInMenu: 'auto',
        widget: 'dxButton',
        options: {
          hint: 'Delete',
          icon: 'trash',
          text: 'Delete',
          type: 'normal',
          onClick: this.deleteDocument.bind(this)
        }
      }
    );
  }

}



@NgModule({
  imports: [
    CommonModule,
    DxDropDownBoxModule,
    RouterModule,
    DxFormModule,
    DxiItemModule,
    DxLoadIndicatorModule,
    ProcessingDialogModule,
    MessageDialogModule,
    DxButtonModule,
    DxToolbarModule,
    DxLookupModule,
    DxTabPanelModule,
    DxoGridModule,
    DxScrollViewModule,
    DxDataGridModule,
    DxPopupModule,
    DxNumberBoxModule,
    DxDateBoxModule,
    DxTextBoxModule,
    ConfirmDialogModule,
    DxBoxModule,
    DxoPagerModule,
    DxTextAreaModule
  ],
  declarations: [
    DocumentGridComponent,
    WorkflowComponent
   ],
  exports: [
    DocumentGridComponent,
    WorkflowComponent
   ]
})
export class DocumentGridModule { }


export class InvoiceDocumentRecord{
  public  ID : number;
  public  RequisitionID : number;
  public  FileName : string;
  public  size: string;
  public  FileType : string;
  public  fileDescription : string;
  public  Category: string;
  public InvoiceNumber : string;
  public Description : string;
  public Amount : number;
  public InvoiceDate: Date;
}


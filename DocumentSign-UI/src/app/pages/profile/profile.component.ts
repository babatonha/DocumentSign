import { CommonModule } from '@angular/common';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, ElementRef, NgModule, OnInit, ViewChild } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { DxBoxModule, DxButtonModule, DxDataGridModule, DxDateBoxModule, DxDropDownBoxModule, DxFormModule, DxLookupModule, DxPopupModule, DxScrollViewModule, DxTabPanelModule, DxTextBoxModule, DxToolbarModule } from 'devextreme-angular';
import { DxiItemModule } from 'devextreme-angular/ui/nested';
import { ConfirmDialogComponent, ConfirmDialogModule } from 'src/app/common/confirm-dialog/confirm-dialog.component';
import { DialogMessageTypes } from 'src/app/common/enums/dialog-message-types';
import { ResponseStatus } from 'src/app/common/enums/response-statuses';
import { MessageDialogComponent, MessageDialogModule } from 'src/app/common/message-dialog/message-dialog.component';
import { ProcessingDialogComponent, ProcessingDialogModule } from 'src/app/common/processing-dialog/processing-dialog.component';
import { ResponseObject } from 'src/app/models/response-object';
import { UserProfile } from 'src/app/models/user-profile';
import { saveAs } from 'file-saver';
import { UtilitiesService } from 'src/app/shared/services/utilitites-service';
import { AuthService } from 'src/app/shared/services';
import { CustomControllerService } from 'src/app/shared/services/custom-controller-service';
import { locale } from "devextreme/localization";
import config from "devextreme/core/config";
import { SignaturePad, SignaturePadModule } from 'angular2-signaturepad';
import { vwUserProfileService } from 'src/app/services/vw-user-profile-service';
import { vwUserDepartmentService } from 'src/app/services/vw-user-department-service';
import { vwDepartmentService } from 'src/app/services/vw-department-service';
import { UserDepartment } from 'src/app/models/user-department';

@Component({
  templateUrl: 'profile.component.html',
  styleUrls: [ './profile.component.scss' ]
})

export class ProfileComponent implements OnInit{

  employee: any;
  colCountByScreen: object;
  userProfile: UserProfile = new UserProfile();
  accountTypes = [{Name: "Savings"}, {Name: "Check"}];
  departmentsDataSource = this.vwDepartments.oDataSource;

  check: Boolean = false;

  userName: string = localStorage.getItem("userName");
  userName2:string = localStorage.getItem("userName2");
  userID: string;
  userEmail: string;
  userCompany: string;
  imageSrc = "./../../images/logo.jpg";
  signatureExists: number = 0;
  isSignaturePopupVisible: Boolean = false;
  isAvailSigPopupVisible: Boolean = false;
  myByteSignatureFile;
  signatureLable: string ="Add Signature";
  userFullName: string;

  signatureImg: string;
  @ViewChild(SignaturePad) signaturePad: SignaturePad;

  signaturePadOptions: Object = { 
    'minWidth': 2,
    'canvasWidth': 700,
    'canvasHeight': 300,
    'backgroundColor': 'white'
  };


  isPopupBankingDetailsVisible: Boolean = false;
  userDepartmentsDataSource;
  selectedRecords;


  userDepartment: UserDepartment = new UserDepartment();
  isPopupUserDepartmentVisible: Boolean = false;
  isLeaveButtonVisible: Boolean = false;
  isLeaveRepresentativeVisible: Boolean = false;
  isUploadUserManualVisible: Boolean = false;

  consolidatedFileRecord: ConsolidatedFileRecord = new ConsolidatedFileRecord();
  isConsolidatedFileVisible: Boolean = false;
  signatureEmailAddress: string;
  userpictureSignature = [];
  lblPictureSignatureAttachment: string = "";
  isPictureSignature: Boolean = false;

  isRefreshBrowserPopupVisible: Boolean = false;
  
  isupLoadPictureSignaturePopupVisible: Boolean = false;
  userId: number = 1;
  aspUserId: string = "588586d1-6587-49c8-bd79-28f7ae21ede0";
  @ViewChild('signaturePictureDocumentsInput') signaturePictureInputVariable: ElementRef;

  @ViewChild(MessageDialogComponent) messageDialog: MessageDialogComponent;
  @ViewChild(ProcessingDialogComponent) processingDialog: ProcessingDialogComponent;
  @ViewChild(ConfirmDialogComponent) confirmDialog: ConfirmDialogComponent;

  
  constructor(
    private customControllerService: CustomControllerService,
    protected http: HttpClient,
    private vwDepartments: vwDepartmentService,
    private vwUserProfile: vwUserProfileService,
    private vwUserDepartment: vwUserDepartmentService,
    private router: Router) 
    {
      locale('en-ZA');
      config({
        defaultCurrency: "ZAR"
      });

   }

  ngOnInit(){

  }


  getUserRoles(user: string){

  }


  uploadUserManualClick(){

  }


  
  ngAfterViewInit() {
    this.refreshWindowsUserProfile();
    this.refreshGrid();
    //this.signaturePad is now available
    this.signaturePad.set('minWidth', 2); 
    this.signaturePad.clear(); 
  }

  drawComplete() {
    //console.log(this.signaturePad.toDataURL());
  }

  drawStart() {
    console.log('begin drawing');
  }

  clearSignaturePad() {
    this.signaturePad.clear();
  }

  saveSignatureClick() {
   if(this.signaturePad.isEmpty() == true){
      this.messageDialog.show("Please sign for you to be able to save.", DialogMessageTypes.Information);
    }else{

      const base64Data = this.signaturePad.toDataURL();
      this.signatureImg = base64Data;
      this.userProfile.SignatureFileType = this.signatureImg;
      this.isSignaturePopupVisible = false;
      let receivedFile = base64Data.replace("data:image/png;base64," , "");
  
      const byteCharacters = atob(receivedFile);
      const byteNumbers = new Array(byteCharacters.length);
      for (let i = 0; i < byteCharacters.length; i++) {
          byteNumbers[i] = byteCharacters.charCodeAt(i);
      }
      const byteArray = new Uint8Array(byteNumbers);
    
      this.myByteSignatureFile  = new File([new Blob([byteArray], {type: 'image/png'})], "signature.png");
    }
  }

  signatureClick(){
    if(this.isPictureSignature === true){
      this.messageDialog.show("You can not preview. Your signature is managed by administrators because you have requested to upload an image signature", DialogMessageTypes.Information);
    }else{
      if(this.signatureExists == 0){
        this.isSignaturePopupVisible = true;
      }
      else{
        this.signatureImg = this.userProfile.SignatureFileType;
        this.isAvailSigPopupVisible = true;
      }
    }
  }

  CloseSignatureClick(){
    this.isSignaturePopupVisible = false;
  }

  changeSignatureClick(){
    this.isAvailSigPopupVisible = false;
    this.isSignaturePopupVisible = true;
  }

  changeCloseSignatureClick(){
    this.isAvailSigPopupVisible = false;
  }


 refreshWindowsUserProfile(){
  this.vwUserProfile.searchNonOdata("vwUserProfiles?$filter=UserId eq " + this.userId).subscribe(
    data => {
      if(data.value.length<1){
        this.userProfile = new UserProfile();
      }else{
        this.userProfile = data.value[0];
        [this.userProfile.FirstName, this.userProfile.LastName] = this.splitFullName( this.userProfile.FullName);
        if( this.userProfile.Signature !== null ){
          this.signatureExists = 1;
       }else{
         this.signatureExists = 0;
       }

      }
      this.processingDialog.hide();
    }, error => {
      this.processingDialog.hide();
      this.messageDialog.show("A system error was encountered while trying to get user details.", DialogMessageTypes.SystemError);
    }
  )
 }


 refreshBrowserClick(){
  window.location.reload();
  this.isRefreshBrowserPopupVisible = false;
 }


  splitFullName(fullName: string): [string, string] {
  const lastSpaceIndex = fullName.lastIndexOf(' ');
  if (lastSpaceIndex !== -1) {
      const firstName = fullName.slice(0, lastSpaceIndex);
      const lastName = fullName.slice(lastSpaceIndex + 1);
      return [firstName, lastName];
  } else {
      return [fullName, ''];
  }
}


  SaveProfileDetailsClick(){
    if (this.userProfile.FirstName == null){
      this.messageDialog.show("Please enter  name!", DialogMessageTypes.Information);
    }
    else if (this.userProfile.LastName == null){
      this.messageDialog.show("Please enter surname!", DialogMessageTypes.Information);
    }

    else if (this.userProfile.Email == null){
      this.messageDialog.show("Please enter email!", DialogMessageTypes.Information);
    }
    else if (this.userProfile.PhoneNumber == null){
      this.messageDialog.show("Please enter phone number!", DialogMessageTypes.Information);
    }
    else if ((this.myByteSignatureFile == null) && (this.userProfile.Signature ==null)){
      this.messageDialog.show("Please add your signature!", DialogMessageTypes.Information);
    }
    else{
      this.processingDialog.show();

      this.userProfile.IsUploadedPictureSignature = this.isPictureSignature;
      this.userProfile.UserId = this.userId;
      this.userProfile.AspNetUserId = this.aspUserId;

      this.userProfile.FullName = this.userProfile.FirstName + " "+ this.userProfile.LastName;
      this.customControllerService.utilitiesPost("UserProfiles/SaveEditUserProfile", this.userProfile).subscribe(
        data => {
          let response: ResponseObject = data;
          if (response.Status == ResponseStatus.Success){
            if(this.myByteSignatureFile != null){
              const frmData = new FormData();

              const url = this.isPictureSignature ? "/UploadImageSignature?x=0&userId=": "/UploadSignatureFromSignaturePad?userId=";

              frmData.append("fileUpload", this.myByteSignatureFile);
              this.customControllerService.utilitiesPost("UserProfiles" + url + data.EntityID, frmData).subscribe(
                  data => {
                    let response: ResponseObject = data;
                    if (response.Status == ResponseStatus.Success) {
                      this.messageDialog.show("Changes applied sucessfully!", DialogMessageTypes.Information);
                      this.refreshWindowsUserProfile();
                    } else {
                      this.processingDialog.hide();
                      this.messageDialog.show("A system error was encountered trying to save your supporting documents.", DialogMessageTypes.SystemError);
                    }
                  }
                )
            }else{
              this.messageDialog.show("Changes applied sucessfully!", DialogMessageTypes.Information);
              this.refreshWindowsUserProfile();
            }

          }else{
            this.messageDialog.show(response.StatusDescription, DialogMessageTypes.Information);
          }
            this.processingDialog.hide();
        },
        error => {
          this.messageDialog.show("A system error has occured while trying to save changes, Please contact Admin", DialogMessageTypes.SystemError);
          this.processingDialog.hide();
        }
    )
    }
  }



  refreshGrid(){  
    this.vwUserDepartment.searchNonOdata("vwUserDepartments?$filter=UserId eq " + this.userId).subscribe(
      data => {  
          this.userDepartmentsDataSource = data.value;
      }, error => {       
        this.messageDialog.show("A system error was encountered while trying to get user details.", DialogMessageTypes.SystemError);
      }
    )
  }

  createNewRecord(){

    if(this.userId === undefined){
      this.messageDialog.show("Please fill in General information and click save changes first.", DialogMessageTypes.Information);
    }else{
      //this.userDepartment = new UserDepartment();
      this.userDepartment.UserId = this.userId;
      this.isPopupUserDepartmentVisible = true;
    }
  }

  deleteButtonClick(){
    if (this.selectedRecords ==undefined || this.selectedRecords[0] ==undefined){
      this.messageDialog.show("Please select record you would like to delete", DialogMessageTypes.Information);
    }else{
      this.confirmDialog.show("Delete","Are you sure you want to delete this " + this.selectedRecords[0].DepartmentName + " department?");
    }
  }

  confirmClick(){
    this.confirmDialog.hide();
    this.processingDialog.show();
    this.customControllerService.utilitiesGet("UserDepartments/DeleteUserDepartment?ID=" +  this.selectedRecords[0].DepartmentId).subscribe(
      data => {
        let response: ResponseObject = data;
        if (response.Status == ResponseStatus.Success){
          this.messageDialog.show("Deleted sucessfully!", DialogMessageTypes.Information);
          this.refreshGrid();
        }else{
          this.messageDialog.show(response.StatusDescription, DialogMessageTypes.Information);
        }
          this.processingDialog.hide();
      },
      error => {
        this.messageDialog.show("A system error has occured while trying to save, Please contact Admin", DialogMessageTypes.SystemError);
        this.processingDialog.hide();
      }
   )

  }

  saveUserDepartmentClick(){
    if(this.userDepartment.UserId ==undefined){
      this.messageDialog.show("Please fill in General information and click save changes first.", DialogMessageTypes.Information);
    }else if(this.userDepartment.DepartmentId ==undefined){
      this.messageDialog.show("Select Department!", DialogMessageTypes.Information);
    } else{
      this.userDepartment.UserId  = this.userId;
      this.processingDialog.show();
      this.customControllerService.utilitiesPost("UserDepartments/SaveUserDepartment", this.userDepartment).subscribe(
        data => {
          let response: ResponseObject = data;
          if (response.Status == ResponseStatus.Success){
            this.messageDialog.show("Saved sucessfully!", DialogMessageTypes.Information);
            this.isPopupUserDepartmentVisible = false;
            this.refreshGrid();
          }else{
            this.messageDialog.show(response.StatusDescription, DialogMessageTypes.Information);
          }
            this.processingDialog.hide();
        },
        error => {
          this.messageDialog.show("A system error has occured while trying to save, Please contact Admin", DialogMessageTypes.SystemError);
          this.processingDialog.hide();
        }
     )
    }
  }

  CloseUserDepartmentClick(){
    this.isPopupUserDepartmentVisible = false;
  }

  onToolbarPreparing(e) {
    e.toolbarOptions.items.unshift({
      location: 'before',
      locateInMenu: 'auto',
      widget: 'dxButton',
      options: {
        hint: 'New',
        icon: 'plus',
        text: 'New',
        type: 'normal',
        onClick: this.createNewRecord.bind(this)
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
          onClick: this.deleteButtonClick.bind(this)
        }
      },
      {
        location: 'after',
        widget: 'dxButton',
        locateInMenu: 'auto',
        options: {
          hint: 'refresh',
          icon: 'refresh',
          width: 115,
          text: 'Refresh',
         onClick: this.refreshGrid.bind(this)
        }
      }
    );
  }


  updateColidatedFileClick(){
    this.consolidatedFileRecord = new ConsolidatedFileRecord();
    this.isConsolidatedFileVisible = true;
  }

  SaveConsolidatedFile(){
    this.processingDialog.show();
    this.customControllerService.utilitiesGet("Reports/SaveEditConsolidatedRequisition?requisitionID=" + this.consolidatedFileRecord.requisitionID
    + "&requestedBy=" + this.consolidatedFileRecord.requestedBy + "&requisitionTemplateCode=" + this.consolidatedFileRecord.requisitionTemplateCode + "&conn='conn'").subscribe(
      data => { 
        this.isConsolidatedFileVisible = false;
        this.processingDialog.hide();
        this.messageDialog.show("Successfully updated.", DialogMessageTypes.Information);
            
      },error =>{
        this.processingDialog.hide();
        this.messageDialog.show("A system error was encountered while updating the consolidated document.", DialogMessageTypes.SystemError);
      });
  }

  saveConsolidatedFileCloseClick(){
    this.isConsolidatedFileVisible = false;
  }

  //upload picture signature
  uploadNewPictureSignature(){
    this.signatureEmailAddress = undefined;
    this.userpictureSignature = [];
    this.lblPictureSignatureAttachment = "";
    this.isupLoadPictureSignaturePopupVisible = true;
  }

  usersignatureNameValueChange(e){
    this.signatureEmailAddress = e.value;
  }

  getPictureSignatureDocuments(e) {
    if (e.target.files.length > 0) {

      for (var i = 0; i < e.target.files.length; i++) {
        this.userpictureSignature.push(e.target.files[i]);
        this.lblPictureSignatureAttachment = e.target.files[i].name;
      }
    }
    this.signaturePictureInputVariable.nativeElement.value ='';
  }


  savePictureSignatureDocuments(){
    if(this.userpictureSignature.length == 0){
      this.messageDialog.show("Please upload the signature", DialogMessageTypes.Information);
    }
    else {
      this.processingDialog.show();
      for (var p = 0; p < this.userpictureSignature.length; p++) {
        const frmData = new FormData();
        frmData.append("fileUpload", this.userpictureSignature[p]);
        this.customControllerService.utilitiesPost("UserProfiles/UploadImageSignature?x=0&userId=" + this.userId, frmData).subscribe(
            data => {
              let response: ResponseObject = data;
              if (response.Status == ResponseStatus.Success) {
          
                  this.messageDialog.show("Updated sucessfully!", DialogMessageTypes.Information);
                  this.isupLoadPictureSignaturePopupVisible = false;
                  this.processingDialog.hide();
      
              } else {
                this.processingDialog.hide();
                this.messageDialog.show(response.StatusDescription, DialogMessageTypes.SystemError);
              }
            }
          )   
      }

    } 
  }
  
}


export class ConsolidatedFileRecord{
  requisitionID: number;
  requestedBy: string 
  requisitionTemplateCode: string;
  conn: string;

}



@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    DxFormModule,
    DxiItemModule,
    ProcessingDialogModule,
    MessageDialogModule,
    DxButtonModule,
    DxToolbarModule,
    DxLookupModule,
    DxPopupModule,
    DxScrollViewModule,
    SignaturePadModule,
    DxBoxModule,
    DxTabPanelModule,
    DxDataGridModule,
    ConfirmDialogModule,
    DxTextBoxModule,
    DxDropDownBoxModule,
    DxDateBoxModule
  ],
  declarations: [ProfileComponent,],
  exports: [ProfileComponent,]
})
export class ProfileModule { }
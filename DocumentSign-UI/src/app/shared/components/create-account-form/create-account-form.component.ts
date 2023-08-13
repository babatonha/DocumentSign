import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, NgModule, ViewChild } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { DxLoadPanelModule } from 'devextreme-angular';
import { DxFormModule } from 'devextreme-angular/ui/form';
import { DxLoadIndicatorModule } from 'devextreme-angular/ui/load-indicator';
import notify from 'devextreme/ui/notify';
import { ConfirmDialogModule } from 'src/app/common/confirm-dialog/confirm-dialog.component';
import { DialogMessageTypes } from 'src/app/common/enums/dialog-message-types';
import { MessageDialogComponent, MessageDialogModule } from 'src/app/common/message-dialog/message-dialog.component';
import { ProcessingDialogComponent, ProcessingDialogModule } from 'src/app/common/processing-dialog/processing-dialog.component';
import { Register } from 'src/app/models/register';
import { AuthService } from '../../services';
import { CustomControllerService } from '../../services/custom-controller-service';
import { UserProfile } from 'src/app/models/user-profile';
import { ResponseObject } from 'src/app/models/response-object';
import { ResponseStatus } from 'src/app/common/enums/response-statuses';


@Component({
  selector: 'app-create-account-form',
  templateUrl: './create-account-form.component.html',
  styleUrls: ['./create-account-form.component.scss']
})
export class CreateAccountFormComponent {
  loading = false;
  formData: any = {};

  @ViewChild(MessageDialogComponent) messageDialog: MessageDialogComponent;
  @ViewChild(ProcessingDialogComponent) processingDialog: ProcessingDialogComponent;
  loadingVisible: Boolean = false;

  registerUser: Register = new Register();
  registerResponse: Boolean = false;

  constructor(private authService: AuthService,
    private customControllerService: CustomControllerService,
     private router: Router) { }

  async onSubmit(e) {
    e.preventDefault();
    const { email, password } = this.formData;

    this.processingDialog.show();

    this.registerUser.Email= email;
    this.registerUser.ConfirmPassword = email;
    this.registerUser.Password = password;
    this.processingDialog.show();
    await this.customControllerService.utilitiesPost("Account/Register", this.registerUser).subscribe(
      data => {
        console.log(data);
        if(data.Succeeded == true){
            this.registerResponse = true;
            this.createProfile();
            this.router.navigate(['/login-form']);
          }else{
            this.registerResponse = false;
            this.messageDialog.show(data.Errors, DialogMessageTypes.Information);
          }

          this.processingDialog.hide();
      },
      error => {
        this.registerResponse = false;
        this.messageDialog.show("A system error has occured while trying to create an account, Please contact Admin", DialogMessageTypes.SystemError);
        this.processingDialog.hide();
      }
   )

  }

  createProfile(){
    var userProfile = new UserProfile;
    userProfile.Email = this.registerUser.Email;
    userProfile.IsUploadedPictureSignature =false;
    this.customControllerService.utilitiesPost("UserProfiles/SaveEditUserProfile", userProfile).subscribe(
      data => {
        let response: ResponseObject = data;
        if (response.Status == ResponseStatus.Success){

        }else{
          this.messageDialog.show(response.StatusDescription, DialogMessageTypes.Information);
        }
          this.processingDialog.hide();
      },
      error => {
        this.messageDialog.show("A system error has occured while trying to save user profile, Please contact Admin", DialogMessageTypes.SystemError);
        this.processingDialog.hide();
      }
    )
  }

  confirmPassword = (e: { value: string }) => {
    return e.value === this.formData.password;
  }
}
@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    DxFormModule,
    DxLoadIndicatorModule,
    HttpClientModule,
    ConfirmDialogModule,
    MessageDialogModule,
    DxLoadPanelModule,
    ProcessingDialogModule
  ],
  declarations: [ CreateAccountFormComponent ],
  exports: [ CreateAccountFormComponent ]
})
export class CreateAccountFormModule { }

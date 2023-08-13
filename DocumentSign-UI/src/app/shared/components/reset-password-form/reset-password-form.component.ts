import { CommonModule } from '@angular/common';
import { Component, NgModule, ViewChild } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { DxFormModule } from 'devextreme-angular/ui/form';
import { DxLoadIndicatorModule } from 'devextreme-angular/ui/load-indicator';
import notify from 'devextreme/ui/notify';
import { AuthService } from '../../services';
import { DialogMessageTypes } from 'src/app/common/enums/dialog-message-types';
import { CustomControllerService } from '../../services/custom-controller-service';
import { ProcessingDialogComponent, ProcessingDialogModule } from 'src/app/common/processing-dialog/processing-dialog.component';
import { MessageDialogComponent, MessageDialogModule } from 'src/app/common/message-dialog/message-dialog.component';
import { DxLoadPanelModule } from 'devextreme-angular';

const notificationText = 'We\'ve sent a link to reset your password. Check your inbox.';

@Component({
  selector: 'app-reset-password-form',
  templateUrl: './reset-password-form.component.html',
  styleUrls: ['./reset-password-form.component.scss']
})
export class ResetPasswordFormComponent {
  loading = false;
  formData: any = {};
  forgotPasswordModel: ForgotPasswordViewModel = new ForgotPasswordViewModel();
  @ViewChild(MessageDialogComponent) messageDialog: MessageDialogComponent;
  @ViewChild(ProcessingDialogComponent) processingDialog: ProcessingDialogComponent;

  constructor( private router: Router,private customControllerService: CustomControllerService) { }

  async onSubmit(e) {
    e.preventDefault();

    this.processingDialog.show();
    await this.customControllerService.utilitiesPost("Account/ForgotPassword", this.forgotPasswordModel).subscribe(
      data => {
          this.router.navigate(['/reset-password-with-code']);
          this.messageDialog.show(data.Errors, DialogMessageTypes.Information);
          this.processingDialog.hide();
      },
      error => {
        this.messageDialog.show("A system error has occured while trying to reset password, Please contact Admin", DialogMessageTypes.SystemError);
        this.processingDialog.hide();
      }
    )
  }
}
@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    DxFormModule,
    DxLoadIndicatorModule,
    MessageDialogModule,
    DxLoadPanelModule,
    ProcessingDialogModule
  ],
  declarations: [ResetPasswordFormComponent],
  exports: [ResetPasswordFormComponent]
})
export class ResetPasswordFormModule { }


export class ForgotPasswordViewModel{
  public  Email : string;
}
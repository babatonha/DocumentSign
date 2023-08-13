import { CommonModule } from '@angular/common';
import { Component, NgModule, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { DxFormModule, DxLoadIndicatorModule } from 'devextreme-angular';
import { DialogMessageTypes } from 'src/app/common/enums/dialog-message-types';
import { MessageDialogComponent, MessageDialogModule } from 'src/app/common/message-dialog/message-dialog.component';
import { ProcessingDialogComponent, ProcessingDialogModule } from 'src/app/common/processing-dialog/processing-dialog.component';
import { CustomControllerService } from '../../services/custom-controller-service';
import { AuthService } from '../../services';

@Component({
  selector: 'app-reset-password-with-code',
  templateUrl: './reset-password-with-code.component.html',
  styleUrls: ['./reset-password-with-code.component.scss']
})
export class ResetPasswordWithCodeComponent implements OnInit {
  loading = false;
  formData: any = {};
  recoveryCode: string;

  resetPasswordWithCode: ResetPasswordWithCode = new ResetPasswordWithCode();

  @ViewChild(MessageDialogComponent) messageDialog: MessageDialogComponent;
  @ViewChild(ProcessingDialogComponent) processingDialog: ProcessingDialogComponent;

  constructor(private authService: AuthService,
     private router: Router, 
     private customControllerService: CustomControllerService,
     private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.paramMap.subscribe(params => {
      this.recoveryCode = params.get('recoveryCode');
    });
  }

  async onSubmit(e) {
    e.preventDefault();

    const { code, email, password,confirmedPassword } = this.formData;

    this.resetPasswordWithCode.Email= email;
    this.resetPasswordWithCode.ConfirmPassword = confirmedPassword;
    this.resetPasswordWithCode.Password = password;
    this.resetPasswordWithCode.Code = code;
    this.processingDialog.show();

     this.customControllerService.utilitiesPost("Account/ResetPassword", this.resetPasswordWithCode).subscribe(
      data => {
        console.log(data);
        if(data ==null){
          this.messageDialog.show("Reset failed, Please contact administrator", DialogMessageTypes.Information);
        }else if(data.Succeeded == false){
          this.messageDialog.show("Reset failed, Please make sure your details are correct!", DialogMessageTypes.Information);
        } else if(data.Succeeded == true){
          this.messageDialog.show("Successfully reset", DialogMessageTypes.Information);
          this.router.navigate(['/login-form']);
        }
          this.processingDialog.hide();
      },
      error => {
        this.messageDialog.show("A system error has occured while trying to reset password, Please contact Admin", DialogMessageTypes.SystemError);
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
    ProcessingDialogModule,
    MessageDialogModule
  ],
  declarations: [ResetPasswordWithCodeComponent],
  exports: [ResetPasswordWithCodeComponent]
})
export class ResetPasswordWithCodeModule { }


export class ResetPasswordWithCode{
  public Code: string;
  public ConfirmPassword: string;
  public Email: string;
  public Password: string;
}
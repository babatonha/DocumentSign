import { CommonModule } from '@angular/common';
import { Component, NgModule, ViewChild } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { DxFormModule } from 'devextreme-angular/ui/form';
import { DxLoadIndicatorModule } from 'devextreme-angular/ui/load-indicator';
import notify from 'devextreme/ui/notify';
import { DialogMessageTypes } from 'src/app/common/enums/dialog-message-types';
import { MessageDialogComponent, MessageDialogModule } from 'src/app/common/message-dialog/message-dialog.component';
import { ProcessingDialogComponent, ProcessingDialogModule } from 'src/app/common/processing-dialog/processing-dialog.component';
import { LoginUser } from 'src/app/models/loginUser';
import { AuthService } from '../../services';
import { LoginService } from '../../services/login-service';
import { HttpClient } from '@angular/common/http';
import { UtilitiesService } from '../../services/utilitites-service';
import { vwUserProfileService } from 'src/app/services/vw-user-profile-service';


@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent {
  loading = false;
  loginUser: LoginUser = new LoginUser();

  @ViewChild(MessageDialogComponent) messageDialog: MessageDialogComponent;
  @ViewChild(ProcessingDialogComponent) processingDialog: ProcessingDialogComponent;

  constructor(private authService: AuthService, 
    private router: Router,
    protected httpClient: HttpClient,
    private vwUserProfile: vwUserProfileService,) { }

  async onSubmit(e) {
    e.preventDefault();
    this.processingDialog.show();
        this.authService.logIn(this.loginUser.username,this.loginUser.password).subscribe((data : any)=>{ 
          this.getUserProfile();
          this.processingDialog.hide();
        },
         error => {
        this.messageDialog.show("The user name or password is incorrect.", DialogMessageTypes.Information);
        this.processingDialog.hide();
      }
    )
    }

    getUserProfile(){
      this.vwUserProfile.searchNonOdata("vwUserProfiles?$filter=Email eq " + "'" + this.loginUser.username + "'").subscribe(
        data => {
          if(data.value.length>0){
            localStorage.setItem('UserId', data.value[0].UserId);
            localStorage.setItem('Email', data.value[0].Email);
            this.router.navigate(['/profile']);
          }
        }, error => {
          this.processingDialog.hide();
          this.messageDialog.show("A system error was encountered while trying to get user details.", DialogMessageTypes.SystemError);
        }
      )
    }

  onCreateAccountClick = () => {
    this.router.navigate(['/create-account']);
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
  declarations: [ LoginFormComponent ],
  exports: [ LoginFormComponent ]
})
export class LoginFormModule { }

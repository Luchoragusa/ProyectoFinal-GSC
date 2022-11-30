import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Person } from 'src/app/interfaces/person';
import { UserService } from 'src/app/services/user.service';
import { AlertDialogComponent } from '../shared/alert-dialog/alert-dialog.component';
import jwt_decode from 'jwt-decode';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form = new FormGroup({
    email: new FormControl('', {
      validators: [
        Validators.required,
        Validators.email,
        Validators.minLength(5),
      ],
    }),
    password: new FormControl('', {
      validators: [Validators.required, Validators.minLength(6)],
    }),
  });
  hide = true;

  constructor(
    private _router: Router,
    public dialog: MatDialog,
    private uS: UserService
  ) {}

  ngOnInit(): void {
  }

  onSubmit() {
    if (this.form.valid) {
      
      const user = {
        email: this.form.value.email,
        password: this.form.value.password,
      };

      this.uS.login(user).subscribe({
        next:(response: any) => {
          localStorage.setItem('token', response.token);
          const decodedToken = this.getDecodedAccessToken(response.token);
          localStorage.setItem('role', decodedToken.role);
          this._router.navigate(['/home']);
        },
        error:(err) => {
          this.dialog.open(AlertDialogComponent, {
            data: {
              title: 'Failed to login',
              message: 'Email or password incorrect'
            }
          })
        }
      });
    }
  }

  getDecodedAccessToken(token: string): any {
    try {
      return jwt_decode(token);
    } catch(Error) {
      return null;
    }
  }
}

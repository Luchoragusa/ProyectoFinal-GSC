import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Person } from 'src/app/interfaces/person';
import { UserService } from 'src/app/services/user.service';

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
          this._router.navigate(['/home']);
        },
        error:(err) => {
            alert('Invalid credentials');
            this.form.reset();
        }
      });
    }
  }
}

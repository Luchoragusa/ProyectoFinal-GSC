import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { CreateComponent } from '../../category/create/create.component';
import { LogoutComponent } from '../../logout/logout.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(private dialog: MatDialog) { }
  ngOnInit(): void {
  }

  createCategory(){
    this.dialog.open(CreateComponent)
  }

  logout(){
    this.dialog.open(LogoutComponent)
  }

  checkRole() {
    console.log(localStorage.getItem('role'));
    if (localStorage.getItem('role') == "Admin") {
      return true;
    } else {
      return false;
    }
  }
}

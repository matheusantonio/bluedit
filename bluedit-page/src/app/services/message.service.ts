import { Injectable } from '@angular/core';
import {MatSnackBar} from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private snackBar : MatSnackBar) { }

  showMessage(message : string) : void {
    this.snackBar.open(message, 'x', {
      duration: 3000,
      horizontalPosition: "right",
      verticalPosition: "top" })
  }

}

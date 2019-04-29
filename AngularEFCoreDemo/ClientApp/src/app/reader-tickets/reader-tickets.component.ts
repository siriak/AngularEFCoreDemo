import { Component } from '@angular/core';
import { ReaderTicket, ReaderTicketsClient, PeopleClient, Person } from '../clients';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'reader-tickets',
  templateUrl: './reader-tickets.component.html',
  styles: ['td { white-space: nowrap; }'],
})
export class ReaderTicketsComponent {
  public showDeleted: boolean;
  readerTickets: ReaderTicket[];
  people: Person[];

  constructor(
    private client: ReaderTicketsClient,
    private peopleClient: PeopleClient,
    private route: ActivatedRoute,
    private dialog: MatDialog,
    ) {
    this.get();
    this.peopleClient.getAll().subscribe(result => this.people = result);
  }

  update() {
    this.client.put(this.readerTickets).subscribe(
      () => {},
      () => {},
      () => this.get());
  }

  get() {
    this.route.params.subscribe(p => {
      const id = p['id'];
      if (id) {
        this.client.get(id).subscribe(
          result => this.readerTickets = [result],
          error => console.error(error));
      } else {
        this.client.getAll().subscribe(
          result => this.readerTickets = result,
          error => console.error(error));
      }
    });
  }

  getList() {
		if (this.readerTickets) {
		  return this.readerTickets.filter(readerTicket => this.showDeleted || !readerTicket.isDeleted);
		}
  }
  
  parseDate(dateString: string): Date {
    if (dateString) {
        return new Date(dateString);
    } else {
        return null;
    }
  }

  entryDeleted(isDeleted: boolean, readerTicket: ReaderTicket) {
		if (isDeleted) {
      const dialogRef = this.dialog.open(ConfirmDialogComponent);
      dialogRef.afterClosed().subscribe(res => {
        if (res !== 'yes') {
          readerTicket.isDeleted = false;
        }
      });
		}
  }
}

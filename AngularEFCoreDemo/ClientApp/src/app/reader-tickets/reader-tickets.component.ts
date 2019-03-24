import { Component } from '@angular/core';
import { ReaderTicket, ReaderTicketsClient, PeopleClient, Person } from '../clients';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'reader-tickets',
  templateUrl: './reader-tickets.component.html',
  styles: ['td { white-space: nowrap; }'],
})
export class ReaderTicketsComponent {
  public showDeleted: boolean;
  readerTickets: ReaderTicket[];
  people: Person[];

  constructor(private client: ReaderTicketsClient, private peopleClient: PeopleClient, private route: ActivatedRoute) {
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
}

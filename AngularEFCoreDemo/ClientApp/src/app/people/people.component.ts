import { Component } from '@angular/core';
import { Person, PeopleClient } from '../clients';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'people',
  templateUrl: './people.component.html',
  styles: ['td { white-space: nowrap; }'],
})
export class PeopleComponent {
  public showDeleted: boolean;
  people: Person[];

  constructor(private client: PeopleClient, private route: ActivatedRoute) {
    this.get();
  }

  update() {
    this.client.put(this.people).subscribe(
      () => {},
      () => {},
      () => this.get());
  }

  get() {
    this.route.params.subscribe(p => {
      const id = p['id'];
      if (id) {
        this.client.get(id).subscribe(
          result => this.people = [result],
          error => console.error(error));
      } else {
        this.client.getAll().subscribe(
          result => this.people = result,
          error => console.error(error));
      }
    });
  }

  getList() {
		if (this.people) {
		  return this.people.filter(person => this.showDeleted || !person.isDeleted);
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

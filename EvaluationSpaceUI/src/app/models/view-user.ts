import {Guid} from "guid-typescript";

export class ViewUser {
  id?: Guid;
  firstName: string;
  lastName: string;
  email: string;
  studentClassroom?: string;

  constructor(id: Guid, firstName: string, lastName: string, email: string, studentClassroom: string) {
    this.id = id;
    this.firstName = firstName;
    this.lastName = lastName;
    this.email = email;
    this.studentClassroom = studentClassroom;
  }
}

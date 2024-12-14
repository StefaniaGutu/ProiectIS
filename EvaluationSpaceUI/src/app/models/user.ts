import {Guid} from "guid-typescript";

export class User {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  idRole: number;
  classroomIds: Guid[];


  constructor(firstName: string, lastName: string, email: string, password: string, idRole: number, classroomIds: Guid[]) {
    this.firstName = firstName;
    this.lastName = lastName;
    this.email = email;
    this.password = password;
    this.idRole = idRole;
    this.classroomIds = classroomIds;
  }
}

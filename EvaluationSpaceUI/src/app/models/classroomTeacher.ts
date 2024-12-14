import {Guid} from "guid-typescript";
import {ViewUser} from "./view-user";

export class ClassroomTeacher {
  id : Guid;
  name : string;
  students : ViewUser[];

  constructor(id: Guid, name: string, students: ViewUser[]) {
    this.id = id;
    this.name = name;
    this.students = students;
  }
}

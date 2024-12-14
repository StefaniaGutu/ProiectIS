import {Guid} from "guid-typescript";

export class EditClassroom {
  id : Guid;
  name : string;
  studentIds: Guid[];

  constructor(id: Guid, name: string, studentIds: Guid[]) {
    this.id = id;
    this.name = name;
    this.studentIds = studentIds;
  }
}

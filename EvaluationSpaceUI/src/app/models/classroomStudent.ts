import { ViewUser } from "./view-user";

export class ClassroomStudent {
    name: string;
    colleagues: ViewUser[];
    teachers: ViewUser[];

    constructor(name: string, colleagues: ViewUser[], teachers: ViewUser[]) {
        this.name = name;
        this.colleagues = colleagues;
        this.teachers = teachers;
    }
}

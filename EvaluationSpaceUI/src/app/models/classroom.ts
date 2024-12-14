export class Classroom {
  disable?: boolean;
  group?: boolean;
  selected?: boolean;
  text: string;
  value: string

  constructor(text: string, value: string) {
    this.text = text;
    this.value = value;
  }
}

export class ZipDetails{
  constructor(zipFile: File, name: string, language: string) {
    this.zipFile = zipFile;
    this.name = name;
    this.language = language;
  }
  zipFile: File;
  name: string;
  language: string;
}

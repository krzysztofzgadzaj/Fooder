export class UserDto {
  constructor(data = {}) {
    this.id = data.id;
    this.login = data.login;
    this.name = data.name;
    this.lastName = data.lastName;
    this.mailAddress = data.mailAddress;
    this.token = data.token;
  }
}

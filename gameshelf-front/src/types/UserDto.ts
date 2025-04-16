export interface UserDto {
  id: string;
  externalId: string;
  email: string;
  pseudo: string;
  givenName: string;
  surname: string;
  role: "User" | "Admin";
}

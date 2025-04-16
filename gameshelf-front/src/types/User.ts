export interface User {
  id: string;
  externalId: string;
  email: string;
  pseudo: string;
  givenName: string;
  surname: string;
  role: "User" | "Admin";
}

export type UserViewModel = User & {
  isAdmin: boolean;
};

export const toUserViewModel = (user: User): UserViewModel => ({
  ...user,
  isAdmin: user.role === "Admin",
});

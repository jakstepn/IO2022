import { AccountType, GlobalStateInterface } from "./Types";

export const initialState: GlobalStateInterface = {
  isUserAuthenticated: false,
  loggedUser: '',
  token: '',
  accountType: AccountType.None
};
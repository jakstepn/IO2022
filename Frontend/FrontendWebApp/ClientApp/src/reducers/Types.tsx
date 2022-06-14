import { Dispatch } from 'react';

export interface GlobalStateInterface {
  isUserAuthenticated: boolean;
  loggedUser: string;
  token: string;
  accountType: AccountType;
  courierStatus: CourierStatus;
}

export enum CourierStatus {
  NotAvaible,
  AvaibleForDelivery,
  DuringDelivery
}

export enum AccountType {
  None,
  Client,
  Employee,
  Courier
}

export type ActionType = {
  type: string;
  payload?: any;
};

export type ContextType = {
  globalState: GlobalStateInterface;
  dispatch: Dispatch<ActionType>;
};
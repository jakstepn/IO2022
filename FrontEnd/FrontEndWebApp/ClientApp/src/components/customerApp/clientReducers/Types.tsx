import { Dispatch } from 'react';
import { Product } from '../classes/Product';

export interface GlobalStateInterface {
    ShopingCart: Product[];
}

export type ActionType = {
  type: string;
  payload?: any;
};

export type ContextType = {
  globalState: GlobalStateInterface;
  dispatch: Dispatch<ActionType>;
};
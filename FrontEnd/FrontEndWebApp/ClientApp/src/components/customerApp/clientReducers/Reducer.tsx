import { ActionType, } from './Types';
import { initialState } from './initial';

const Reducer = (state = initialState, action: ActionType): any => {
  switch (action.type) {
    case 'Add':
          return {
              ...state,
              ShopingCart: [...state.ShopingCart, action.payload],
          };
    case 'Clear':
          return {
              ...state,
              ShopingCart: [],
          };
    case 'Set':
          return {
              ...state,
              ShopingCart: action.payload,
          };
    case 'PURGE_STATE':
      return initialState;
    default:
      return state;
  }
};

export default Reducer;
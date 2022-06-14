import { Component, useContext } from 'react';
import { Route, Routes, Navigate } from "react-router-dom";
import NotFound from './NotFound';
import { globalContext } from '../reducers/GlobalStore';
import Login from '../components/Login';
import CourierHome from './courierApp/Home';
import CustomerHome from './customerApp/Home';
import CustomerBrowser from './customerApp/Browser/Main';
import CustomerShopingCart from './customerApp/ShopingCart/Main';
import CustomerDelivery from './customerApp/Delivery/Main';

export const AppRouter: React.FC = () => {
  const { globalState } = useContext(globalContext);

  return (
     <Routes>
            { !globalState.isUserAuthenticated && <Route path='*' element={<Login />}/> }
            <Route path='/login' element={<Login />} />

            <Route path='courier'>
              <Route path='home' element={<CourierHome />} />
            </Route>

            <Route path='customer'>
              <Route path='home' element={<CustomerHome />} />
              <Route path='browser' element={<CustomerBrowser />} />
              <Route path='delivery' element={<CustomerDelivery />} />
              <Route path='shopingcard' element={<CustomerShopingCart />} />
            </Route>
            
            <Route path='*' element={<NotFound />}/>
 </Routes>
  );
}

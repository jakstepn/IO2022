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
import ShopHome from './shopApp/Home';
import PendingOrders from './shopApp/order/PendingOrders';
import Orders from './shopApp/order/ShopOrders';
import Products from './shopApp/product/Products';
import CourierOrders from './courierApp/order/CourierOrders';

export const AppRouter: React.FC = () => {
  const { globalState } = useContext(globalContext);

  return (
      <Routes>
            { !globalState.isUserAuthenticated && <Route path='*' element={<Login />}/> }
            <Route path='/login' element={<Login />} />

            <Route path='courier'>
              <Route path='home' element={<CourierHome />} />
              <Route path='orders' element={<CourierOrders />} />
            </Route>

            <Route path='shop'>
              <Route path='home' element={<ShopHome />} />
              <Route path='orders/pending' element={<PendingOrders />} />
              <Route path='orders' element={<Orders />} />
              <Route path='products' element={<Products />} />
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

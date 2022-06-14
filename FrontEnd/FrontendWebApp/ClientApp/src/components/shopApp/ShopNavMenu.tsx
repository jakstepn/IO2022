import { Component, useContext, useState } from 'react';
import { NavLink } from 'reactstrap';
import { useLocation, Link } from 'react-router-dom';
import './../NavMenu.css';
import 'antd/dist/antd.css';
import { HomeOutlined, ShoppingCartOutlined, IdcardOutlined, BarcodeOutlined } from '@ant-design/icons';

import { Menu } from 'antd';
import { GlobalStore, globalContext } from '../../reducers/GlobalStore';


export function ShopNavMenu() {
    const { globalState, dispatch } = useContext(globalContext);
    const location = useLocation(); 
    const navigateTo_IfLoggedIn = (to : string) => {
        return globalState.isUserAuthenticated ? to : "/login"
    }
    const getSelectedKeyFromPath = () => {
        let path = location.pathname;
        if(path.includes('home'))       return ['Home'];
        if(path.includes('pending'))    return ['PendingOrders'];
        if(path.includes('orders'))     return ['Orders'];
        if(path.includes('products'))     return ['Products'];
        return ['Home'];
      }

    return (
        <Menu theme="dark" mode="horizontal" selectedKeys={getSelectedKeyFromPath()}> 
            <Menu.Item key="Orders" icon={<ShoppingCartOutlined />}><NavLink tag={Link} to={navigateTo_IfLoggedIn("/shop/orders")}>Orders</NavLink></Menu.Item>
            <Menu.Item key="PendingOrders" icon={<IdcardOutlined />}><NavLink tag={Link} to={navigateTo_IfLoggedIn("/shop/orders/pending")}>Pending orders</NavLink></Menu.Item>
            <Menu.Item key="Products" icon={<BarcodeOutlined />}><NavLink tag={Link} to={navigateTo_IfLoggedIn("/shop/products")}>Products</NavLink></Menu.Item>
        </Menu>
    )
}

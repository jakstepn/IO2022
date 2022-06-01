import { Component, useContext, useState } from 'react';
import { NavLink } from 'reactstrap';
import { useLocation, Link } from 'react-router-dom';
import './../NavMenu.css';
import 'antd/dist/antd.css';
import { HomeOutlined, SearchOutlined } from '@ant-design/icons';

import { Menu, } from 'antd';
import { GlobalStore, globalContext } from '../../reducers/GlobalStore';


export function CustomerNavMenu() {
    const { globalState, dispatch } = useContext(globalContext);
    const navigateTo_IfLoggedIn = (to : string) => {
        return globalState.isUserAuthenticated ? to : "/login"
    }



    return (
        <Menu mode={'horizontal' } > 
        <Menu.Item key="Home" icon={<HomeOutlined />}>
            <NavLink tag={Link} to={navigateTo_IfLoggedIn("/customer/home")}>CustomerHome</NavLink>
            
        </Menu.Item>

            <Menu.Item key="Browser" icon={<SearchOutlined />}>
                <NavLink tag={Link} to={navigateTo_IfLoggedIn("/customer/browser")}>CustomerBrowser</NavLink>
            </Menu.Item>

            <Menu.Item key="Delivery" icon={<SearchOutlined />}>
                <NavLink tag={Link} to={navigateTo_IfLoggedIn("/customer/delivery")}>CustomerDelivery</NavLink>
            </Menu.Item>

            <Menu.Item key="ShopingCard" icon={<SearchOutlined />}>
                <NavLink tag={Link} to={navigateTo_IfLoggedIn("/customer/shopingcard")}>CustomerShopingCard</NavLink>
            </Menu.Item>
        </Menu >
    )
}

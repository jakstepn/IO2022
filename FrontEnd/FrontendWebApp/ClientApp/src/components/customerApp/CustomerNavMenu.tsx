import { Component, useContext, useState } from 'react';
import { NavLink } from 'reactstrap';
import { useLocation, Link } from 'react-router-dom';
import './../NavMenu.css';
import 'antd/dist/antd.css';
import { HomeOutlined, SearchOutlined, CodeSandboxOutlined, ShoppingCartOutlined   } from '@ant-design/icons';

import { Menu, } from 'antd';
import { GlobalStore, globalContext } from '../../reducers/GlobalStore';


export function CustomerNavMenu() {
    const { globalState, dispatch } = useContext(globalContext);
    const navigateTo_IfLoggedIn = (to : string) => {
        return globalState.isUserAuthenticated ? to : "/login"
    }




    return (
        <Menu theme="dark" mode="horizontal"  > 
        <Menu.Item key="Home" icon={<HomeOutlined />}>
            <NavLink tag={Link} to={navigateTo_IfLoggedIn("/customer/home")}>Home</NavLink>
            
        </Menu.Item>

            <Menu.Item key="Browser" icon={<SearchOutlined />}>
                <NavLink tag={Link} to={navigateTo_IfLoggedIn("/customer/browser")}>Browser</NavLink>
            </Menu.Item>

            <Menu.Item key="Delivery" icon={<CodeSandboxOutlined />}>
                <NavLink tag={Link} to={navigateTo_IfLoggedIn("/customer/delivery")}>Delivery</NavLink>
            </Menu.Item>

            <Menu.Item key="ShopingCard" icon={<ShoppingCartOutlined />} >
                <NavLink tag={Link} to={navigateTo_IfLoggedIn("/customer/shopingcard")}>ShopingCart</NavLink>
            </Menu.Item>
        </Menu >
    )
}

import { Component, useContext, useState } from 'react';
import { NavLink } from 'reactstrap';
import { useLocation, Link } from 'react-router-dom';
import './../NavMenu.css';
import 'antd/dist/antd.css';
import { HomeOutlined } from '@ant-design/icons';

import { Menu } from 'antd';
import { GlobalStore, globalContext } from '../../reducers/GlobalStore';


export function CourierNavMenu() {
    const { globalState, dispatch } = useContext(globalContext);
    const navigateTo_IfLoggedIn = (to : string) => {
        return globalState.isUserAuthenticated ? to : "/login"
    }

    return (
        <Menu.Item key="Home" icon={<HomeOutlined />}><NavLink tag={Link} to={navigateTo_IfLoggedIn("/courier/home")}>CourierHome</NavLink></Menu.Item>
    )
}
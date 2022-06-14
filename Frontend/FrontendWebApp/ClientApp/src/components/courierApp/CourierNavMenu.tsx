import { Component, useContext, useState } from 'react';
import { NavLink } from 'reactstrap';
import { useLocation, Link } from 'react-router-dom';
import './../NavMenu.css';
import 'antd/dist/antd.css';
import { DingtalkOutlined, CarOutlined } from '@ant-design/icons';

import { Row, Col, Menu, Button } from 'antd';
import { GlobalStore, globalContext } from '../../reducers/GlobalStore';
import { CourierStatus } from '../../reducers/Types';


export function CourierNavMenu() {
    const { globalState, dispatch } = useContext(globalContext);
    const location = useLocation(); 
    const navigateTo_IfLoggedIn = (to : string) => {
        return globalState.isUserAuthenticated ? to : "/login"
    }

    const getSelectedKeys = () => {
        let selectedKeys : string[] = [];
        selectedKeys.push(getSelectedKeyFromPath()[0]);
        selectedKeys.push(getSelectedKeyFromStatus()[0]);

        return selectedKeys;
    }
    const getSelectedKeyFromPath = () => {
        let path = location.pathname;
        if(path.includes('home'))       return ['Home'];
        if(path.includes('orders'))     return ['Orders'];
        return ['Home'];
      }
    const getSelectedKeyFromStatus = () => {
        switch(globalState.courierStatus) {
            case CourierStatus.AvaibleForDelivery :
                return ['AvaibleForDelivery'];
            case CourierStatus.DuringDelivery :
                return ['DuringDelivery'];
            case CourierStatus.NotAvaible :
                return ['NotAvailable'];
        }
    }
    const statusToString = (status: CourierStatus) => {
        switch(status) {
            case CourierStatus.AvaibleForDelivery :
                return "Available for delivery";
            case CourierStatus.DuringDelivery :
                    return "During delivery";
            case CourierStatus.NotAvaible :
                    return "Not available";
            default :
                return "Unknown status"
        }
    }
    const changeStatus = (status : CourierStatus) => {
        dispatch({ type: 'SET_COURIER_STATUS', payload: status });
    }

    return (
        <Menu theme="dark" mode="horizontal" selectedKeys={getSelectedKeys()}> 
            <Menu.Item key="Orders" icon={<CarOutlined />}><NavLink tag={Link} to={navigateTo_IfLoggedIn("/courier/orders")}>Orders</NavLink></Menu.Item>

            <Menu.SubMenu key="Status" icon={<DingtalkOutlined />} title={"Status: " + statusToString(globalState.courierStatus) }  style={{ width: '250px' }}>
                <Menu.Item key="AvaibleForDelivery" onClick={() => changeStatus(CourierStatus.AvaibleForDelivery)}>{ statusToString(CourierStatus.AvaibleForDelivery) }</Menu.Item>
                <Menu.Item key="DuringDelivery" onClick={() => changeStatus(CourierStatus.DuringDelivery)}>{ statusToString(CourierStatus.DuringDelivery) }</Menu.Item>
                <Menu.Item key="NotAvailable" onClick={() => changeStatus(CourierStatus.NotAvaible)}>{ statusToString(CourierStatus.NotAvaible) }</Menu.Item>
            </Menu.SubMenu>
        </Menu>
    )
}
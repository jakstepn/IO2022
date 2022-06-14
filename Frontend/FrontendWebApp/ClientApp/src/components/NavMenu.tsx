import { Component, useContext, useState } from 'react';
import { NavLink } from 'reactstrap';
import { useLocation, Link } from 'react-router-dom';
import './NavMenu.css';
import 'antd/dist/antd.css';
import { UserOutlined, HomeOutlined } from '@ant-design/icons';

import { Avatar, Button, Layout, Menu, message } from 'antd';
import { GlobalStore, globalContext } from '../reducers/GlobalStore';
import { AccountType } from '../reducers/Types';
import { CourierNavMenu } from './courierApp/CourierNavMenu';
import { CustomerNavMenu } from './customerApp/CustomerNavMenu';
import { ShopNavMenu } from './shopApp/ShopNavMenu';
const { Header } = Layout;
const { SubMenu } = Menu;

export function NavMenu() {
  const [avatarClicked, setAvatarClicked] = useState(false);
  const location = useLocation(); 
  const { globalState, dispatch } = useContext(globalContext);

  const getSelectedKeyFromPath = () => {
    let path = location.pathname;
    if(path.includes('home')) return ['Home'];
    return ['Home'];
  }
  const navigateTo_IfLoggedIn = (to : string) => {
    return globalState.isUserAuthenticated ? to : "/login"
  }
  const avatarClickHandle = () => {
    setAvatarClicked(!avatarClicked);
  }
  const logout = () => {
    dispatch({ type: 'PURGE_STATE' });
    setAvatarClicked(false);
    message.success('Logged out succesfully!');
  }
  function generateMenu() {
    if(globalState.accountType == AccountType.None)
      return defaultMenu();
    if(globalState.accountType == AccountType.Courier)
      return CourierNavMenu();
    if (globalState.accountType == AccountType.Client)
      return CustomerNavMenu();
    if (globalState.accountType == AccountType.Employee)
      return ShopNavMenu();
  }
  function defaultMenu() {
    return <Menu.Item key="Home" icon={<HomeOutlined />}><NavLink tag={Link} to={navigateTo_IfLoggedIn("/login")}>Home</NavLink></Menu.Item>
  }

  return (
    <Header >
       { globalState.isUserAuthenticated &&  
        <div className="user-avatar">
          <Button icon={<UserOutlined />} shape="circle" onClick={avatarClickHandle}/>
        </div> }
     
      <Menu theme="dark" mode="horizontal" selectedKeys={getSelectedKeyFromPath()}>
        { globalState.isUserAuthenticated && avatarClicked && <Menu.Item key="logout" onClick={logout}><NavLink tag={Link} to="/login">Log out</NavLink></Menu.Item> }
        { generateMenu() }
      </Menu>
      
    </Header>
  );
}

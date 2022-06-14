import { Col, Row, Button, Modal } from 'antd';
import { useCallback, useContext, useEffect, useState } from 'react';
import {useLocation, useNavigate } from 'react-router-dom';
import { TeamOutlined, ShoppingCartOutlined } from '@ant-design/icons';
import { GlobalStore, globalContext } from '../../reducers/GlobalStore';
import { green } from '@ant-design/colors';

import {Typography} from "antd"

const { Title } = Typography;


export default function ShopHome() {
  const { globalState } = useContext(globalContext);
  const navigate = useNavigate();

  useEffect(() => {
    if(!globalState.isUserAuthenticated) {
      navigate('/login', {replace: true});
    }
    navigate('/shop/orders', {replace: true});
  }, [globalState.isUserAuthenticated, navigate]);

    return (
        <div  >
            <Row justify="space-around" align="middle" style={{ minHeight: "81vh" }}>
                This is shop module
            </Row>
      </div>
    );

}


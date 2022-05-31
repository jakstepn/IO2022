import { Col, Row, Button } from 'antd';
import { useCallback, useContext, useEffect, useState } from 'react';
import {useLocation, useNavigate } from 'react-router-dom';
import { TeamOutlined, ShoppingCartOutlined } from '@ant-design/icons';
import { GlobalStore, globalContext } from '../../reducers/GlobalStore';

import {Typography} from "antd"

const { Title } = Typography;


export default function CustomerHome() {
  const { globalState } = useContext(globalContext);
  const navigate = useNavigate();
  const location = useLocation(); 


  useEffect(() => {
    if(!globalState.isUserAuthenticated) {
      navigate('/login', {replace: true});
    }
  }, []);

    return (
      <div>
          <Row justify="space-around" align="middle" style={{minHeight: "81vh" }}>
              <Col flex="auto">
                  Customer Home
              </Col>
          </Row>
      </div>
    );

}

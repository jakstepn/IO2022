import { Col, Row, Button, Modal } from 'antd';
import { useCallback, useContext, useEffect, useState } from 'react';
import {useLocation, useNavigate } from 'react-router-dom';
import { LoadingOutlined, SmileOutlined } from '@ant-design/icons';
import { GlobalStore, globalContext } from '../../../reducers/GlobalStore';
import { green } from '@ant-design/colors';
import { Order } from './Order';

import {Typography} from "antd"
import { OrderJson } from '../classes/OrderJson';
import { exampleOrders } from '../exampleData/ExampleShopItem';

const { Title } = Typography;


export default function ShopOrders() {
  const { globalState } = useContext(globalContext);
  const navigate = useNavigate();
  const location = useLocation(); 
  const [loading, setLoading] = useState(false);
  const [dataLoaded, setDataLoaded] = useState(false);
  const [_pageSize, setPageSize] = useState(5);
  const [totalPages, setTotalPages] = useState<number>(1);
  const [orders, setOrders] = useState<OrderJson[]>();

  function fetchData(_pageNumber : number) {
    setLoading(true);
    setOrders(exampleOrders);
    setLoading(false);
    setDataLoaded(true);
  }
  function updateStatus(order : OrderJson, _status : string) {
    console.log("Updated s: " + _status);
    let updatedOrders = orders?.filter(o => o.orderId !== order.orderId);
    let updatedOrder = {
        ...order,
        orderStatus: _status
    }
    updatedOrders?.push(updatedOrder);
    setOrders(updatedOrders);
  }

  useEffect(() => {
    if(!globalState.isUserAuthenticated) {
        navigate('/login', {replace: true});
      }
      
    fetchData(1);
  }, []);

    return (
        <div  >
           <Row style={{ marginTop: 50 }}>
                <Col flex="auto">
                    <div className="site-layout-content">
                        <Title>Orders</Title>
                        { !loading ? 
                            (dataLoaded ? 
                                orders?.sort((a, b) => a.orderId.localeCompare(b.orderId)).map((item: OrderJson) => (
                                    <Order order={item} statusUpdateHandler={updateStatus}/>
                                ))
                                :
                                <Row align="middle" justify="center" style={{ marginTop: 50 }}>
                                    <Title level={5} type="secondary">No orders for now <SmileOutlined /></Title>
                                </Row>
                            ) : 
                            <Col flex="auto">
                                <Row align="middle" justify="center">
                                    <LoadingOutlined style={{ fontSize: '70px' }}/>
                                </Row>
                            </Col>
                        }
                    </div> 
                    <br />        
                </Col>
            </Row>
      </div>
    );

}


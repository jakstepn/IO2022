import { Col, Row, Button, Modal, Divider } from 'antd';
import { useCallback, useContext, useEffect, useState } from 'react';
import {useLocation, useNavigate } from 'react-router-dom';
import { LoadingOutlined, SmileOutlined } from '@ant-design/icons';
import { GlobalStore, globalContext } from '../../../reducers/GlobalStore';
import { green } from '@ant-design/colors';
import { Order } from './Order';

import {Typography} from "antd"
import { OrderJson } from '../classes/OrderJson';
import { exampleOrders } from '../exampleData/ExampleCourierItem';
import { exampleCurrentOrder } from '../exampleData/ExampleCourierItem';
import { CourierStatus } from '../../../reducers/Types';

const { Title, Text } = Typography;


export default function CourierOrders() {
  const { globalState, dispatch } = useContext(globalContext);
  const navigate = useNavigate();
  const location = useLocation(); 
  const [loading, setLoading] = useState(false);
  const [dataLoaded, setDataLoaded] = useState(false);
  const [_pageSize, setPageSize] = useState(5);
  const [totalPages, setTotalPages] = useState<number>(1);
  const [orders, setOrders] = useState<OrderJson[]>();
  const [currentOrder, setCurrentOrder] = useState<OrderJson>();

  function fetchData(_pageNumber : number) {
    setLoading(true);
    setOrders(exampleOrders);
    setCurrentOrder(exampleCurrentOrder);
    setLoading(false);
    setDataLoaded(true);
  }
  function updateCurrentStatus(order : OrderJson, _status : string) {
    if(currentOrder !== undefined) {
        let updatedOrder = {
            ...order,
            orderStatus: _status
        }
        if(updatedOrder.orderStatus === "Delivered") {
            setOrders(orders !== undefined ? [...orders, updatedOrder] : [updatedOrder]);
            setCurrentOrder(undefined);
            dispatch({ type: 'SET_COURIER_STATUS', payload: CourierStatus.AvaibleForDelivery });
        }
        else  
            setCurrentOrder(updatedOrder);
    }
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
                        <Divider />

                        <Title level={4}>Current order</Title>
                        { !loading && 
                            dataLoaded &&
                                currentOrder !== undefined ?
                                    <Order order={currentOrder} statusUpdateHandler={updateCurrentStatus}/>
                                    :
                                    <Text>No orders for now <SmileOutlined /></Text>
                        }
                        
                        <Divider />

                        <Title level={4}>All orders</Title>
                        { !loading ? 
                            (dataLoaded ? 
                                orders?.map((item: OrderJson) => (
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


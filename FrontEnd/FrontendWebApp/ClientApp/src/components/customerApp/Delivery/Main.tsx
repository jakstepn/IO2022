import { Col, Row, Button, Card } from 'antd';
import { useCallback, useContext, useEffect, useState } from 'react';
import {useLocation, useNavigate } from 'react-router-dom';
import { TeamOutlined, ShoppingCartOutlined } from '@ant-design/icons';
import { GlobalStore, globalContext } from '../../../reducers/GlobalStore';
import { Product } from "../classes/Product";
import { FilterOutlined } from '@ant-design/icons';
import { Pagination, Typography } from "antd"
import DeliverysInfoItem from "./DeliveryInfoListItem";
import { exampleDeliveryInfos } from "../exampleData/ExampleItem";
import { Delivery } from '../classes/Delivery';

const { Title } = Typography;


export default function CustomerDelivery() {
  const { globalState } = useContext(globalContext);
  const navigate = useNavigate();
    const location = useLocation(); 
    const [deliverysInfo, setDeliverysInfo] = useState(exampleDeliveryInfos);
    

  useEffect(() => {
    if(!globalState.isUserAuthenticated) {
      navigate('/login', {replace: true});
    }
  }, []);

    return (
        <div>

            <Row color="#8c8c8c" >

                <Col flex="auto">
                    <div className="site-layout-content">
                        {deliverysInfo.map((item: Delivery) => (
                            <DeliverysInfoItem DeliveryInfo={item}  />)
                        )}
                    </div>
                </Col>
            </Row>
        </div>
    );

}


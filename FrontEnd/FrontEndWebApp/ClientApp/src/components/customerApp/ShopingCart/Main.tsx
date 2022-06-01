import { Col, Row, Button, Card } from 'antd';
import { useCallback, useContext, useEffect, useState } from 'react';
import {useLocation, useNavigate } from 'react-router-dom';
import { TeamOutlined, ShoppingCartOutlined } from '@ant-design/icons';
import { GlobalStore, globalContext } from '../../../reducers/GlobalStore';
import { Product } from "../classes/Product";
import { FilterOutlined } from '@ant-design/icons';
import { Pagination, Typography } from "antd"
import ProductListItem from "../Browser/ProductListItem";
import { exampleDeliveryInfos } from "../exampleData/ExampleItem";

const { Title } = Typography;


export default function CustomerShopingCart() {
  const { globalState } = useContext(globalContext);
  const navigate = useNavigate();
    const location = useLocation(); 
    const [products, setProducts] = useState(exampleDeliveryInfos);
    

  useEffect(() => {
    if(!globalState.isUserAuthenticated) {
      navigate('/login', {replace: true});
    }
  }, []);

    return (
        <div>
            <Row justify="space-around" align="middle" style={{ minHeight: "81vh" }}>
                <Col flex="auto">
                    Customer ShopingCard
                </Col>
            </Row>
        </div>
    );

}

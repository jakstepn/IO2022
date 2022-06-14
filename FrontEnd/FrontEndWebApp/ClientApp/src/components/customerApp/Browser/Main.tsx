import { Col, Row, Button, Card } from 'antd';
import { useCallback, useContext, useEffect, useState } from 'react';
import {useLocation, useNavigate } from 'react-router-dom';
import { TeamOutlined, ShoppingCartOutlined } from '@ant-design/icons';
import { GlobalStore, globalContext } from '../../../reducers/GlobalStore';
import { Product } from "../classes/Product";
import { FilterOutlined } from '@ant-design/icons';
import { Pagination, Typography } from "antd"
import ProductListItem from "./ProductListItem";
import { exampleProducts } from "../exampleData/ExampleItem";

const { Title } = Typography;


export default function CustomerBrowser() {
  const { globalState } = useContext(globalContext);
  const navigate = useNavigate();
    const location = useLocation(); 
    const [products, setProducts] = useState(exampleProducts);
    

  useEffect(() => {
    if(!globalState.isUserAuthenticated) {
      navigate('/login', {replace: true});
    }
  }, []);

    return (
        <div>
            <Row style={{ marginTop: 50 }}>
                <Col flex="400px">
                    <Card>
                        <Title level={2}><FilterOutlined /> Filter results</Title>
                       {/* <BookListFilter filterResultsHandler={filterResultsHandler} />*/}
                    </Card>
                </Col>

                <Col flex="20px" />

                <Col flex="auto">
                    <div className="site-layout-content">
                        {products.map((item: Product) => (
                            <ProductListItem product={item}  />)
                        )}
                    </div>
                    <br />
                    <Pagination />
                </Col>
            </Row>
        </div>
    );

}

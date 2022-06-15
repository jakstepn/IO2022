import { Col, Row, Button, Card, Input, Form, Space, InputNumber } from 'antd';
import React, { EventHandler, useCallback, useContext, useEffect, useState } from 'react';
import {useLocation, useNavigate } from 'react-router-dom';
import { TeamOutlined, ShoppingCartOutlined } from '@ant-design/icons';
import { GlobalStore, globalContext } from '../../../reducers/GlobalStore';
import { Product } from "../classes/Product";
import { FilterOutlined } from '@ant-design/icons';
import { Pagination, Typography } from "antd"
import ProductListItem from "./ProductListItem";
import { exampleProducts } from "../exampleData/ExampleItem";
import { Label } from 'reactstrap';
import { number } from 'yup/lib/locale';


const { Title } = Typography;


export default function CustomerBrowser() {
  const { globalState } = useContext(globalContext);
  const navigate = useNavigate();
    const location = useLocation(); 
    const [products, setProducts] = useState(exampleProducts);
    const [productsToShow, setProductsToShow] = useState(exampleProducts);
    const [category, setCategory] = useState("");
    const maxValue = 99999999;
    const [max, setMax] = useState(maxValue);
    const [mini, setMini] = useState(0);


    const changeCategory = (e: any) => {
        
        setCategory(e.target.value.toLowerCase());
    };

    const changeMax = (value: any) => {
        console.log(value);
        var tmp = value;
        if (tmp == '')
            setMax(maxValue);
        else
            setMax( tmp);
    };

    const changeMini = (value: any) => {
        var tmp = value;
        if (tmp == '')
            setMini(0);
        else
            setMini(tmp);
    };

    const scherch = () => {
        var toShow = products.filter(item => item.category.toLowerCase().includes(category) &&
            item.price >= mini &&
            item.price <= max);
        setProductsToShow(toShow);
    }

  useEffect(() => {
    if(!globalState.isUserAuthenticated) {
      navigate('/login', {replace: true});
    }
  }, []);

    return (
        <div >
            <Row style={{ marginTop: 50 }}>
                <Col flex="400px">
                    <Card>
                        <Title level={2}><FilterOutlined /> Filter results</Title>
                        
                        <Row >
                            <Label> Category </Label>
                            
                            <Input type="text " onInput={changeCategory} />
                        </Row>
                        <br/>
                        <Row >
                            <Label> Price: </Label>
                            <Input.Group>
                                <Space>
                                    <InputNumber placeholder="min" onInput={changeMini} max={max} min={0} />
                                    <InputNumber placeholder="max" onInput={changeMax} min={mini }/>
                                 
                                </Space>

                            </Input.Group>
                        </Row>
                        <br/>
                        <Row justify="end">
                            <Button type="primary" onClick={scherch}> Scherch </Button>
                            </Row>
                        
                    </Card>
                </Col>

                <Col flex="20px" />

                <Col flex="auto">
                    <div className="site-layout-content">
                        {productsToShow.map((item: Product) => (
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

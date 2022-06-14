import { Col, Row, Button, Card, Modal, Input, Radio, Space, InputNumber } from 'antd';
import type { RadioChangeEvent } from 'antd';
import { useCallback, useContext, useEffect, useState } from 'react';
import {useLocation, useNavigate } from 'react-router-dom';
import { TeamOutlined, ShoppingCartOutlined } from '@ant-design/icons';
import { GlobalStore, globalContext } from '../../../reducers/GlobalStore';
import { Pagination, Typography } from "antd"
import { exampleProducts } from "../exampleData/ExampleItem";
import { Product } from "../classes/Product";
import ProductListItem from "./ProductListItem";
import { ModalFooter } from 'reactstrap';

const { Title } = Typography;


export default function CustomerShopingCart() {
  const { globalState } = useContext(globalContext);
  const navigate = useNavigate();
    const [products, setProducts] = useState(exampleProducts);
    const [isModalVisible, setIsModalVisible] = useState(false);
    const { TextArea } = Input;

    const showModal = () => {
        setIsModalVisible(true);
    };

    const handleOk = () => {
        setIsModalVisible(false);
    };

    const handleCancel = () => {
        setIsModalVisible(false);
    };


  useEffect(() => {
    if(!globalState.isUserAuthenticated) {
      navigate('/login', {replace: true});
    }
  }, []);

    return (
        <div>
            <Row justify="space-around" align="middle" >
               
                <Col flex="auto">
                    <div className="site-layout-content">
                        {products.map((item: Product) => (
                            <ProductListItem product={item} />)
                        )}
                    </div>
                    </Col>
                
               
            </Row>
            <Row justify='end'>
                <div >
                    <br />
                    <Row> <p style={{fontSize:20} }> <b> Sum: </b> </p></Row>
                    <Row> <Button type="primary" style={{ width: 100, fontSize: 20, height: 40 }} onClick={showModal }> Buy</Button> </Row>
                </div>
            </Row>

            <Modal width={800} title="Products" visible={isModalVisible} onOk={handleOk} onCancel={handleCancel} okText="Buy"
                footer={[
                    <Row >
                        <Col>
                            <p> Sum: 999</p>
                        </Col>
                        <Col >
                        <Button 
                            key="Buy"
                            type="primary"
                            onClick={handleOk}
                            >
                            Buy
                            </Button>
                    </Col>
                    </Row>
                ]}
            >
                <Card title="Addres">
                    <Row justify='space-between'>
                        <Col title="City" flex={3 }>
                            <Input placeholder="city" required />
                        </Col>
                        <Col title="Post Code" flex={1}>
                            <Space>
                                <InputNumber placeholder=" 00" type="number" max={99} required min={0} 
                                    formatter={ (value) => {
                                        if (value == undefined)
                                            return "";
                                        if (value >= 10)
                                            return `${value}`;
                                    else
                                            return `${0}${value}`;
                                    }
                                    }
                                />
                                <InputNumber placeholder=" 000" type="number" max={999} required min={0}  />
                            </Space>
                        </Col>
                    </Row>
                    <Row title="Street" >
                        <Col flex={3}>
                            <Input placeholder="street" />
                        </Col>
                        <Col flex={1}>
                            <Space>
                                <InputNumber placeholder=" street nr."  required />
                                <InputNumber placeholder=" apart nr."  />
                            </Space>
                        </Col>
                    </Row>
                </Card>
                <Card title="Delivery Time">
                    <Input type="date" /> 
                    <Input type="time"/>
                </Card>
                <Card title="Payment Option">
                    <Radio.Group>
                        <Radio value={1}>Cash</Radio>
                        <Radio value={2}>Card</Radio>
                        <Radio value={3}>Card in advance</Radio>
                    </Radio.Group>
                </Card>
                <Card title="Additional info">
                    <TextArea rows={4} />
                </Card>
                
            </Modal>
            
        </div>


    );

}

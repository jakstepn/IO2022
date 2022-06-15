import { Col, Row, Button, Card, Modal, Input, Radio, Space, InputNumber, Form, Divider, message } from 'antd';
import type { RadioChangeEvent } from 'antd';
import { useCallback, useContext, useEffect, useState } from 'react';
import {useLocation, useNavigate } from 'react-router-dom';
import { TeamOutlined, ShoppingCartOutlined } from '@ant-design/icons';
import { GlobalStore, globalContext } from '../../../reducers/GlobalStore';
import { Pagination, Typography } from "antd"
import { exampleProducts } from "../exampleData/ExampleItem";
import { Product } from "../classes/Product";
import ProductListItem from "./ProductListItem";
import { Label } from 'reactstrap';
import { parse } from 'path';
import { valueType } from 'antd/lib/statistic/utils';
import "../Global/ShoppingCartContend";
import { stat } from 'fs';
import { useWindowDimensions } from 'react-native';

const { Title } = Typography;
const layout = {
    labelCol: { span: 8 },
    wrapperCol: { span: 16 },
};
const tailLayout = {
    wrapperCol: { offset: 8, span: 16 },
};


export default function CustomerShopingCart() {
  const { globalState } = useContext(globalContext);
  const navigate = useNavigate();
    const [state, setState] = useState(globalThis.Cart);
    const [isModalVisible, setIsModalVisible] = useState(false);
    const { TextArea } = Input;
    const [form] = Form.useForm();
    const [refresh, setRefresh] = useState(false)


    let callback = () => {
        setRefresh(true);
        setRefresh(false);
        setState(globalThis.Cart);
        console.log(state);
        // do something with value in parent component, like save to state
    }

    const showModal = () => {
        setIsModalVisible(true);
        console.log(globalThis.Cart);
    };

    const handleOk = () => {
        setIsModalVisible(false);
        globalThis.RemoveAll(); callback(); message.success('Order has been made');
    };

    const handleCancel = () => {
        setIsModalVisible(false);
    };

    const formater = (value: any) => {
        console.log(value);
        if (value == undefined)
            return "";
        if (value >= 10)
            return `${value}`;
        else
            return `${0}${value}`;
    }

    const parser = (value: any, cut: number) => {
        console.log(value.length + " parse");
        if (value == undefined || value.length<=0)
            return "";
        return value.substring(value.length - cut);
    }

    const formater2 = (value: any) => {
        if (value == undefined)
            return "";
        if (value >= 100)
            return `${value}`;
        else if( value >=10)
            return `${0}${value}`;
        else
            return `${0}${0}${value}`;
    }

  useEffect(() => {
    if(!globalState.isUserAuthenticated) {
      navigate('/login', {replace: true});
    }
  }, []);


    

    return (
        <div >

            {globalThis.Cart.Products.length > 0 &&
            <>
                <Row justify="space-around" align="middle" >

                    <Col flex="auto">
                        <div className="site-layout-content">
                            {state.Products.map((item: Product) => (
                                <ProductListItem product={item} refresh={callback }/>)
                            )}
                        </div>
                    </Col>


                </Row>
                <Row justify='end'>
                    <div >
                        <br />
                        <Row> <p style={{ fontSize: 20 }}> <b> Sum: {state.Sum.toFixed(2)}</b> </p></Row>
                        <Row> <Button type="primary" style={{ width: 100, fontSize: 20, height: 40 }} onClick={showModal}> Buy</Button> </Row>
                    </div>
                </Row>
                </>}

            {globalThis.Cart.Products.length <= 0 &&
                <Row justify='center'>
                    <p style={{ fontSize: 30 }}> <b>Your shopping cart is empty </b></p>
                </Row>
            }
            
            <Modal  width={700} title="Products" visible={isModalVisible} onOk={handleOk} onCancel={handleCancel} okText="Buy"
                footer={ []}
            >
                <Form {...layout} form={form} name="control-hooks" onFinish={handleOk} >

                <Card title="Addres">
                    <Row justify='space-between'>
                            <Col title="City" flex={3}>
                               
                                <Label> <b> City </b> </Label>
                                <br />
                            <Input placeholder="city" required />
                        </Col>
                            <Col title="Post Code" flex={1}>
                                
                                <Label> <b> Post Code </b> </Label>
                                <br />
                            <Space>
                                <InputNumber placeholder=" 00" type="number"  required
                                        formatter={(value) => formater(value)} parser={value => parser(value, 2)}
                                />
                                    <InputNumber placeholder=" 000" type="number" required
                                        formatter={(value) => formater2(value)} parser={value => parser(value, 3)} />
                            </Space>
                        </Col>
                    </Row>
                        <Row title="Street" >
                            
                            <Col flex={3}>
                                <br />
                                <Label > <b> Street </b> </Label>
                                <br />
                            <Input placeholder="street" required />
                        </Col>
                            <Col flex={1}>
                                <br />
                                <Label>  </Label>
                                <br />
                            <Space>
                                <InputNumber placeholder=" street nr."  required />
                                    <InputNumber placeholder=" apart nr."   />
                            </Space>
                        </Col>
                    </Row>
                </Card>
                    <Card title="Delivery Time">
                        <Space>
                        <Input type="date" required /> 
                            <Input type="time" required />
                        </Space>
                </Card>
                    <Card title="Payment Option">
                        <Form.Item
                            name="radio-button"
                            rules={[{ required: true, message: 'Please pick an payment option!' }]}
                        >
                  <Radio.Group >
                        <Radio value={1}>Cash</Radio>
                        <Radio value={2}>Card</Radio>
                        <Radio value={3}>Card in advance</Radio>
                            </Radio.Group>
                  </Form.Item>
                </Card>
                <Card title="Additional info">
                    <TextArea rows={4} />
                
                    </Card>
                    <Divider/>
                    <Form.Item {...tailLayout} >
                        <Row  justify="end">
                        <Button 
                                key="Buy"
                                type="primary"
                                htmlType="submit"
                        >
                            Buy
                            </Button>
                            </Row>
                    </Form.Item >
            
            </Form>
            </Modal>
            
        </div>


    );

}

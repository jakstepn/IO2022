import { Col, Row, Button, Modal, Card, Divider, Table, AutoComplete, Select, Form, Input, InputNumber } from 'antd';
import { useCallback, useContext, useEffect, useState } from 'react';
import {useLocation, useNavigate } from 'react-router-dom';
import { EnvironmentOutlined, RightOutlined } from '@ant-design/icons';
import { GlobalStore, globalContext } from '../../../reducers/GlobalStore';
import { green } from '@ant-design/colors';

import {Typography} from "antd"
import { OrderJson } from '../classes/OrderJson';
import { exampleOrders } from '../exampleData/ExampleShopItem';
import { useWindowDimensions } from 'react-native';
import { ColumnsType } from 'antd/lib/table';
import { OrderItem } from '../classes/OrderItem';
import { OrderStatus } from '../classes/OrderStatus';
import { Product } from '../classes/Product';

const { Title, Text } = Typography;
const { Option } = Select;

interface Props {
    visible   : boolean,
    onCancel  : Function,
    onConfirm : Function
}

export const AddProduct: React.FC<Props> = (props: Props) => {
    const { height, width } = useWindowDimensions();
    const [form] = Form.useForm();
    let product : Product;
    
    const onCreate = (values: Product) => {
        console.log(values);
        props.onConfirm();
    }

    return (
        <Modal title="Add a new product"
            centered
            visible={props.visible}
            okText="Save changes"
            onCancel={(e) => props.onCancel()}
            width={width/2}
            onOk={() => {
                form
                  .validateFields()
                  .then(values => {
                    form.resetFields();
                    onCreate(values);
                  })
                  .catch(info => {
                    console.log('Validate Failed:', info);
                  });
              }}
            >
            
            <Form
                form={form}
                name="form"
                labelCol={{ span: 8 }}
                wrapperCol={{ span: 16 }}
                initialValues={{ remember: true }}
                autoComplete="off"
                >
                <Form.Item
                    label="Product name"
                    name="productName"
                    rules={[{ required: true, message: 'Please input the name of the product!' }]}
                >
                    <Input />
                </Form.Item>

                <Form.Item
                    label="Quantity"
                    name="quantity"
                    rules={[{ required: true, message: 'Please input the quantity in stock!' }]}
                >
                    <Input />
                </Form.Item>

                <Form.Item
                    label="Price"
                    name="price"
                    rules={[{ required: true, message: 'Please input the price!' }]}
                >
                    <InputNumber  />
                </Form.Item>

                <Form.Item label="Currency"
                    rules={[{ required: true, message: 'Please input the currency!' }]}
                >
                    <Select>
                        <Select.Option value="PLN">Polish zloty</Select.Option>
                        <Select.Option value="EUR">Euro</Select.Option>
                        <Select.Option value="USD">US dolars</Select.Option>
                    </Select>
                </Form.Item>
            </Form>
        </Modal>
    );
}


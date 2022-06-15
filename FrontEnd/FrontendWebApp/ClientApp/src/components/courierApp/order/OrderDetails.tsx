import { Col, Row, Button, Modal, Card, Divider, Table, AutoComplete, Select } from 'antd';
import { useCallback, useContext, useEffect, useState } from 'react';
import {useLocation, useNavigate } from 'react-router-dom';
import { EnvironmentOutlined, RightOutlined } from '@ant-design/icons';
import { GlobalStore, globalContext } from '../../../reducers/GlobalStore';
import { green } from '@ant-design/colors';

import {Typography} from "antd"
import { OrderJson } from '../classes/OrderJson';
import { useWindowDimensions } from 'react-native';
import { ColumnsType } from 'antd/lib/table';
import { OrderItem } from '../classes/OrderItem';
import { OrderStatus } from '../classes/OrderStatus';

const { Title, Text } = Typography;
const { Option } = Select;

interface Props {
    order     : OrderJson,
    visible   : boolean,
    onCancel  : Function,
    onConfirm : Function
}

export const OrderDetails: React.FC<Props> = (props: Props) => {
    const { height, width } = useWindowDimensions();
    const [ orderStatus, setOrderStatus ] = useState(props.order.orderStatus);
    
    const updateStatusHandle = (status: string) => {
        setOrderStatus(status);
    }
    function orderStatusToString(status : string) {
        if(status.includes("RejectedByShop")) return "Rejected by shop";
        if(status.includes("InProgress")) return "In progress";
        if(status.includes("ReadyForDelivery")) return "Ready for delivery";
        if(status.includes("WaitingForPickUp")) return "Waiting for pick-up";
        if(status.includes("PickedUpByCourier")) return "Picked up by courier";

        return status;
    }

    const columns: ColumnsType<OrderItem> = [
        {
          title: 'Name',
          dataIndex: 'productName',
          key: 'productName',
          render: text => <Text>{text}</Text>,
          width: (width/2)/4
        },
        {
          title: 'Quantity',
          dataIndex: 'quantity',
          key: 'quantity',
          width: (width/2)/4
        },
        {
          title: 'Gross price',
          dataIndex: 'grossPrice',
          key: 'grossPrice',
          width: (width/2)/4
        },
        {
            title: 'Currency',
            dataIndex: 'currency',
            key: 'currency',
            width: (width/2)/4
        }
      ];
    return (
        <Modal title="Order details"
            centered
            visible={props.visible}
            onOk={() => {props.onConfirm(orderStatus);}}
            okText="Save changes"
            onCancel={(e) => props.onCancel()}
            width={width/2}
            >
            
            <Row>
                <Col flex="auto">
                    <Row justify='start'>
                        <Title level={4}>Order ID</Title>
                    </Row>
                    <Row justify='start'>
                        {props.order.orderId}
                    </Row>
                </Col>
                <Col >
                    <Row justify='start'>
                        <Title level={4}>Dates</Title>
                    </Row>
                    <Row justify='end'>
                        Creation date: { (new Date(props.order.creationDate)).toLocaleString() }
                    </Row>
                    <Row justify='end'>
                        Delivery date: { (new Date(props.order.deliveryDate)).toLocaleString() }
                    </Row>
                </Col>
            </Row>

            <Divider />

            <Row>
                <Col>
                    <Row justify='start'>
                        <Title level={4}>Delivery address</Title>
                    </Row>
                    <Row justify='start'>
                        {props.order.clientAddress.city}
                    </Row>
                    <Row justify='start'>
                        {props.order.clientAddress.zipCode} {props.order.clientAddress.city}
                    </Row>
                </Col>
                <Col flex="auto">
                    <Row justify='end'>
                        <Title level={4}>Status</Title>
                    </Row>
                    <Row justify='end'>
                        <Select
                            defaultValue={ orderStatusToString(props.order.orderStatus) }
                            onChange={updateStatusHandle}
                            style={{ width: 200 }}
                        >
                            <Option value="PickedUpByCourier">Picked up by courier</Option>
                            <Option value="Delivered">Delivered</Option>
                        </Select>
                    </Row>
                </Col>
            </Row>

            <Divider />

            <Row>
                <Col>
                    <Row justify='start'>
                        <Title level={4}>Additional info</Title>
                    </Row>
                    <Row justify='start'>
                        {props.order.additionalInfo}
                    </Row>
                </Col>
            </Row>

            <Divider />

            <Row>
                <Title level={4}>Ordered products</Title>
            </Row>
            <Row style={{ marginTop: 5 }} justify='center'>
                <Table columns={columns} dataSource={props.order.orderItems}/>
            </Row>
        </Modal>
    );
}


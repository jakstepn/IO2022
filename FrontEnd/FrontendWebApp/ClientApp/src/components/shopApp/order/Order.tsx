import { Col, Row, Button, Modal, Card } from 'antd';
import { useCallback, useContext, useEffect, useState } from 'react';
import {useLocation, useNavigate } from 'react-router-dom';
import { EnvironmentOutlined, RightOutlined } from '@ant-design/icons';
import { GlobalStore, globalContext } from '../../../reducers/GlobalStore';
import { green } from '@ant-design/colors';

import {Typography} from "antd"
import { OrderJson } from '../classes/OrderJson';
import { exampleOrders } from '../exampleData/ExampleShopItem';
import { OrderDetails } from './OrderDetails';

const { Title } = Typography;

interface Props {
    order: OrderJson,
    statusUpdateHandler: Function
}

export const Order: React.FC<Props> = (props: Props) => {
  const { globalState } = useContext(globalContext);
  const navigate = useNavigate();
  const location = useLocation(); 
  const [loading, setLoading] = useState(false);
  const [dataLoaded, setDataLoaded] = useState(false);
  const [_pageSize, setPageSize] = useState(5);
  const [totalPages, setTotalPages] = useState<number>(1);
  const [orders, setOrders] = useState<OrderJson[]>();
  const [showOderDetails, setShowOderDetails] = useState(false);

  function fetchData(_pageNumber : number) {
    setLoading(true);
    setOrders(exampleOrders);
    setLoading(false);
    setDataLoaded(true);
  }
  function orderDetailsClickHandle() {
    setShowOderDetails(true);
  }
  function orderDetailsModalCancelHandle() {
    setShowOderDetails(false);
  }
  function orderDetailsModalConfirmHandle(newStatus : string) {
    props.statusUpdateHandler(props.order, newStatus)
    setShowOderDetails(false);
  }
  function orderStatusToString(status : string) {
    if(status.includes("RejectedByShop")) return "Rejected by shop";
    if(status.includes("InProgress")) return "In progress";
    if(status.includes("ReadyForDelivery")) return "Ready for delivery";
    if(status.includes("WaitingForPickUp")) return "Waiting for pick-up";
    if(status.includes("PickedUpByCourier")) return "Picked up by courier";

    return status;
}

  useEffect(() => {
    if(!globalState.isUserAuthenticated) {
        navigate('/login', {replace: true});
      }

    fetchData(1);
  }, []);

    return (
        <div className="space-align-block">
        <Card title={"Order ID: " + props.order.orderId}>
            <Row justify="space-between">
                <Col>
                    <Row>
                        <span style={{ marginRight: 8 }}><EnvironmentOutlined /></span>
                        {props.order.clientAddress.street}, {props.order.clientAddress.zipCode} {props.order.clientAddress.city}
                    </Row>
                    <Row>
                        Creation date: { (new Date(props.order.creationDate)).toLocaleString() }
                    </Row>
                    <Row>
                        Delivery date: { (new Date(props.order.deliveryDate)).toLocaleString() }
                    </Row>
                </Col>

                <Col>
                    <Row justify="end">
                        <Button type="text" onClick={orderDetailsClickHandle} shape="circle" icon={<RightOutlined />} />
                    </Row>
                </Col>
            </Row>
            <Row justify="end">
                { orderStatusToString(props.order.orderStatus) }
            </Row>
        </Card>

        <OrderDetails visible={showOderDetails} onCancel={orderDetailsModalCancelHandle} onConfirm={orderDetailsModalConfirmHandle} order={props.order} />
    </div>
    );

}


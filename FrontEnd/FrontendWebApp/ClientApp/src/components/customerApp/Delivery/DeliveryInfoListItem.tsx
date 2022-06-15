import React, { useContext, useState } from 'react';
import { Card, Row, Col, Image, Rate, Tag, Divider, Button, Modal } from "antd";
import { Delivery } from '../classes/Delivery';
import { Product } from '../classes/Product';
import  ProductListItem  from './ProductListItem'
import { FileImageOutlined } from '@ant-design/icons';

import { Typography } from 'antd';
/*import { globalContext } from '../reducers/GlobalStore';*/
const { Text } = Typography;


interface Props {
    DeliveryInfo: Delivery
}

const DeliverysInfoListItem: React.FC<Props> = (props: Props) => {
    const [isModalVisible, setIsModalVisible] = useState(false);

    const showModal = () => {
        setIsModalVisible(true);
    };

    const handleOk = () => {
        setIsModalVisible(false);
    };

    const handleCancel = () => {
        setIsModalVisible(false);
    };
  /*  const { globalState } = useContext(globalContext);*/

    //const markAsReadClickHandle = () => {
    //    setRatingBook(true);
    //}
    //const markAsReadCancelHandle = () => {
    //    setRatingBook(false);
    //    console.log("Rating has been canceled");
    //  }
    //const markAsReadConfirmHandle = (rating : number) => {
    //    console.log("Book " + props.book.id + " has been read by " + globalState.loggedUser + " and rated as " + rating);
    //    props.book.readByUser = true;
    //    setRatingBook(false);
    //}
    const Add = () => {
        console.log("Show similar books to " + props.DeliveryInfo.status);
    }

    const DemoBox: React.FC<{ children: string; name: string }> = props => (


        <div>
            <p > <b>{props.name }:</b> </p>
             <p>{props.children}</p>
        </div>
    );

    return (
       <div className="space-align-block">
            <Card >
                <Row justify="space-between">
                    <Col span={3} >
                        <DemoBox name="Status" children={props.DeliveryInfo.status} />
                    </ Col>
                    <Col span={4}>
                        <DemoBox name="Oreder Time" children={props.DeliveryInfo.orderTime} />
                    </ Col>
                    <Col span={3 }>
                        
                        <DemoBox name="Delivery Time" children={props.DeliveryInfo.delivertTime} />
                        
                    </Col>
                   
                    <Col span={3}  >
                        <br /> 
                             <p > <b> Price: {props.DeliveryInfo.price }</b> </p>
                        
                    </Col>

                    <Col span={3} >

                        <Row justify="center"> 
                            <Button type="primary" onClick={showModal}>  Detalis </Button>
                        </Row>
                        <br /> 
                        <Row justify="center">
                            <Button type="primary" danger> Cancel </Button>
                        </Row>

                    </Col>
                </Row>
                
            </Card>
            <br />

            <Modal width={800 } title="Products" visible={isModalVisible} onOk={handleOk} onCancel={handleCancel}>
                <Col flex="auto">
                    <div className="site-layout-content">
                        {props.DeliveryInfo.products.map((item: Product) => (
                            <ProductListItem product={item}  />)
                        )}
                    </div>
                </Col>
            </Modal>

           {/* <RateTheBookModal visible={ratingBook} onCancel={markAsReadCancelHandle} onConfirm={markAsReadConfirmHandle}/>*/}
        </div>
    );
}

export default DeliverysInfoListItem;
import { Col, Row, Button, Modal } from 'antd';
import { useCallback, useContext, useEffect, useState } from 'react';
import {useLocation, useNavigate } from 'react-router-dom';
import { TeamOutlined, ShoppingCartOutlined } from '@ant-design/icons';
import { GlobalStore, globalContext } from '../../reducers/GlobalStore';
import { green } from '@ant-design/colors';

import {Typography} from "antd"

const { Title } = Typography;


export default function CourierHome() {
  const { globalState } = useContext(globalContext);
  const navigate = useNavigate();
    const location = useLocation(); 
    const [isModalVisible, setIsModalVisible] = useState(false);
    const [messenge, setMessenge] = useState("");
    const showModal = () => {
        setIsModalVisible(true);
    };

    const handleCancel = () => {
        setIsModalVisible(false);
    };

    const handleDelivered = () => {
        setMessenge("Thank you for delivery!");
        showModal();
    };

    const handleCancelPackege = () => {
        setMessenge("Packege delivery succesfully cancelled!");
        showModal();
    };


  useEffect(() => {
    if(!globalState.isUserAuthenticated) {
      navigate('/login', {replace: true});
    }
    navigate('/courier/orders', {replace: true});
  }, []);

    return (
        <div  >
            <Row justify="space-around" align="middle" style={{ minHeight: "81vh" }}>
                <Col flex="auto" >
                    <Row justify="end">
                        <Button style={{ height: 200, width: 300 }} type="primary" onClick={handleDelivered} > Mark package as delivered  </Button>
                    </Row>
                </Col>
                <Col flex="auto" >
                    
                </Col>
                <Col flex="auto"  onClick={() => console.log("asd") }>
                    <Button style={{ height: 200, width: 300 }} danger onClick={handleCancelPackege}> Cancel delivery </Button>
                </Col>
            </Row>

            <Modal width={800} visible={isModalVisible} onCancel={handleCancel} onOk={handleCancel }>
                <Col flex="auto">
                    <div className="site-layout-content">
                        <Row/>
                        <Row justify="center">
                        <p style={{ fontSize: 60, textAlign: "center" }}>
                                {messenge }
                         </p>
                        </Row>
                        <Row />
                    </div>
                </Col>
            </Modal>


      </div>
    );

}


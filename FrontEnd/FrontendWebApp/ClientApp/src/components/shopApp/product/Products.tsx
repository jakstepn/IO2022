import { Col, Row, Button, Modal, Table, Space } from 'antd';
import { useCallback, useContext, useEffect, useState } from 'react';
import {useLocation, useNavigate } from 'react-router-dom';
import { LoadingOutlined, SmileOutlined, ExclamationCircleOutlined } from '@ant-design/icons';
import { GlobalStore, globalContext } from '../../../reducers/GlobalStore';
import { green } from '@ant-design/colors';

import {Typography} from "antd"
import { OrderJson } from '../classes/OrderJson';
import { exampleProducts } from '../exampleData/ExampleShopItem';
import { useWindowDimensions } from 'react-native';
import { ColumnsType } from 'antd/lib/table';
import { Product } from '../classes/Product';
import { AddProduct } from './AddProduct';

const { Title, Text } = Typography;
const { confirm } = Modal;

export default function Products() {
  const { globalState } = useContext(globalContext);
  const navigate = useNavigate();
  const location = useLocation(); 
  const [loading, setLoading] = useState(false);
  const [dataLoaded, setDataLoaded] = useState(false);
  const [_pageSize, setPageSize] = useState(5);
  const [totalPages, setTotalPages] = useState<number>(1);
  const [products, setProducts] = useState<Product[]>();
  const { height, width } = useWindowDimensions();
  const [addingProduct, setAddingProduct] = useState(false);
  let deletingProduct : Product;

  function fetchData(_pageNumber : number) {
    setLoading(true);
    setProducts(exampleProducts);
    setLoading(false);
    setDataLoaded(true);
  }

  useEffect(() => {
    if(!globalState.isUserAuthenticated) {
        navigate('/login', {replace: true});
      }
      
    fetchData(1);
  }, []);

  const showConfirm = () => {
    confirm({
      title: 'Are you sure you want to delete this product?',
      icon: <ExclamationCircleOutlined />,
      width: width/2,
      content: deletingProduct !== undefined ? 
                  <Text>
                    Order ID: { deletingProduct.productId } 
                    <br />    
                    Name:     { deletingProduct.name }     
                    <br />
                    Price:    { deletingProduct.price}     
                    <br />
                    Quantity: { deletingProduct.quantity } 
                    <br />
                    Category: { deletingProduct.category }
                  </Text>
                : "Unknown product",
      onOk() {
        return new Promise((resolve, reject) => {
          setTimeout(Math.random() > 0.5 ? resolve : reject, 1000);
        }).catch(() => console.log('Oops errors!'));
      },
      onCancel() {
        console.log('Cancel');
      },
    });
  };
  const deleteProductClickHandler = (product: Product) => {
    deletingProduct = product;
    showConfirm();
    console.log("Delete product " + product.productId);
  }
  const addProductHandler = () => {
    setAddingProduct(true);
  }
  const addProductConfirmHandler = () => {
    setAddingProduct(false);
  }
  const addProductCancelHandler = () => {
    setAddingProduct(false);
  }
  const columns: ColumnsType<Product> = [
    {
      title: 'Product ID',
      dataIndex: 'productId',
      key: 'productId',
      render: text => <Text>{text}</Text>,
      width: (width)/3
    },
    {
      title: 'Name',
      dataIndex: 'name',
      key: 'name',
      width: (width)/6
    },
    {
      title: 'Category',
      dataIndex: 'category',
      key: 'category',
      width: (width)/7
    },
    {
        title: 'Price',
        dataIndex: 'price',
        key: 'price',
        width: (width)/8
    },
    {
        title: 'Quantity',
        dataIndex: 'quantity',
        key: 'quantity',
        width: (width)/8
    },
    {
        key: 'action',
        render: (_, record) => (
            <Button onClick={() => deleteProductClickHandler(record)}>Delete</Button>
          ),
        width: '100px'
    }
  ];
    return (
        <div  >
           <Row style={{ marginTop: 50 }}>
                <Col flex="auto">
                    <div className="site-layout-content">
                        <Row justify='space-between'>
                            <Col flex='start'>
                                <Title>Products</Title>
                            </Col>
                            <Col flex='end'>
                                <Button onClick={addProductHandler}>Add a new product</Button>
                            </Col>
                        </Row>
                        
                        { !loading ? 
                            (dataLoaded ? 
                                <Row style={{ marginTop: 5 }} justify='center'>
                                    <Table columns={columns} dataSource={products}/>
                                </Row>
                                :
                                <Row align="middle" justify="center" style={{ marginTop: 50 }}>
                                    <Title level={5} type="secondary">No products for now <SmileOutlined /></Title>
                                </Row>
                            ) : 
                            <Col flex="auto">
                                <Row align="middle" justify="center">
                                    <LoadingOutlined style={{ fontSize: '70px' }}/>
                                </Row>
                            </Col>
                        }
                    </div> 
                    <br />        
                </Col>
            </Row>
            <AddProduct visible={addingProduct} onConfirm={addProductConfirmHandler} onCancel={addProductCancelHandler}/>
      </div>
    );

}


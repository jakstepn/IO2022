import { Col, Row, Button, Modal, Table, Space, message, Form, InputNumber, Input, Popconfirm } from 'antd';
import { ReactChild, ReactFragment, ReactPortal, useCallback, useContext, useEffect, useState } from 'react';
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

interface EditableCellProps extends React.HTMLAttributes<HTMLElement> {
  editing: boolean;
  dataIndex: string;
  title: any;
  inputType: 'number' | 'text';
  record: Product;
  index: number;
  children: React.ReactNode;
}
const EditableCell: React.FC<EditableCellProps> = ({
  editing,
  dataIndex,
  title,
  inputType,
  record,
  index,
  children,
  ...restProps
}) => {
  const inputNode = inputType === 'number' ? <InputNumber /> : <Input />;
  return (
    <td {...restProps}>
      {editing ? (
        <Form.Item
          name={dataIndex}
          style={{ margin: 0 }}
          rules={[
            {
              required: true,
              message: `Please Input ${title}!`,
            },
          ]}
        >
          {inputNode}
        </Form.Item>
      ) : (
        children
      )}
    </td>
  );
};

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
  const [form] = Form.useForm();
  const [editingID, setEditingID] = useState("");
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
          let newProducts = products?.filter(p => p.productId !== deletingProduct.productId);
          setTimeout(Math.random() > 0.5 ? resolve : reject, 1000);
          setProducts(newProducts);
        }).catch(() => console.log('Oops errors!'));
      },
      onCancel() {
        console.log('Cancel');
      },
    });
  };
  const deleteProductClickHandler = (product: Product) => {
    setEditingID('');
    deletingProduct = product;
    showConfirm();
  }
  const addProductHandler = () => {
    setAddingProduct(true);
  }
  const addProductConfirmHandler = (newProduct: Product) => {
    console.log("Adding product [id:" + newProduct.productId + ", name:" + newProduct.name);
    message.success('Succesfully added new product!');
    products !== undefined ? setProducts([...products, newProduct]) : setProducts([newProduct]);
    setAddingProduct(false);
  }
  const addProductCancelHandler = () => {
    setAddingProduct(false);
  }

  const isEditing = (record: Product) => record.productId === editingID;
  const edit = (record: Product) => {
    form.setFieldsValue({ ...record });
    let id = record !== undefined ? record.productId !== undefined ? record.productId : '' : '';
    setEditingID(id);
  };
  const cancel = () => {
    setEditingID('');
  };
  const save = async (id: string) => {
    try {
      const row = (await form.validateFields()) as Product;

      const newData = products === undefined ? [] : [...products];
      const index = newData.findIndex(item => id === item.productId);
      if (index > -1) {
        const item = newData[index];
        newData.splice(index, 1, {
          ...item,
          ...row,
        });
        setProducts(newData);
        setEditingID('');
      } else {
        newData.push(row);
        setProducts(newData);
        setEditingID('');
      }
    } catch (errInfo) {
      console.log('Validate Failed:', errInfo);
    }
  };
  const columns = [
    {
      title: 'Product ID',
      dataIndex: 'productId',
      key: 'productId',
      width: (width)/3,
      editable: false
    },
    {
      title: 'Name',
      dataIndex: 'name',
      key: 'name',
      width: (width)/6,
      editable: true
    },
    {
      title: 'Category',
      dataIndex: 'category',
      key: 'category',
      width: (width)/7,
      editable: true
    },
    {
        title: 'Price',
        dataIndex: 'price',
        key: 'price',
        width: (width)/8,
        editable: true
    },
    {
        title: 'Quantity',
        dataIndex: 'quantity',
        key: 'quantity',
        width: (width)/8,
        editable: true
    },
    {
        title: 'operation',
        dataIndex: 'operation',
        render: (_: any, record: Product) => {
          const editable = isEditing(record);
          return editable ? (
            <span>
              <Button onClick={() => save(record.productId)} style={{ marginRight: 8 }}>
                Save
              </Button>
              <Popconfirm title="Sure to cancel?" onConfirm={cancel}>
                <Button>Cancel</Button>
              </Popconfirm>
            </span>
          ) : (
            <Row justify='center'>
              <Button disabled={editingID !== ''} onClick={() => edit(record)} style={{ marginRight: 10 }}>Edit</Button>
              <Button onClick={() => deleteProductClickHandler(record)}>Delete</Button>
            </Row>
          );
        },
        width: '210px'
    }
  ];
  const mergedColumns = columns.map(col => {
    if (!col.editable) {
      return col;
    }
    return {
      ...col,
      onCell: (record: Product) => ({
        record,
        inputType: col.dataIndex === 'price' || col.dataIndex === 'quantity' ? 'number' : 'text',
        dataIndex: col.dataIndex,
        title: col.title,
        editing: isEditing(record),
      }),
    };
  });
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
                                    <Form form={form} component={false}>
                                        <Table
                                        components={{
                                          body: {
                                            cell: EditableCell,
                                          },
                                        }}
                                        bordered
                                        dataSource={products}
                                        columns={mergedColumns}
                                        rowClassName="editable-row"
                                        pagination={{
                                          onChange: cancel,
                                        }}
                                      />
                                    </Form>
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


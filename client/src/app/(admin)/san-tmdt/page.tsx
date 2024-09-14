'use client';
import { Divider, Flex, Table } from 'antd';
import ModalConnect from './components/modal-connect';
import { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { saveShop } from '../../../redux/shop.slice';

const SanTMDTPage = () => {
  const dispatch = useDispatch();
  const dataSource = [
    {
      key: '1',
      name: 'Mike',
      age: 32,
      address: '10 Downing Street'
    },
    {
      key: '2',
      name: 'John',
      age: 42,
      address: '10 Downing Street'
    }
  ];

  const columns = [
    {
      title: 'Name',
      dataIndex: 'name',
      key: 'name'
    },
    {
      title: 'Age',
      dataIndex: 'age',
      key: 'age'
    },
    {
      title: 'Address',
      dataIndex: 'address',
      key: 'address'
    }
  ];

  useEffect(() => {
    dispatch(saveShop({ firstName: 'asd' }));
  });

  return (
    <>
      <Flex gap="small" wrap justify="space-between">
        <h2 className="font-bold">Kết nối sàn TMĐT</h2>
        <ModalConnect />
      </Flex>
      <Divider />
      <Table dataSource={dataSource} columns={columns} size="large" />
    </>
  );
};

export default SanTMDTPage;

'use client';
import { Divider, Flex, Table } from 'antd';
import ModalConnect from './components/modal-connect';
import { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { useCreateTokenMutation } from '../../../services/shop.service';
import { useSearchParams } from 'next/navigation';

const SanTMDTPage = () => {
  const [createToken, { isLoading, isSuccess, isError, error }] =
    useCreateTokenMutation();

  const dispatch = useDispatch();
  const searchParams = useSearchParams();

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
    const shop_id = searchParams.get('shop_id');
    const code = searchParams.get('code');
    if (code && shop_id) {
      const data = {
        code,
        shopId: shop_id
      };
      createToken(data);
    }
  }, [searchParams]);

  return (
    <>
      {isLoading && <p>Creating token...</p>}
      {isSuccess && <p>Token created successfully!</p>}
      {isError && <p>Failed to create token: {error?.message}</p>}
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

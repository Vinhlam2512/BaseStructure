'use client';
import { PlusOutlined } from '@ant-design/icons';
import { Button, Card, Flex, Modal } from 'antd';
import { useState } from 'react';

const ecommerces = {};

const ModalConnect: React.FC = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);

  const showModal = () => {
    setIsModalOpen(true);
  };

  const handleOk = () => {
    setIsModalOpen(false);
  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };

  return (
    <>
      <Button type="primary" onClick={showModal}>
        <PlusOutlined />
        Kết nối gian hàng mới
      </Button>
      <Modal
        title="Kết nối gian hàng trên sàn"
        open={isModalOpen}
        onOk={handleOk}
        onCancel={handleCancel}
      >
        <Flex gap="middle" vertical>
          <Card bordered={true}>Kết nối với TikTokShop</Card>
          <Card bordered={true}>Kết nối với Lazada</Card>
          <Card bordered={true}>Kết nối với Shopee</Card>
        </Flex>
      </Modal>
    </>
  );
};

export default ModalConnect;

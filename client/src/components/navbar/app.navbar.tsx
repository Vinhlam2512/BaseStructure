import { Button, Menu, Avatar, theme, MenuProps, Layout } from 'antd';
import React from 'react';
import {
  MenuFoldOutlined,
  MenuUnfoldOutlined,
  UserOutlined
} from '@ant-design/icons';

const items1: MenuProps['items'] = ['1', '2', '3'].map((key) => ({
  key,
  label: `nav ${key}`
}));

interface NavbarProps {
  collapsed: boolean;
  setCollapsed: () => void;
}

const { Header } = Layout;

const Navbar: React.FC<NavbarProps> = ({ collapsed, setCollapsed }) => {
  const {
    token: { colorBgContainer }
  } = theme.useToken();

  return (
    <Header
      style={{
        padding: '0 20px 0 0',
        background: colorBgContainer,
        display: 'flex',
        alignItems: 'center'
      }}
    >
      <Button
        type="text"
        icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
        onClick={setCollapsed}
        style={{
          fontSize: '16px',
          width: 64,
          height: 64
        }}
      />
      <Menu
        theme="light"
        mode="horizontal"
        defaultSelectedKeys={['2']}
        items={items1}
        style={{ flex: 1, minWidth: 0 }}
      />
      <Avatar size="large" icon={<UserOutlined />} className="pr-5" />
    </Header>
  );
};

export default Navbar;

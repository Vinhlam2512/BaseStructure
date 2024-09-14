'use client';
import { Avatar, Button, Layout, Menu, MenuProps, theme } from 'antd';
import { useEffect, useState } from 'react';

import Sidebar from '../../components/sidebar/app.sidebar';
import Navbar from '../../components/navbar/app.navbar';

const { Content } = Layout;

export default function RootLayout({
  children
}: Readonly<{
  children: React.ReactNode;
}>) {
  const [collapsed, setCollapsed] = useState(false);
  const {
    token: { colorBgContainer, borderRadiusLG }
  } = theme.useToken();

  const toggleCollapsed = () => {
    setCollapsed(!collapsed);
  };

  return (
    <Layout className={`h-screen ${collapsed ? '' : 'ant-layout-has-sider'}`}>
      <Sidebar collapsed={collapsed} />
      <Layout>
        <Navbar collapsed={collapsed} setCollapsed={() => toggleCollapsed()} />
        <Content
          style={{
            margin: '24px 16px',
            padding: 24,
            minHeight: 280,
            background: colorBgContainer,
            borderRadius: borderRadiusLG
          }}
        >
          {children}
        </Content>
      </Layout>
    </Layout>
  );
}

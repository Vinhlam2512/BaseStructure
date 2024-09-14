import { Layout, Menu, MenuProps } from 'antd';
import React from 'react';
import {
  AppstoreOutlined,
  ContainerOutlined,
  DesktopOutlined,
  MailOutlined,
  MenuFoldOutlined,
  MenuUnfoldOutlined,
  PieChartOutlined
} from '@ant-design/icons';
import { useRouter } from 'next/navigation';

type MenuItem = Required<MenuProps>['items'][number];

interface SidebarProps {
  collapsed: boolean;
}
const { Sider } = Layout;

const Sidebar: React.FC<SidebarProps> = ({ collapsed }) => {
  const router = useRouter();

  const items: MenuItem[] = [
    {
      key: '1',
      icon: <PieChartOutlined />,
      label: 'Sàn TMĐT',
      onClick: () => router.push('/san-tmdt')
    },
    { key: '2', icon: <DesktopOutlined />, label: 'Option 2' },
    { key: '3', icon: <ContainerOutlined />, label: 'Option 3' },
    {
      key: 'sub1',
      label: 'Navigation One',
      icon: <MailOutlined />,
      children: [
        { key: '5', label: 'Option 5' },
        { key: '6', label: 'Option 6' },
        { key: '7', label: 'Option 7' },
        { key: '8', label: 'Option 8' }
      ]
    },
    {
      key: 'sub2',
      label: 'Navigation Two',
      icon: <AppstoreOutlined />,
      children: [
        { key: '9', label: 'Option 9' },
        { key: '10', label: 'Option 10' },
        {
          key: 'sub3',
          label: 'Submenu',
          children: [
            { key: '11', label: 'Option 11' },
            { key: '12', label: 'Option 12' }
          ]
        }
      ]
    }
  ];

  return (
    <Sider trigger={null} collapsible collapsed={collapsed} width={250}>
      {/* <Image
src="/logo.jpg"
width={100}
height={100}
alt="Picture of the author"
/> */}
      <div className="h-[100px]"></div>
      <Menu disabledOverflow={true} mode="inline" theme="dark" items={items} />
    </Sider>
  );
};

export default Sidebar;

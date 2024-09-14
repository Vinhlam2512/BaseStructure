'use client';
import type { ReactNode } from 'react';
import { Provider } from 'react-redux';
import { store } from '../redux/store';

interface Props {
  readonly children: ReactNode;
}

export const StoreProvider = ({ children }: Props) => {
  return <Provider store={store}>{children}</Provider>;
};

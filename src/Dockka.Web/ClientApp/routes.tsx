import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './react/pages/Home';

export const routes: any = (
  <Layout>
    <Route exact path='/' component={Home} />
  </Layout>
);

import { createRouter, createWebHistory } from 'vue-router'

const routes = [

  {
    path: '/register-shipper',
    name: 'RegisterShipper',

    component: () => import(/* webpackChunkName: "about" */ '../views/RegisterShipper.vue')
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router

import { createRouter, createWebHistory } from 'vue-router'

const routes = [

  {
    path: '/register-shipper',
    name: 'RegisterShipper',
    component: () => import(/* webpackChunkName: "about" */ '../views/RegisterShipper.vue')
  },
  {
    path: '/submit-trouble',
    name: 'SubmitTrouble',
    component: () => import(/* webpackChunkName: "about" */ '../views/SubmitTrouble.vue')
    
  }
]


const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router

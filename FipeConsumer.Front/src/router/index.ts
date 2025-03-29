// src/router/index.ts
import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import Home from '@/views/Home.vue'

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  // Outras rotas podem ser adicionadas aqui
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router

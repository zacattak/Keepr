import { createRouter, createWebHashHistory } from 'vue-router'
import { authGuard, authSettled } from '@bcwdev/auth0provider-client'

function loadPage(page) {
  return () => import(`./pages/${page}.vue`)
}

const routes = [
  {
    path: '/',
    name: 'Home',
    component: loadPage('HomePage')
  },
  {
    path: '/profile/:accountId',
    name: 'Profile',
    component: loadPage('ProfilePage')
  },
  {
    path: '/account',
    name: 'Account',
    component: loadPage('AccountPage'),
    beforeEnter: authGuard
  },
  {
    path: '/account/vaults',
    name: 'Vaults',
    component: loadPage('VaultsPage'),
    beforeEnter: authGuard
  },
  {
    path: '/vaults/:vaultId',
    name: 'VaultDetails',
    component: loadPage('VaultDetailsPage'),
    beforeEnter: authSettled
  },




]

export const router = createRouter({
  linkActiveClass: 'router-link-active',
  linkExactActiveClass: 'router-link-exact-active',
  history: createWebHashHistory(),
  routes
})

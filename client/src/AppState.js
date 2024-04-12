import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */

  // @ts-ignore
  account: {},

  keeps: [],

  vaults: [],

  accountVaults: [],

  vaultKeeps: [],


  // FIXME crate new array for just logged in user's vaults

  activeKeep: null,

  activeVault: null,

  activeAccount: null

})

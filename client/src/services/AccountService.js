import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async updateAccount(editableAccountData) {
    const res = await api.put('/account', editableAccountData)
    logger.log('Updated account', res.data)
    AppState.account = new Account(res.data)
  }
  async getAccountById(profileId) {
    const response = await api.get(`api/profiles/${profileId}`)
    logger.log('got profile', response.data)
    const profile = new Account(response.data)
    AppState.activeAccount = profile
  }
}

export const accountService = new AccountService()

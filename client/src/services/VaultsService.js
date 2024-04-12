import { AppState } from "../AppState.js";
import { PrivateVault } from "../models/PrivateVault.js";
import { Vault } from "../models/Vault.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";


class VaultsService {
    async getVaults() {
        const response = await api.get('api/vaults')
        logger.log('GOT VAULTS', response.data)
        AppState.vaults = response.data.map(pojo => new Vault(pojo))
    }

    async createVault(vaultData) {
        const response = await api.post('api/vaults', vaultData)
        logger.log('created vault', response.data)
        const newVault = new Vault(response.data)
        return newVault
    }

    async createPrivateVault(privateVaultData) {
        const response = await api.post('api/vaults', privateVaultData)
        logger.log('created private vault', response.data)
        const privateVault = new PrivateVault(response.data)
        return privateVault
    }

    async getVaultsByAccountId(accountId) {
        const res = await api.get(`api/profiles/${accountId}/vaults`)
        // api/profiles/${accountId}/vaults

        //note this is probably where my problem lies, working on profile page because of route params
        logger.log('got vaults', res.data)
        AppState.vaults = res.data.map(pojo => new Vault(pojo))
    }

    async getAccountVaults() {
        const response = await api.get('/account/vaults')
        logger.log('GOT VAULTS', response.data)
        AppState.accountVaults = response.data.map(pojo => new Vault(pojo))
    }

    async getVaultById(vaultId) {
        const response = await api.get(`api/vaults/${vaultId}`)
        logger.log('got vault', response.data)
        AppState.activeVault = new Vault(response.data)
    }
}

export const vaultsService = new VaultsService()
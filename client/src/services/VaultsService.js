import { AppState } from "../AppState.js";
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
}

export const vaultsService = new VaultsService()
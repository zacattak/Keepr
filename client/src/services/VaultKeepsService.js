import { VaultKeep } from "../models/VaultKeep.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class VaultKeepsService {

    async createVaultKeep(vaultKeepData) {
        const response = await api.post('api/vaultKeeps', vaultKeepData)
        logger.log('created vaultKeep', response.data)
        const newVaultKeep = new VaultKeep(response.data)

        // if (AppState.activeAccount?.id == newVaultKeep.creatorId) {
        //     AppState.vaultKeeps.push(newVaultKeep)
        // }
        return newVaultKeep
    }

    // async getVaultKeepsByAccountId(accountId){
    //     const response = await api.get(`api/`)
    // }

}

export const vaultKeepsService = new VaultKeepsService()
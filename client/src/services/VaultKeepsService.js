import { VaultKeep } from "../models/VaultKeep.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class VaultKeepsService {

    async createVaultKeep(vaultKeepData) {
        const response = await api.post('api/vaultKeeps', vaultKeepData)
        logger.log('created vaultKeep', response.data)
        const newVaultKeep = new VaultKeep(response.data)
        return newVaultKeep
    }

}

export const vaultKeepsService = new VaultKeepsService()
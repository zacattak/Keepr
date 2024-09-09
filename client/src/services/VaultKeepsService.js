import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
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
        AppState.activeKeep.kept++
        return newVaultKeep
    }

    async getVaultKeepsByVaultId(vaultId) {
        AppState.vaultKeeps = []
        const response = await api.get(`api/vaults/${vaultId}/keeps`);
        logger.log('vaults keeps had', response.data);
        const keeps = response.data.map(pojo => new VaultKeep(pojo));
        AppState.vaultKeeps = keeps

    }


    async getVaultKeepById(vaultKeepId) {
        const res = await api.get(`api/vaultKeeps/${vaultKeepId}`)
        logger.log('GOT VAULT KEEP', res.data)
        AppState.activeVaultKeep = new VaultKeep(res.data)
    }


    async deleteVaultKeep(vaultKeepId) {
        const response = await api.delete(`api/vaultKeeps/${vaultKeepId}`)
        logger.log('destroyed vaultKeep', response.data)
    }

}

export const vaultKeepsService = new VaultKeepsService()
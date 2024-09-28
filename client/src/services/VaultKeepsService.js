import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { KeepClone } from "../models/KeepClone.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class VaultKeepsService {

    async createVaultKeep(vaultKeepData) {
        const response = await api.post('api/vaultKeeps', vaultKeepData)
        logger.log('created vaultKeep', response.data)
        const newVaultKeep = new KeepClone(response.data)

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
        const keeps = response.data.map(pojo => new KeepClone(pojo));
        AppState.vaultKeeps = keeps

    }


    // async getVaultKeepById(vaultKeepId) {
    //     const response = await api.get(`api/vaultKeeps/${vaultKeepId}`)
    //     logger.log('GOT VAULT KEEP', response.data)
    //     // AppState.activeVaultKeep = vaultKeepId

    // }
    async getVaultKeepById(vaultKeepId) {
        const res = await api.get(`api/vaultKeeps/${vaultKeepId}`)
        logger.log('vault keep had', res.data)
        AppState.activeVaultKeep = new KeepClone(res.data)

    }


    async deleteVaultKeep(vaultKeepId) {
        const response = await api.delete(`api/vaultKeeps/${vaultKeepId}`)
        logger.log('destroyed vaultKeep', response.data)
    }

}

export const vaultKeepsService = new VaultKeepsService()